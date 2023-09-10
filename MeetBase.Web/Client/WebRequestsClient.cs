
using Microsoft.AspNetCore.WebUtilities;

using Newtonsoft.Json;

using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;

namespace MeetBase.Web
{
    /// <summary>
    /// Client that provides HTTP calls for sending and receiving information from a HTTP server
    /// </summary>
    /// <typeparam name="TAuthenticationArgs">
    /// The type of the authentication args.
    /// NOTE: The authentication args determine the authentication scheme that will be used against the server!
    /// </typeparam>
    public abstract class WebRequestsClient<TAuthenticationArgs>
        where TAuthenticationArgs : class
    {
        #region Constants

        /// <summary>
        /// The name of the PATCH HTTP method
        /// </summary>
        public const string PATCH = "PATCH";

        /// <summary>
        /// The json request and response preferred media type
        /// </summary>
        public const string MediaTypeJson = "application/json";

        /// <summary>
        /// The standard deserialization error message
        /// </summary>
        public string DeserializationErrorMessage { get; set; } = "Failed to deserialize server response to the expected type!";

        #endregion

        #region Private Members

        /// <summary>
        /// THe member of the <see cref="Client"/> property
        /// </summary>
        private HttpClient? mClient;

        #endregion

        #region Protected Properties

        /// <summary>
        /// The HTTP client that is used for sending the HTTP requests
        /// NOTE: HTTP client is recommended to be injected as a Singleton in DI
        ///       https://stackoverflow.com/questions/4015324/how-to-make-http-post-web-request
        /// </summary>
        protected HttpClient Client
        {
            get
            {
                if (mClient is null)
                {
                    mClient = CreateClient();

                    ConfigureClient(mClient);
                }

                return mClient;
            }
        }

        /// <summary>
        /// The type for the request body
        /// </summary>
        protected HttpContentType RequestBodyType { get; set; } = HttpContentType.JsonBody;

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public WebRequestsClient() : base()
        {

        }

        #endregion

        #region Public Methods

        #region Non-Generic

        #region POST

        /// <summary>
        /// POSTs a web request to an URL that contains the specified <paramref name="content"/> and returns a <see cref="HttpResponseMessage"/>
        /// </summary>
        /// <param name="url">The URL</param>
        /// <param name="content">The content to post</param>
        /// <param name="authenticationArgs">If specified, provides the Authorization header is set</param>
        /// <returns></returns>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public async Task<HttpResponseMessage> PostAsync(string url, object? content, TAuthenticationArgs? authenticationArgs)
        {
            // Set the authorization scheme
            SetAuthorizationHeader(authenticationArgs);

            HttpResponseMessage responseMessage;

            #region Send Request

#if DEBUG
            if (ShouldLogRequests())
                Debug.WriteLine($"POST request made to: {url}");
#endif

            try
            {
                // If there is content...
                if (content is not null)
                    responseMessage = await Client.PostAsync(url, CreateHttpContent(content));
                // Else...
                else
                    responseMessage = await Client.PostAsync(url, null);
            }
            catch
            {
                // Re-throw
                throw;
            }

            #endregion

            return responseMessage;
        }

        /// <summary>
        /// POSTs a web request to an URL that contains the specified <paramref name="content"/> and returns a <see cref="WebRequestResult{T}"/>
        /// </summary>
        /// <param name="url">The URL</param>
        /// <param name="content">The content to post</param>
        /// <param name="authenticationArgs">If specified, provides the Authorization header is set</param>
        /// <returns></returns>
        public async Task<WebRequestResult<byte[]>> PostBytesAsync(string url, object content, TAuthenticationArgs? authenticationArgs)
        {
#if DEBUG
            if (ShouldLogRequests())
                Debug.WriteLine($"POST bytes request made to: {url}");
#endif

            // Create server response holder
            HttpResponseMessage serverResponse;
            try
            {
                // Make the standard Post call first
                serverResponse = await PostAsync(url, content, authenticationArgs);
            }
            catch (Exception ex)
            {
                // If we got unexpected error, return that
                return new WebRequestResult<byte[]>(ex);
            }

            // Create the result
            var result = await serverResponse.CreateWebRequestResultFromStreamAsync();

            // If the response status code is not 200...
            if (!serverResponse.IsSuccessStatusCode)
            {
                // Call failed
                result.ErrorMessage = ParseErrorMessageCore(result.RawServerResponse);

                // Done
                return result;
            }

            // Return result
            return result;
        }

        #endregion

        #region GET

        /// <summary>
        /// GETs a web request to a URL and returns a <see cref="HttpResponseMessage"/>
        /// </summary>
        /// <param name="url">The URL</param>
        /// <param name="authenticationArgs">If specified, provides the Authorization header is set</param>
        /// <returns></returns>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public async Task<HttpResponseMessage> GetAsync(string url, TAuthenticationArgs? authenticationArgs)
        {
            // Set the authorization scheme
            SetAuthorizationHeader(authenticationArgs);

            #region Send Request

#if DEBUG
            if (ShouldLogRequests())
                Debug.WriteLine($"GET request made to: {url}");
#endif

            try
            {
                // Send it with the request
                return await Client.GetAsync(url);
            }
            catch
            {
                // Re-throw
                throw;
            }

            #endregion
        }

        /// <summary>
        /// GETs the bytes that are returned in the <see cref="HttpResponseMessage"/>
        /// </summary>
        /// <param name="url">The URL</param>
        /// <param name="authenticationArgs">If specified, provides the Authorization header is set</param>
        /// <returns></returns>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public async Task<IFailable<byte[]>> GetBytesAsync(string url, TAuthenticationArgs? authenticationArgs)
        {
            // Set the authorization scheme
            SetAuthorizationHeader(authenticationArgs);

            #region Send Request

#if DEBUG
            if (ShouldLogRequests())
                Debug.WriteLine($"GET bytes request made to: {url}");
#endif

            try
            {
                // Send it with the request
                var response = await Client.GetAsync(url);

                // Make sure we got a valid response
                response.EnsureSuccessStatusCode();

                // Load the content
                await response.Content.LoadIntoBufferAsync();

                // Return
                return Failable.FromResult(await response.Content.ReadAsByteArrayAsync());

            }
            catch (Exception ex)
            {
                return new Failable<byte[]>(ex);
            }

            #endregion
        }

        /// <summary>
        /// Downloads the file from the specified <paramref name="url"/> and saves it at the specified <paramref name="fileName"/>
        /// </summary>
        /// <param name="url">The URL</param>
        /// <param name="fileName">The file path combined with the file name</param>
        /// <param name="authenticationArgs">If specified, provides the Authorization header is set</param>
        /// <returns></returns>
        public async Task<IFailable> DownloadFileAsync(string url, string fileName, TAuthenticationArgs authenticationArgs)
        {
            // Set the authorization scheme
            SetAuthorizationHeader(authenticationArgs);

            #region Send Request

#if DEBUG
            if (ShouldLogRequests())
                Debug.WriteLine($"GET file (download) request made to: {url}");
#endif

            try
            {
                // Send it with the request
                var response = await Client.GetStreamAsync(url);

                using (var fs = new FileStream(fileName, FileMode.CreateNew))
                {
                    await response.CopyToAsync(fs);
                }

                return Failable.Success;
            }
            catch (Exception ex)
            {
                return new Failable(ex);
            }

            #endregion
        }

        #endregion

        #region PUT

        /// <summary>
        /// PUTs the specified <paramref name="content"/> at the specified <paramref name="url"/> and returns a <see cref="HttpResponseMessage"/>
        /// </summary>
        /// <param name="url">The URL</param>
        /// <param name="content">The content to put</param>
        /// <param name="authenticationArgs">If specified, provides the Authorization header is set</param>
        /// <returns></returns>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public async Task<HttpResponseMessage> PutAsync(string url, object? content, TAuthenticationArgs? authenticationArgs)
        {
            // Set the authorization scheme
            SetAuthorizationHeader(authenticationArgs);

            #region Send Request

#if DEBUG
            if (ShouldLogRequests())
                Debug.WriteLine($"PUT request made to: {url}");
#endif

            try
            {
                // Send it with the request
                return await Client.PutAsync(url, content is null ? null : CreateHttpContent(content));
            }
            catch
            {
                // Re-throw
                throw;
            }

            #endregion
        }

        /// <summary>
        /// PUTs the specified <paramref name="content"/> at the specified <paramref name="url"/> and returns a <see cref="WebRequestResult{T}"/>
        /// </summary>
        /// <param name="url">The URL</param>
        /// <param name="content">The content to put</param>
        /// <param name="authenticationArgs">If specified, provides the Authorization header is set</param>
        /// <returns></returns>
        public async Task<WebRequestResult<byte[]>> PutBytesAsync(string url, object content, TAuthenticationArgs authenticationArgs)
        {
#if DEBUG
            if (ShouldLogRequests())
                Debug.WriteLine($"PUT bytes request made to: {url}");
#endif

            // Create server response holder
            HttpResponseMessage serverResponse;
            try
            {
                // Make the standard PUT call first
                serverResponse = await PutAsync(url, content, authenticationArgs);
            }
            catch (Exception ex)
            {
                // If we got unexpected error, return that
                return new WebRequestResult<byte[]>(ex);
            }

            // Create the result
            var result = await serverResponse.CreateWebRequestResultFromStreamAsync();

            // If the response status code is not 200...
            if (!serverResponse.IsSuccessStatusCode)
            {
                // Call failed
                result.ErrorMessage = ParseErrorMessageCore(result);

                // Done
                return result;
            }

            // Return result
            return result;
        }

        #endregion

        #region PATCH

        /// <summary>
        /// PATCHes the specified <paramref name="content"/> at the specified <paramref name="url"/> and returns a <see cref="HttpResponseMessage"/>
        /// </summary>
        /// <param name="url">The URL</param>
        /// <param name="content">The content to patch</param>
        /// <param name="authenticationArgs">If specified, provides the Authorization header is set</param>
        /// <returns></returns>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public async Task<HttpResponseMessage> PatchAsync(string url, object? content, TAuthenticationArgs authenticationArgs)
        {
            // Set the authorization scheme
            SetAuthorizationHeader(authenticationArgs);

            #region Send Request

#if DEBUG
            if (ShouldLogRequests())
                Debug.WriteLine($"PATCH request made to: {url}");
#endif

            try
            {
                // Create the request
                var request = new HttpRequestMessage(new HttpMethod(PATCH), url)
                {
                    Content = content is null ? null : CreateHttpContent(content)
                };

                // Send it with the request
                return await Client.SendAsync(request);
            }
            catch
            {
                throw;
            }

            #endregion
        }

        /// <summary>
        /// Patches the specified <paramref name="content"/> at the specified <paramref name="url"/> and returns a <see cref="WebRequestResult{T}"/>
        /// </summary>
        /// <param name="url">The URL</param>
        /// <param name="content">The content to patch</param>
        /// <param name="authenticationArgs">If specified, provides the Authorization header is set</param>
        /// <returns></returns>
        public async Task<WebRequestResult<byte[]>> PatchBytesAsync(string url, object content, TAuthenticationArgs authenticationArgs)
        {
#if DEBUG
            if (ShouldLogRequests())
                Debug.WriteLine($"PATCH bytes request made to: {url}");
#endif

            // Create server response holder
            HttpResponseMessage serverResponse;
            try
            {
                // Make the standard PATCH call first
                serverResponse = await PatchAsync(url, content, authenticationArgs);
            }
            catch (Exception ex)
            {
                // If we got unexpected error, return that
                return new WebRequestResult<byte[]>(ex);
            }

            // Create the result
            var result = await serverResponse.CreateWebRequestResultFromStreamAsync();

            // If the response status code is not 200...
            if (!serverResponse.IsSuccessStatusCode)
            {
                // Call failed
                result.ErrorMessage = ParseErrorMessageCore(result);

                // Done
                return result;
            }

            // Return result
            return result;
        }

        #endregion

        #region DELETE

        /// <summary>
        /// Sends a DELETE request and returns a <see cref="HttpResponseMessage"/>
        /// </summary>
        /// <param name="url">The URL</param>
        /// <param name="authenticationArgs">If specified, provides the Authorization header is set</param>
        /// <returns></returns>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public async Task<HttpResponseMessage> DeleteAsync(string url, TAuthenticationArgs? authenticationArgs)
        {
            // Set the authorization scheme
            SetAuthorizationHeader(authenticationArgs);

            #region Send Request

#if DEBUG
            if (ShouldLogRequests())
                Debug.WriteLine($"DELETE request made to: {url}");
#endif

            try
            {
                // Send it with the request
                return await Client.DeleteAsync(url);
            }
            catch
            {
                // Re-throw
                throw;
            }

            #endregion
        }

        /// <summary>
        /// Sends a DELETE request and returns a <see cref="WebRequestResult{T}"/>
        /// </summary>
        /// <param name="url">The URL</param>
        /// <param name="authenticationArgs">If specified, provides the Authorization header is set</param>
        /// <returns></returns>
        public async Task<WebRequestResult<byte[]>> DeleteBytesAsync(string url, TAuthenticationArgs authenticationArgs)
        {
            // Create server response holder
            HttpResponseMessage serverResponse;
            try
            {
                // Make the standard DELETE call first
                serverResponse = await DeleteAsync(url, authenticationArgs);
            }
            catch (Exception ex)
            {
                // If we got unexpected error, return that
                return new WebRequestResult<byte[]>(ex);
            }

            // Create the result
            var result = await serverResponse.CreateWebRequestResultFromStreamAsync();

            // If the response status code is not 200...
            if (!serverResponse.IsSuccessStatusCode)
            {
                // Call failed
                result.ErrorMessage = ParseErrorMessageCore(result);

                // Done
                return result;
            }

            // If we have no content to deserialize...
            if (result.RawServerResponse.IsNullOrEmpty())
                // Done
                return result;

            // Return result
            return result;
        }


        #endregion

        #region HEAD

        /// <summary>
        /// Sends a head request to the specified <paramref name="url"/>
        /// </summary>
        /// <param name="url">The URL</param>
        /// <param name="authenticationArgs">If specified, provides the Authorization header is set</param>
        /// <returns></returns>
        public async Task<WebRequestResult<THeadersResult>> HeadAsync<THeadersResult>(string url, TAuthenticationArgs authenticationArgs)
            where THeadersResult : class, new()
        {
            // Set the authorization scheme
            SetAuthorizationHeader(authenticationArgs);

            #region Send Request

#if DEBUG
            if (ShouldLogRequests())
                Debug.WriteLine($"HEAD request made to: {url}");
#endif

            try
            {
                // Create the request
                var request = new HttpRequestMessage(HttpMethod.Head, url);

                // Send it with the request
                var response = await Client.SendAsync(request);

                return await response.CreateWebRequestResultFromHeadersAsync<THeadersResult>();
            }
            catch (Exception ex)
            {
                return new WebRequestResult<THeadersResult>() { ErrorMessage = ex.Message };
            }

            #endregion
        }

        #endregion

        #region OPTIONS

        /// <summary>
        /// Sends an options request to the specified <paramref name="url"/>
        /// </summary>
        /// <param name="url">The URL</param>
        /// <param name="authenticationArgs">If specified, provides the Authorization header is set</param>
        /// <returns></returns>
        public async Task<HttpResponseMessage> OptionsAsync(string url, TAuthenticationArgs authenticationArgs)
        {
            // Set the authorization scheme
            SetAuthorizationHeader(authenticationArgs);

            #region Send Request

#if DEBUG
            if (ShouldLogRequests())
                Debug.WriteLine($"OPTIONS request made to: {url}");
#endif

            try
            {
                // Create the request
                var request = new HttpRequestMessage(HttpMethod.Options, url);

                // Send it with the request
                return await Client.SendAsync(request);
            }
            catch
            {
                // Re-throw
                throw;
            }

            #endregion
        }

        #endregion

        #endregion

        #region Generic

        #region POST

        /// <summary>
        /// POSTs a web request to an URL that contains the specified <paramref name="content"/> and returns a <see cref="WebRequestResult{T}"/>
        /// </summary>
        /// <typeparam name="TResponse">The type of the response</typeparam>
        /// <param name="url">The URL</param>
        /// <param name="content">The content to post</param>
        /// <param name="authenticationArgs">If specified, provides the Authorization header is set</param>
        /// <returns></returns>
        public async Task<WebRequestResult<TResponse>> PostAsync<TResponse>(string url, object? content, TAuthenticationArgs? authenticationArgs)
        {
            // Create server response holder
            HttpResponseMessage serverResponse;
            try
            {
                // Make the standard Post call first
                serverResponse = await PostAsync(url, content, authenticationArgs);
            }
            catch (Exception ex)
            {
                // If we got unexpected error, return that
                return new WebRequestResult<TResponse>(ex);
            }

            // Create the result
            var result = await serverResponse.CreateWebRequestResultAsync<TResponse>();

            // If the response status code is not 200...
            if (!serverResponse.IsSuccessStatusCode)
            {
                // Call failed
                result.ErrorMessage = ParseErrorMessageCore(result);

                // Done
                return result;
            }

            // If we have no content to deserialize...
            if (result.RawServerResponse.IsNullOrEmpty())
                // Done
                return result;

            // Deserialize raw response
            try
            {
                // Deserialize Json string
                result.Result = Deserialize<TResponse>(result.RawServerResponse);
            }
            catch (Exception ex)
            {
                // Break
                Debugger.Break();

                // If deserialize failed then set error message
                result.ErrorMessage = ParseErrorMessageCore(result, typeof(TResponse), ex);

                // Done
                return result;
            }

            // Return result
            return result;
        }

        #endregion

        #region GET

        /// <summary>
        /// GETs a web request to a URL and returns a <see cref="WebRequestResult{T}"/>
        /// </summary>
        /// <param name="url">The URL</param>
        /// <param name="authenticationArgs">If specified, provides the Authorization header is set</param>
        /// <returns></returns>
        public async Task<WebRequestResult<TResponse>> GetAsync<TResponse>(string url, TAuthenticationArgs? authenticationArgs)
        {
            // Create server response holder
            HttpResponseMessage serverResponse;
            try
            {
                // Make the standard GET call first
                serverResponse = await GetAsync(url, authenticationArgs);
            }
            catch (Exception ex)
            {
                // If we got unexpected error, return that
                return new WebRequestResult<TResponse>(ex);
            }

            // Create the result
            var result = await serverResponse.CreateWebRequestResultAsync<TResponse>();

            // If the response status code is not 200...
            if (!serverResponse.IsSuccessStatusCode)
            {
                // Call failed
                result.ErrorMessage = ParseErrorMessageCore(result);

                // Done
                return result;
            }

            // If we have no content to deserialize...
            if (result.RawServerResponse.IsNullOrEmpty())
                // Done
                return result;

            // Deserialize raw response
            try
            {
                // Deserialize Json string
                result.Result = Deserialize<TResponse>(result.RawServerResponse);
            }
            catch (Exception ex)
            {
                // Break
                Debugger.Break();

                // If deserialize failed then set error message
                result.ErrorMessage = ParseErrorMessageCore(result, typeof(TResponse), ex);

                // Done
                return result;
            }

            // Return result
            return result;
        }

        #endregion

        #region PUT

        /// <summary>
        /// PUTs the specified <paramref name="content"/> at the specified <paramref name="url"/> and returns a <see cref="WebRequestResult{T}"/>
        /// </summary>
        /// <param name="url">The URL</param>
        /// <param name="content">The content to put</param>
        /// <param name="authenticationArgs">If specified, provides the Authorization header is set</param>
        /// <returns></returns>
        public async Task<WebRequestResult<TResponse>> PutAsync<TResponse>(string url, object? content, TAuthenticationArgs? authenticationArgs)
        {
            // Create server response holder
            HttpResponseMessage serverResponse;
            try
            {
                // Make the standard PUT call first
                serverResponse = await PutAsync(url, content, authenticationArgs);
            }
            catch (Exception ex)
            {
                // If we got unexpected error, return that
                return new WebRequestResult<TResponse>(ex);
            }

            // Create the result
            var result = await serverResponse.CreateWebRequestResultAsync<TResponse>();

            // If the response status code is not 200...
            if (!serverResponse.IsSuccessStatusCode)
            {
                // Call failed
                result.ErrorMessage = ParseErrorMessageCore(result);

                // Done
                return result;
            }

            // If we have no content to deserialize...
            if (result.RawServerResponse.IsNullOrEmpty())
                // Done
                return result;

            // Deserialize raw response
            try
            {
                // Deserialize Json string
                result.Result = Deserialize<TResponse>(result.RawServerResponse);
            }
            catch (Exception ex)
            {
                // Break
                Debugger.Break();

                // If deserialize failed then set error message
                result.ErrorMessage = ParseErrorMessageCore(result, typeof(TResponse), ex);

                // Done
                return result;
            }

            // Return result
            return result;
        }

        #endregion

        #region PATCH

        /// <summary>
        /// Patches the specified <paramref name="content"/> at the specified <paramref name="url"/> and returns a <see cref="WebRequestResult{T}"/>
        /// </summary>
        /// <param name="url">The URL</param>
        /// <param name="content">The content to patch</param>
        /// <param name="authenticationArgs">If specified, provides the Authorization header is set</param>
        /// <returns></returns>
        public async Task<WebRequestResult<TResponse>> PatchAsync<TResponse>(string url, object? content, TAuthenticationArgs authenticationArgs)
        {
            // Create server response holder
            HttpResponseMessage serverResponse;
            try
            {
                // Make the standard PATCH call first
                serverResponse = await PatchAsync(url, content, authenticationArgs);
            }
            catch (Exception ex)
            {
                // If we got unexpected error, return that
                return new WebRequestResult<TResponse>(ex);
            }

            // Create the result
            var result = await serverResponse.CreateWebRequestResultAsync<TResponse>();

            // If the response status code is not 200...
            if (!serverResponse.IsSuccessStatusCode)
            {
                // Call failed
                result.ErrorMessage = ParseErrorMessageCore(result);

                // Done
                return result;
            }

            // If we have no content to deserialize...
            if (result.RawServerResponse.IsNullOrEmpty())
                // Done
                return result;

            // Deserialize raw response
            try
            {
                // Deserialize Json string
                result.Result = Deserialize<TResponse>(result.RawServerResponse);
            }
            catch (Exception ex)
            {
                // Break
                Debugger.Break();

                // If deserialize failed then set error message
                result.ErrorMessage = ParseErrorMessageCore(result, typeof(TResponse), ex);

                // Done
                return result;
            }

            // Return result
            return result;
        }

        #endregion

        #region DELETE

        /// <summary>
        /// Sends a DELETE request and returns a <see cref="WebRequestResult{T}"/>
        /// </summary>
        /// <param name="url">The URL</param>
        /// <param name="authenticationArgs">If specified, provides the Authorization header is set</param>
        /// <returns></returns>
        public async Task<WebRequestResult<TResponse>> DeleteAsync<TResponse>(string url, TAuthenticationArgs? authenticationArgs)
        {
            // Create server response holder
            HttpResponseMessage serverResponse;
            try
            {
                // Make the standard DELETE call first
                serverResponse = await DeleteAsync(url, authenticationArgs);
            }
            catch (Exception ex)
            {
                // If we got unexpected error, return that
                return new WebRequestResult<TResponse>(ex);
            }

            // Create the result
            var result = await serverResponse.CreateWebRequestResultAsync<TResponse>();

            // If the response status code is not 200...
            if (!serverResponse.IsSuccessStatusCode)
            {
                // Call failed
                result.ErrorMessage = ParseErrorMessageCore(result);

                // Done
                return result;
            }

            // If we have no content to deserialize...
            if (result.RawServerResponse.IsNullOrEmpty())
                // Done
                return result;

            // Deserialize raw response
            try
            {
                // Deserialize Json string
                result.Result = Deserialize<TResponse>(result.RawServerResponse);
            }
            catch (Exception ex)
            {
                // Break
                Debugger.Break();

                // If deserialize failed then set error message
                result.ErrorMessage = ParseErrorMessageCore(result, typeof(TResponse), ex);

                // Done
                return result;
            }

            // Return result
            return result;
        }

        #endregion

        #region OPTIONS

        /// <summary>
        /// Sends an OPTIONS request and returns a <see cref="WebRequestResult{T}"/>
        /// </summary>
        /// <typeparam name="TResponse">The type of the response</typeparam>
        /// <param name="url">The URL</param>
        /// <param name="authenticationArgs">If specified, provides the Authorization header is set</param>
        /// <returns></returns>
        public async Task<WebRequestResult<TResponse>> OptionsAsync<TResponse>(string url, TAuthenticationArgs authenticationArgs)
        {
            // Create server response holder
            HttpResponseMessage serverResponse;
            try
            {
                // Make the standard OPTIONS call first
                serverResponse = await OptionsAsync(url, authenticationArgs);
            }
            catch (Exception ex)
            {
                // If we got unexpected error, return that
                return new WebRequestResult<TResponse>(ex);
            }

            // Create the result
            var result = await serverResponse.CreateWebRequestResultAsync<TResponse>();

            // If the response status code is not 200...
            if (!serverResponse.IsSuccessStatusCode)
            {
                // Call failed
                result.ErrorMessage = ParseErrorMessageCore(result);

                // Done
                return result;
            }

            // If we have no content to deserialize...
            if (result.RawServerResponse.IsNullOrEmpty())
                // Done
                return result;

            // Deserialize raw response
            try
            {
                // Deserialize Json string
                result.Result = Deserialize<TResponse>(result.RawServerResponse);
            }
            catch (Exception ex)
            {
                // Break
                Debugger.Break();

                // If deserialize failed then set error message
                result.ErrorMessage = ParseErrorMessageCore(result, typeof(TResponse), ex);

                // Done
                return result;
            }

            // Return result
            return result;
        }

        #endregion

        #endregion

        #endregion

        #region Protected Methods

        /// <summary>
        /// Gets a flag indicating whether the requests should be logged or not
        /// </summary>
        /// <returns></returns>
        protected virtual bool ShouldLogRequests() => true;

        /// <summary>
        /// Creates and returns an <see cref="HttpClient"/>
        /// </summary>
        /// <returns></returns>
        protected virtual HttpClient CreateClient() => new HttpClient();

        /// <summary>
        /// Configures the specified <paramref name="client"/>
        /// </summary>
        /// <param name="client">The client</param>
        protected virtual void ConfigureClient(HttpClient client) => client.Timeout = new TimeSpan(0, 30, 0);

        /// <summary>
        /// Configures the specified <paramref name="stringContent"/>
        /// </summary>
        /// <param name="stringContent">The content</param>
        protected virtual void ConfigureStringContent(StringContent stringContent) { }

        /// <summary>
        /// Serializes the specified <paramref name="obj"/> to a string
        /// before sending the request
        /// </summary>
        /// <param name="obj">The object</param>
        /// <returns></returns>
        protected virtual string Serialize(object obj) => JsonConvert.SerializeObject(obj);

        /// <summary>
        /// Deserializes the specified <paramref name="rawServerResponse"/> to the requested type
        /// </summary>
        /// <typeparam name="TResponse">The type to deserialize to</typeparam>
        /// <param name="rawServerResponse">The raw server response</param>
        /// <returns></returns>
        protected virtual TResponse Deserialize<TResponse>(string rawServerResponse)
        {
            // If the response is a string....
            if (typeof(TResponse) == typeof(string))
                return rawServerResponse.CastTo<TResponse>();

            // Deserialize the JSON
            var result = JsonConvert.DeserializeObject<TResponse>(rawServerResponse);

            // If the deserialization failed...
            if (result is null)
                throw new InvalidOperationException("Invalid response format");

            // Return the result
            return result;
        }

        /// <summary>
        /// Gets the media type header that is attached to the request before sending it
        /// </summary>
        /// <returns></returns>
        protected virtual string GetMediaType() => MediaTypeJson;

        /// <summary>
        /// Gets the encoding that is attached to the request before sending it
        /// </summary>
        /// <returns></returns>
        protected virtual Encoding GetEncoding() => Encoding.UTF8;

        /// <summary>
        /// Creates and returns a <see cref="AuthenticationHeaderValue"/> using the
        /// specified <paramref name="authenticationArgs"/>
        /// </summary>
        /// <param name="authenticationArgs">The authentication arguments</param>
        /// <returns></returns>
        protected abstract AuthenticationHeaderValue CreateAuthenticationHeader(TAuthenticationArgs authenticationArgs);

        /// <summary>
        /// Attempts to parse the error message contained in the <paramref name="result"/>
        /// </summary>
        /// <param name="result">The result</param>
        /// <returns></returns>
        protected virtual string? ParseErrorMessage(WebRequestResult result)
            => result.RawServerResponse;

        /// <summary>
        /// Attempts to parse the error message contained in the specified <paramref name="deserializationException"/>
        /// that was thrown when attempting to deserialize the <paramref name="result"/> to the specified <paramref name="deserializationType"/>
        /// </summary>
        /// <param name="deserializationType">The type that response should have been deserialized to</param>
        /// <param name="deserializationException">The deserialization exception that was thrown</param>
        /// <param name="result">The result</param>
        /// <returns></returns>
        protected virtual string? ParseDeserializationErrorMessage(WebRequestResult result, Type deserializationType, Exception deserializationException)
            => DeserializationErrorMessage;

        /// <summary>
        /// Handles the null arguments
        /// </summary>
        protected virtual void HandleNullAuthenticationArgs() => Client.DefaultRequestHeaders.Authorization = null;

        #endregion

        #region Private Methods

        /// <summary>
        /// Creates and returns the <see cref="StringContent"/> that represents
        /// the specified <paramref name="content"/>
        /// </summary>
        /// <param name="content">The content</param>
        /// <returns></returns>
        private StringContent CreateStringContent(object content)
        {
            // Create the HTTP content
            var httpContent = new StringContent(Serialize(content), GetEncoding(), GetMediaType());

            ConfigureStringContent(httpContent);

            // Return it
            return httpContent;
        }

        /// <summary>
        /// Creates the form-data, using the non null value in the <paramref name="content"/>
        /// </summary>
        /// <param name="content">The form-data content</param>
        /// <returns></returns>
        private MultipartFormDataContent CreateFormDataContent(object content)
        {
            // Declare a form-data
            var formDataContent = new MultipartFormDataContent();

            // For every property...
            foreach (var property in content.GetType().GetProperties())
            {
                // Get the name attribute
                var nameAttribute = property.GetCustomAttribute<NameAttribute>();

                // Get the key for the property
                var key = nameAttribute is null ? property.Name : nameAttribute.Name;

                // Get the property value
                var value = property.GetValue(content)?.ToString();

                // If the value is not null...
                if (value is not null)
                    // Add the row into the form
                    formDataContent.Add(new StringContent(value), key);
            }

            // Return the form-data
            return formDataContent;
        }

        /// <summary>
        /// Attempts to parse the error message contained in the <paramref name="response"/>
        /// </summary>
        /// <param name="response">The Web request result</param>
        /// <returns></returns>
        private string? ParseErrorMessageCore(WebRequestResult response)
        {
            try
            {
                return ParseErrorMessage(response);
            }
            catch
            {
                return DeserializationErrorMessage;
            }
        }

        /// <summary>
        /// Attempts to parse the error message contained in the specified <paramref name="deserializationException"/>
        /// that was thrown when attempting to deserialize the <paramref name="response"/> to the specified <paramref name="deserializationType"/>
        /// </summary>
        /// <param name="deserializationType">The type that response should have been deserialized to</param>
        /// <param name="deserializationException">The deserialization exception that was thrown</param>
        /// <param name="response">The Web request result</param>
        /// <returns></returns>
        private string? ParseErrorMessageCore(WebRequestResult response, Type deserializationType, Exception deserializationException)
        {
            try
            {
                return ParseDeserializationErrorMessage(response, deserializationType, deserializationException);
            }
            catch
            {
                return DeserializationErrorMessage;
            }
        }

        /// <summary>
        /// Creates the <paramref name="content"/> for 
        /// </summary>
        /// <param name="content">The request body content</param>
        /// <returns></returns>
        private HttpContent CreateHttpContent(object content)
        {
            // If the request body is form-data...
            if (RequestBodyType == HttpContentType.FormData)
                // Return the form-data content
                return CreateFormDataContent(content);

            // Return the JSON string content
            return CreateStringContent(content);
        }

        /// <summary>
        /// Sets the specified <paramref name="authentication"/> if any to the <see cref="Client"/>
        /// </summary>
        /// <param name="authentication">The token</param>
        private void SetAuthorizationHeader(TAuthenticationArgs? authentication)
        {
            // If there is a bearer token...
            if (authentication is not null)
                // Set it
                Client.DefaultRequestHeaders.Authorization = CreateAuthenticationHeader(authentication);
            else
                // Handle the null arguments
                HandleNullAuthenticationArgs();
        }

        #endregion
    }

    /// <summary>
    /// The JWT token based implementation of the <see cref="WebRequestsClient{TAuthenticationArgs}"/>
    /// </summary>
    public class WebRequestsClient : WebRequestsClient<string>
    {
        #region Private Members

        /// <summary>
        /// The member of the <see cref="Instance"/>
        /// </summary>
        private static Lazy<WebRequestsClient> mInstance = new(() => new());

        #endregion

        #region Public Properties

        #region Singleton

        /// <summary>
        /// A single instance of the <see cref="WebRequestsClient"/>
        /// </summary>
        public static WebRequestsClient Instance => mInstance.Value;

        #endregion

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        protected WebRequestsClient() : base()
        {

        }

        #endregion

        #region Protected Methods

        /// <summary>
        /// Creates and returns a <see cref="AuthenticationHeaderValue"/> using the
        /// specified <paramref name="authenticationArgs"/>
        /// </summary>
        /// <param name="authenticationArgs">The authentication arguments</param>
        /// <returns></returns>
        protected override AuthenticationHeaderValue CreateAuthenticationHeader(string authenticationArgs)
            => new AuthenticationHeaderValue("Bearer", authenticationArgs);

        #endregion
    }

    /// <summary>
    /// A web response from a call to get generic object data from a HTTP server
    /// </summary>
    public class WebRequestResult : IFailable
    {
        #region Constants

        /// <summary>
        /// The default headers
        /// </summary>
        public static HttpResponseHeaders DefaultHeaders = new HttpResponseMessage().Headers;

        /// <summary>
        /// The error message for the exception
        /// </summary>
        public const string ExceptionMessage = $"The flag {nameof(IsSuccessful)} has the be true in order to access the {nameof(Result)}.";

        #endregion

        #region Private Members

        /// <summary>
        /// The member of the <see cref="Exception"/> property
        /// </summary>
        private Exception? mException;

        /// <summary>
        /// The member of the <see cref="Result"/>
        /// </summary>
        private object mResult = default!;

        #endregion

        #region Public Properties

        /// <summary>
        /// The exception that was thrown
        /// </summary>
        public Exception? Exception
        {
            get => mException ?? (IsSuccessful ? null : new Exception(ErrorMessage));

            set => mException = value;
        }

        /// <summary>
        /// The error type
        /// </summary>
        public ErrorType ErrorType { get; set; } = ErrorType.Error;

        /// <summary>
        /// If something failed, this is the error message.
        /// </summary>
        public string? ErrorMessage { get; set; }

        /// <summary>
        /// A flag indicating whether the request was successful 
        /// </summary>
        [MemberNotNullWhen(false, nameof(ErrorMessage), nameof(Exception))]
        public virtual bool IsSuccessful => ErrorMessage is null;

        /// <summary>
        /// The status code
        /// </summary>
        public HttpStatusCode StatusCode { get; set; }

        /// <summary>
        /// The status description
        /// </summary>
        public string StatusDescription { get; set; } = string.Empty;

        /// <summary>
        /// The type of content returned by the server
        /// </summary>
        public string ContentType { get; set; } = string.Empty;

        /// <summary>
        /// All the response headers
        /// </summary>
        public HttpResponseHeaders Headers { get; set; } = DefaultHeaders;

        /// <summary>
        /// The raw server response body
        /// </summary>
        public string RawServerResponse { get; set; } = string.Empty;

        /// <summary>
        /// The actual server response as an object
        /// </summary>
        public object Result { get => IsSuccessful ? mResult : throw new InvalidOperationException(ExceptionMessage); set => mResult = value; }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public WebRequestResult() : base()
        {

        }

        /// <summary>
        /// Exception based constructor
        /// </summary>
        /// <param name="ex">The exception</param>
        public WebRequestResult(Exception ex) : base()
        {
            Exception = ex;
            ErrorMessage = ex.AggregateExceptionMessages();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Returns a string that represents the current object
        /// </summary>
        /// <returns></returns>
        public override string ToString() => IFailableHelpers.GetStringRepresentation(this);

        #endregion

        #region Operators

        /// <summary>
        /// Creates a <see cref="WebRequestResult"/> using the specified string as its error message
        /// </summary>
        /// <param name="s">The string</param>
        public static implicit operator WebRequestResult(string s) => new WebRequestResult(new Exception(s));

        /// <summary>
        /// Creates a <see cref="Failable"/> using the message of the specified ex and its inner exceptions
        /// as its error message
        /// </summary>
        /// <param name="ex">The exception</param>
        public static implicit operator WebRequestResult(Exception ex) => new WebRequestResult(ex);

        #endregion
    }

    /// <summary>
    /// A web response from a call to get specific data from a HTTP server
    /// </summary>
    /// <typeparam name="TResult">The type of data to deserialize the raw body into</typeparam>
    public class WebRequestResult<TResult> : WebRequestResult, IFailable<TResult>
    {
        #region Public Properties

        /// <summary>
        /// Casts the underlying object to the specified type
        /// </summary>
        public new TResult Result
        {
            get => (TResult)base.Result;
            set
            {
                if (value is not null)
                    base.Result = value;
            }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public WebRequestResult() : base()
        {

        }

        /// <summary>
        /// Result based constructor
        /// </summary>
        /// <param name="result">The result</param>
        public WebRequestResult(TResult result)
        {
            Result = result;
        }

        /// <summary>
        /// Exception based constructor
        /// </summary>
        /// <param name="ex">The exception</param>
        public WebRequestResult(Exception ex) : base(ex)
        {

        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Creates and returns a <see cref="WebRequestResult{T}"/> from the current object
        /// </summary>
        /// <typeparam name="T">The type of the failable</typeparam>
        /// <param name="customErrorMessage">The custom error message</param>
        /// <returns></returns>
        public WebRequestResult<T> ToUnsuccessfulWebRequestResult<T>(string? customErrorMessage = null)
            => new WebRequestResult<T>()
            {
                ErrorMessage = customErrorMessage ?? ErrorMessage,
                ErrorType = ErrorType,
                Exception = Exception,
                ContentType = ContentType,
                Headers = Headers,
                RawServerResponse = RawServerResponse,
                StatusCode = StatusCode,
                StatusDescription = StatusDescription
            };

        /// <summary>
        /// Creates and returns a <see cref="WebRequestResult{T}"/> from the current object
        /// </summary>
        /// <typeparam name="T">The type of the failable</typeparam>
        /// <param name="valueConverter">The method that converts an instance of type <typeparamref name="TResult"/> to <typeparamref name="T"/></param>
        /// <returns></returns>
        public WebRequestResult<T> ToSuccessfulWebRequestResult<T>(Func<TResult, T> valueConverter)
            => new WebRequestResult<T>()
            {
                Result = valueConverter(Result),
                ContentType = ContentType,
                Headers = Headers,
                RawServerResponse = RawServerResponse,
                StatusCode = StatusCode,
                StatusDescription = StatusDescription
            };

        /// <summary>
        /// Returns a string that represents the current object
        /// </summary>
        /// <returns></returns>
        public override string ToString() => IFailableHelpers.GetStringRepresentation(this);

        #endregion

        #region Operators

        /// <summary>
        /// Creates a <see cref="WebRequestResult{TResult}"/> using the specified string as its error message
        /// </summary>
        /// <param name="s">The string</param>
        public static implicit operator WebRequestResult<TResult>(string s) => new WebRequestResult<TResult>(new Exception(s));

        /// <summary>
        /// Creates a <see cref="WebRequestResult{TResult}"/> using the message of the specified ex and its inner exceptions
        /// as its error message
        /// </summary>
        /// <param name="ex">The exception</param>
        public static implicit operator WebRequestResult<TResult>(Exception ex) => new WebRequestResult<TResult>(ex);

        /// <summary>
        /// Creates a <see cref="Failable{TResult}"/> using the specified result as its result
        /// </summary>
        /// <param name="result">The result</param>
        public static implicit operator WebRequestResult<TResult>(TResult result) => new WebRequestResult<TResult>(result);

        /// <summary>
        /// Converts a <see cref="WebRequestResult{TResult}"/> to a <see cref="Failable{TResult}"/>
        /// </summary>
        /// <param name="result">The data storage result</param>
        public static implicit operator Failable<TResult>(WebRequestResult<TResult> result)
            => new Failable<TResult>()
            {
                Result = result.Result,
                Exception = result.Exception,
                ErrorMessage = result.ErrorMessage,
                ErrorType = result.ErrorType
            };

        #endregion
    }

    /// <summary>
    /// Extension methods for <see cref="HttpWebResponse"/>
    /// </summary>
    public static class HttpResponceMessageExtensions
    {
        #region Public Methods

        /// <summary>
        /// Returns a <see cref="WebRequestResult"/> pre-populated with the <see cref="HttpWebResponse"/> information
        /// </summary>
        /// <param name="serverResponse">The server response</param>
        /// <returns></returns>
        public static async Task<WebRequestResult> CreateWebRequestResultAsync(this HttpResponseMessage serverResponse)
        {
            // Create a result
            var result = new WebRequestResult
            {
                // Status code
                StatusCode = serverResponse.StatusCode,

                // Status description
                StatusDescription = serverResponse.ReasonPhrase.ToNonNullString(),

                // The headers
                Headers = serverResponse.Headers,

                // The raw server response
                RawServerResponse = await serverResponse.Content.ReadAsStringAsync()
            };

            if (!serverResponse.IsSuccessStatusCode)
                result.ErrorMessage = result.RawServerResponse;

            return result;
        }

        /// <summary>
        /// Returns a <see cref="WebRequestResult{T}"/> pre-populated with the <see cref="HttpWebResponse"/> information
        /// read as stream
        /// </summary>
        /// <param name="serverResponse">The server response</param>
        /// <returns></returns>
        public static async Task<WebRequestResult<byte[]>> CreateWebRequestResultFromStreamAsync(this HttpResponseMessage serverResponse)
        {
            var result = new WebRequestResult<byte[]>
            {
                StatusCode = serverResponse.StatusCode,
                StatusDescription = serverResponse.ReasonPhrase.ToNonNullString(),
                Headers = serverResponse.Headers,
                RawServerResponse = await serverResponse.Content.ReadAsStringAsync(),
                Result = await serverResponse.Content.ReadAsByteArrayAsync()
            };

            if (!serverResponse.IsSuccessStatusCode)
                result.ErrorMessage = result.RawServerResponse;

            return result;
        }

        #endregion

        #region Internal Methods

        /// <summary>
        /// Returns a <see cref="WebRequestResult{T}"/> pre-populated with the <see cref="HttpWebResponse"/> information
        /// </summary>
        /// <typeparam name="TResponse">The type of response to create</typeparam>
        /// <param name="serverResponse">The server response</param>
        /// <returns></returns>
        internal static async Task<WebRequestResult<TResponse>> CreateWebRequestResultAsync<TResponse>(this HttpResponseMessage serverResponse)
        {
            var result = new WebRequestResult<TResponse>
            {
                // Status code
                StatusCode = serverResponse.StatusCode,

                // Status description
                StatusDescription = serverResponse.ReasonPhrase.ToNonNullString(),

                // The headers
                Headers = serverResponse.Headers,

                // The raw server response
                RawServerResponse = await serverResponse.Content.ReadAsStringAsync()
            };

            if (!serverResponse.IsSuccessStatusCode)
                result.ErrorMessage = result.RawServerResponse;

            return result;
        }

        /// <summary>
        /// Creates and returns a <see cref="WebRequestResult{T}"/> using the headers of the specified <paramref name="serverResponse"/>
        /// </summary>
        /// <typeparam name="THeadersResult">The type of the header result</typeparam>
        /// <param name="serverResponse">The server response</param>
        /// <returns></returns>
        public static async Task<WebRequestResult<THeadersResult>> CreateWebRequestResultFromHeadersAsync<THeadersResult>(this HttpResponseMessage serverResponse)
            where THeadersResult : class, new()
        {
            // Create the result
            var result = new WebRequestResult<THeadersResult>
            {
                // Status code
                StatusCode = serverResponse.StatusCode,

                // Status description
                StatusDescription = serverResponse.ReasonPhrase.ToNonNullString(),

                // The headers
                Headers = serverResponse.Headers,

                // The raw server response
                RawServerResponse = await serverResponse.Content.ReadAsStringAsync()
            };

            if (!serverResponse.IsSuccessStatusCode)
            {
                result.ErrorMessage = result.RawServerResponse;

                return result;
            }

            // Create the headers result
            var headersResult = new THeadersResult();

            // For every property...
            foreach (var property in typeof(THeadersResult).GetProperties().Where(x => x.CanWrite))
            {
                // If there isn't a header...
                if (!serverResponse.Headers.Any(x => x.Key.ToLower() == property.Name.ToLower()))
                    // Continue
                    continue;

                // Get the header
                var header = serverResponse.Headers.First(x => x.Key.ToLower() == property.Name.ToLower());

                // Get the value
                var value = header.Value?.FirstOrDefault();

                // If there isn't a value...
                if (value is null)
                    // Continue
                    continue;

                // If it's a string...
                if (property.PropertyType == typeof(string))
                    // Set it
                    property.SetValue(headersResult, value);
                // If it's a number...
                else if (property.PropertyType.IsNumber())
                    // Set it
                    property.SetValue(headersResult, TypeHelpers.ConvertNumber(value.ToNullableDouble(), property.PropertyType));
                // If it's a date time offset...
                else if (property.PropertyType == typeof(DateTimeOffset) || property.PropertyType == typeof(DateTimeOffset?))
                    // Set it
                    property.SetValue(headersResult, HeaderConstants.StringToDateTimeOffset(value));
            }

            // Set the headers result
            result.Result = headersResult;

            // Return the result
            return result;
        }

        #endregion
    }

    /// <summary>
    /// Constants related to headers
    /// </summary>
    public static class HeaderConstants
    {
        /// <summary>
        /// The count
        /// </summary>
        public const string Count = "Count";

        /// <summary>
        /// The minimum date
        /// </summary>
        public const string MinDate = "MinDate";

        /// <summary>
        /// The maximum date
        /// </summary>
        public const string MaxDate = "MaxDate";

        /// <summary>
        /// Parses the specified <paramref name="dateTimeOffset"/> to a string
        /// </summary>
        /// <param name="dateTimeOffset">The date time</param>
        /// <returns></returns>
        public static string DateTimeOffsetToString(DateTimeOffset dateTimeOffset) => dateTimeOffset.ToString("yyyyMMddHHmmssK");

        /// <summary>
        /// Parses the specified <paramref name="s"/> to a date time 
        /// </summary>
        /// <param name="s">The string</param>
        /// <returns></returns>
        public static DateTimeOffset StringToDateTimeOffset(string s) => DateTimeOffset.ParseExact(s, "yyyyMMddHHmmssK", LocalizationConstants.Culture);
    }

    /// <summary>
    /// Provides enumeration for the HTTP request content type
    /// </summary>
    public enum HttpContentType
    {
        /// <summary>
        /// The raw Json body option
        /// </summary>
        JsonBody,
        /// <summary>
        /// The form-data body option
        /// </summary>
        FormData,
    }

    /// <summary>
    /// Helper methods related to web routes
    /// </summary>
    public static class RouteHelpers
    {
        #region Private Members

        /// <summary>
        /// Maps a type to its query arguments
        /// </summary>
        private static readonly Dictionary<Type, List<PropertyQueryArgumentInfo>> mTypeToQueryArgumentsMapper = new();

        #endregion

        #region Public Methods

        /// <summary>
        /// Attaches the specified <paramref name="parameters"/> to the specified <paramref name="url"/>
        /// </summary>
        /// <param name="url">The URL</param>
        /// <param name="parameters">The parameters</param>
        /// <returns></returns>
        public static string AttachStringParameters(string url, params QueryArgument[] parameters)
        {
            if (parameters.IsNullOrEmpty())
                return url;

            foreach (var parameter in parameters)
                if (!parameter.Value.IsNullOrEmpty())
                    url = QueryHelpers.AddQueryString(url, parameter.Name, parameter.Value);

            return url;
        }

        /// <summary>
        /// Attaches the parameters that are specified as properties in the <paramref name="args"/>
        /// to the specified <paramref name="url"/>.
        /// NOTE: A property can be marked as ignorable using the <see cref="IgnoreAttribute"/>!
        /// NOTE: A custom property name can be set using the <see cref="NameAttribute"/>!
        /// </summary>
        /// <param name="url">The URL</param>
        /// <param name="args">The arguments</param>
        /// <returns></returns>
        public static string AttachParameters<TArgs>(string url, TArgs? args)
            where TArgs : class, new()
        {
            // Instantiate The arguments if they are not set.
            // NOTE: Null object pattern!
            args = args ?? new TArgs();

            // Return the URL
            return AttachParametersCore(url, args, typeof(TArgs));
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Attaches the parameters that are specified as properties in the <paramref name="args"/>
        /// to the specified <paramref name="url"/>.
        /// NOTE: A property can be marked as ignorable using the <see cref="IgnoreAttribute"/>!
        /// NOTE: A custom property name can be set using the <see cref="NameAttribute"/>!
        /// </summary>
        /// <param name="url">The URL</param>
        /// <param name="args">The arguments</param>
        /// <param name="argumentType">The type of the argument</param>
        /// <returns></returns>
        private static string AttachParametersCore(string url, object args, Type argumentType)
        {
            // If this is the first time the specified arguments are used...
            if (!mTypeToQueryArgumentsMapper.TryGetValue(argumentType, out var argumentInfos))
            {
                // Initialize the collection
                argumentInfos = new List<PropertyQueryArgumentInfo>();

                // For every property...
                foreach (var property in argumentType.GetProperties())
                {
                    // Get the custom attributes
                    var attributes = property.GetCustomAttributes(true);

                    // If the property is marked as ignorable...
                    if (attributes.Any(x => x.GetType() == typeof(IgnoreAttribute)))
                        // Continue
                        continue;

                    // Get the argument name
                    var argumentName = attributes?.OfType<NameAttribute>().FirstOrDefault()?.Name ?? property.Name.ToLower();

                    // Create and add the argument info
                    argumentInfos.Add(new PropertyQueryArgumentInfo(property, argumentName));
                }

                // Map it
                mTypeToQueryArgumentsMapper.Add(argumentType, argumentInfos);
            }

            // For every argument info...
            foreach (var argumentInfo in argumentInfos)
            {
                // Get the value
                var value = argumentInfo.PropertyInfo.GetValue(args);

                // If there isn't a value...
                if (value is null)
                    // Continue
                    continue;

                // Get the property type
                var propertyType = TypeHelpers.GetNonNullableType(argumentInfo.PropertyInfo.PropertyType);

                // If it's an enumerable...
                if (argumentInfo.IsEnumerable)
                {
                    // Get the enumerable
                    var enumerable = new List<object>();

                    // For every enumerable value...
                    foreach (var v in (IEnumerable)value)
                        // Add it to the list
                        enumerable.Add(v);

                    // If there aren't any items
                    if (enumerable.IsNullOrEmpty())
                        // Continue
                        continue;

                    // Set the values
                    url = QueryHelpers.AddQueryString(url, argumentInfo.Name, enumerable.AggregateString(","));

                    // Continue
                    continue;
                }

                // If the type is complex type...
                if (propertyType.IsComplexType())
                {
                    // Get the parameters fort the nested type
                    url = AttachParametersCore(url, value, propertyType);

                    // Continue
                    continue;
                }

                // If it's a date...
                if (argumentInfo.IsDate)
                {
                    var isoStringValue = string.Empty;

                    if (propertyType == typeof(DateTime))
                        isoStringValue = ((DateTime)value).ToISO8601String();
                    else if (propertyType == typeof(DateTime?))
                        isoStringValue = ((DateTime?)value).Value.ToISO8601String();
                    else if (propertyType == typeof(DateTimeOffset))
                        isoStringValue = ((DateTimeOffset)value).ToISO8601String();
                    else if (propertyType == typeof(DateTimeOffset?))
                        isoStringValue = ((DateTimeOffset?)value).Value.ToISO8601String();
                    else if (propertyType == typeof(DateOnly))
                        isoStringValue = ((DateOnly)value).ToISO8601String();
                    else if (propertyType == typeof(DateOnly?))
                        isoStringValue = ((DateOnly?)value).Value.ToISO8601String();

                    url = QueryHelpers.AddQueryString(url, argumentInfo.Name, isoStringValue);

                    // Continue
                    continue;
                }

                // If it's a time...
                if (argumentInfo.PropertyInfo.PropertyType == typeof(TimeOnly) || argumentInfo.PropertyInfo.PropertyType == typeof(TimeOnly?))
                {
                    var isoStringValue = string.Empty;

                    if (propertyType == typeof(TimeOnly))
                        isoStringValue = ((TimeOnly)value).ToISO8601String();
                    else if (propertyType == typeof(TimeOnly?))
                        isoStringValue = ((TimeOnly?)value).Value.ToISO8601String();

                    url = QueryHelpers.AddQueryString(url, argumentInfo.Name, isoStringValue);

                    // Continue
                    continue;
                }

                // Get the argument value
                var argumentValue = value.ToNonNullString();

                // If the property type is boolean...
                if (propertyType == typeof(bool))
                    // Lower the first letter of the value
                    argumentValue = StringHelpers.FirstCharToLower(argumentValue);

                // If the property type is an enum and there is a value
                if (propertyType.IsEnum && value is not null)
                {
                    // Get the related member info
                    var memberInfo = propertyType.GetMember(argumentValue)
                        .First();

                    // Get the name attribute
                    var nameAttribute = memberInfo.GetCustomAttribute<NameAttribute>();

                    // If the there is the name attribute...
                    if (nameAttribute != null)
                        // Get the argument name
                        argumentValue = nameAttribute.Name;

                    url = QueryHelpers.AddQueryString(url, argumentInfo.Name, argumentValue);

                    // Continue
                    continue;
                }

                // Set the value
                url = QueryHelpers.AddQueryString(url, argumentInfo.Name, argumentValue);
            }

            // Return the URL
            return url;
        }

        #endregion

        #region Private Classes

        /// <summary>
        /// The information for the query argument that is related to a property
        /// </summary>
        private class PropertyQueryArgumentInfo
        {
            #region Public Properties

            /// <summary>
            /// The property
            /// </summary>
            public PropertyInfo PropertyInfo { get; }

            /// <summary>
            /// The name
            /// </summary>
            public string Name { get; }

            /// <summary>
            /// A flag indicating whether the <see cref="PropertyInfo"/> is a date property
            /// </summary>
            public bool IsDate { get; }

            /// <summary>
            /// A flag indicating whether the <see cref="PropertyInfo"/> is an enumerable property
            /// </summary>
            public bool IsEnumerable { get; }

            #endregion

            #region Constructors

            /// <summary>
            /// Default constructor
            /// </summary>
            /// <param name="name">The name</param>
            /// <param name="propertyInfo">A flag indicating whether the <see cref="PropertyInfo"/> is an enumerable property</param>
            public PropertyQueryArgumentInfo(PropertyInfo propertyInfo, string name) : base()
            {
                PropertyInfo = propertyInfo ?? throw new ArgumentNullException(nameof(propertyInfo));
                Name = name.NotNullOrEmpty();

                IsDate = propertyInfo.PropertyType.IsDate();
                IsEnumerable = propertyInfo.PropertyType != typeof(string) && propertyInfo.PropertyType.IsEnumerable();
            }

            #endregion

            #region Public Methods

            /// <summary>
            /// Returns a string that represents the current object.
            /// </summary>
            /// <returns></returns>
            public override string ToString() => Name;

            #endregion
        }

        #endregion
    }

    /// <summary>
    /// Represents a query parameter
    /// </summary>
    public class QueryArgument
    {
        #region Public Properties

        /// <summary>
        /// The name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The value
        /// </summary>
        public string? Value { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="name">The name</param>
        /// <param name="value">The value</param>
        public QueryArgument(string name, string? value) : base()
        {
            Name = name.NotNullOrEmpty();
            Value = value;
        }

        #endregion
    }
}
