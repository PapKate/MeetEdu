using Microsoft.JSInterop;

namespace MeetBase.Blazor
{
    /// <summary>
    /// Manages the browser session storage
    /// </summary>
    public class SessionStorageManager : IAsyncDisposable
    {
        #region Constants

        public const string DepartmentId = "Department Id";
        public const string IsSecretary = "Is secretary";
        public const string UserId = "User Id";
        public const string SecretaryId = "Secretary Id";
        public const string ProfessorId = "Professor Id";

        #endregion

        #region Private Members

        private Lazy<IJSObjectReference> _accessorJsRef = new();
        private readonly IJSRuntime _jsRuntime;

        #endregion
        
        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public SessionStorageManager(IJSRuntime jsRuntime) : base()
        {
            _jsRuntime = jsRuntime;
        }

        #endregion

        #region Public Methods
        /// <summary>
        /// Gets the value of the specified <paramref name="key"/>
        /// </summary>
        /// <typeparam name="T">The type of the value</typeparam>
        /// <param name="key">The pair key</param>
        /// <returns></returns>
        public async Task<T> GetValueAsync<T>(string key)
        {
            await WaitForReference();
            var result = await _accessorJsRef.Value.InvokeAsync<string>("get", key);
            return (T)Convert.ChangeType(result, typeof(T));
        }

        /// <summary>
        /// Creates a new key-value pair in the storage
        /// </summary>
        /// <typeparam name="T">The value type</typeparam>
        /// <param name="key">The pair key</param>
        /// <param name="value">The pair value</param>
        /// <returns></returns>
        public async Task SetValueAsync<T>(string key, T value)
        {
            await WaitForReference();
            await _accessorJsRef.Value.InvokeVoidAsync("set", key, value);
        }

        /// <summary>
        /// Removes the pair with the specified <paramref name="key"/>
        /// </summary>
        /// <param name="key">The pair key</param>
        /// <returns></returns>
        public async Task RemoveAsync(string key)
        {
            await WaitForReference();
            await _accessorJsRef.Value.InvokeVoidAsync("remove", key);
        }

        /// <summary>
        /// Clears the session storage
        /// </summary>
        /// <returns></returns>
        public async Task ClearAsync()
        {
            await WaitForReference();
            await _accessorJsRef.Value.InvokeVoidAsync("clear");
        }

        /// <inheritdoc/>
        public async ValueTask DisposeAsync()
        {
            if (_accessorJsRef.IsValueCreated)
            {
                await _accessorJsRef.Value.DisposeAsync();
            }
        }

        #endregion
        
        #region Private Methods

        /// <summary>
        /// Imports the JS file
        /// </summary>
        /// <returns></returns>
        private async Task WaitForReference()
        {
            if (_accessorJsRef.IsValueCreated is false)
            {
                _accessorJsRef = new(await _jsRuntime.InvokeAsync<IJSObjectReference>("import", "./_content/MeetBase.Blazor/js/sessionStorageFunctions.js"));
            }
        }

        #endregion
    }
}
