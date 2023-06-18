using Amazon.Runtime;
using AppointMate;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using MongoDB.Bson;

using System;

namespace AppointMate
{
    /// <summary>
    /// An implementation of the <see cref="IFailable"/> used by the services of the web server
    /// </summary>
    public class WebServerFailable : IFailable
    {
        #region Private Members

        /// <summary>
        /// The member of the <see cref="Exception"/> property
        /// </summary>
        private Exception? mException;

        /// <summary>
        /// The member of the <see cref="ErrorMessage"/> property
        /// </summary>
        private string? mErrorMessage;

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
        /// The error message
        /// </summary>
        public string ErrorMessage
        {
            get => mErrorMessage ?? string.Empty;
            set => mErrorMessage = value;
        }

        /// <summary>
        /// A flag indicating whether the task was successful or not
        /// </summary>
        public bool IsSuccessful => ErrorMessage.IsNullOrEmpty();

        /// <summary>
        /// The status code
        /// </summary>
        public virtual int? StatusCode { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public WebServerFailable() : base()
        {

        }

        /// <summary>
        /// Exception based constructor
        /// </summary>
        /// <param name="ex">The exception</param>
        public WebServerFailable(Exception ex) : base()
        {
            Exception = ex;
            ErrorMessage = ex.AggregateExceptionMessages();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Creates and returns a <see cref="WebServerFailable{TResult}"/> that
        /// represents the specified <paramref name="result"/>
        /// </summary>
        /// <typeparam name="T">The type of the result</typeparam>
        /// <param name="result">The result</param>
        /// <returns></returns>
        public static WebServerFailable<T> FromResult<T>(T result)
            => new WebServerFailable<T>(result);

        /// <summary>
        /// Creates and returns a <see cref="WebServerFailable{TResult}"/> indicating
        /// that a resource was not found
        /// </summary>
        /// <param name="itemId">The id of the item that was searched</param>
        /// <param name="itemsName">The name of the items</param>
        /// <returns></returns>
        public static UnsuccessfulWebServerFailable NotFound(string itemId, string itemsName)
            => new UnsuccessfulWebServerFailable()
            {
                ErrorMessage =
                    $"Error: Resource not found!" + Environment.NewLine +
                    $"Id: {itemId}" + Environment.NewLine +
                    $"Collection: {itemsName}"
            };

        /// <summary>
        /// Creates and returns a <see cref="WebServerFailable{TResult}"/> indicating
        /// that a resource was not found
        /// </summary>
        /// <param name="itemId">The id of the item that was searched</param>
        /// <param name="itemsName">The name of the items</param>
        /// <returns></returns>
        public static UnsuccessfulWebServerFailable NotFound(ObjectId itemId, string itemsName)
            => NotFound(itemId.ToString(), itemsName);

        /// <summary>
        /// Returns a string that represents the current object
        /// </summary>
        /// <returns></returns>
        public override string ToString() => IFailableHelpers.GetStringRepresentation(this);

        #endregion

        #region Operators

        /// <summary>
        /// Creates a <see cref="WebServerFailable"/> using the specified string as its error message
        /// </summary>
        /// <param name="s">The string</param>
        public static implicit operator WebServerFailable(string s) => new WebServerFailable(new Exception(s));

        /// <summary>
        /// Creates a <see cref="WebServerFailable"/> using the message of the specified ex and its inner exceptions
        /// as its error message
        /// </summary>
        /// <param name="ex">The exception</param>
        public static implicit operator WebServerFailable(Exception ex) => new WebServerFailable(ex);

        #endregion

        #region Public Classes

        /// <summary>
        /// A <see cref="WebServerFailable"/> used when the operation was unsuccessful
        /// </summary>
        public class UnsuccessfulWebServerFailable : WebServerFailable
        {
            #region Constructors

            /// <summary>
            /// Default constructor
            /// </summary>
            public UnsuccessfulWebServerFailable() : base()
            {

            }

            #endregion
        }

        #endregion
    }

    /// <summary>
    /// An implementation of the <see cref="IFailable{TResult}"/> used by the web server services
    /// </summary>
    /// <typeparam name="TResult">The type of the result</typeparam>
    public class WebServerFailable<TResult> : WebServerFailable, IFailable<TResult?>
    {
        #region Public Properties

        /// <summary>
        /// The result
        /// </summary>
        public TResult? Result { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public WebServerFailable() : base()
        {

        }

        /// <summary>
        /// Result based constructor
        /// </summary>
        /// <param name="result">The result</param>
        public WebServerFailable(TResult result)
        {
            Result = result;
        }

        /// <summary>
        /// Exception based constructor
        /// </summary>
        /// <param name="ex">The exception</param>
        public WebServerFailable(Exception ex) : base(ex)
        {
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Returns a string that represents the current object
        /// </summary>
        /// <returns></returns>
        public override string ToString() => IFailableHelpers.GetStringRepresentation(this);

        /// <summary>
        /// Creates and returns an <see cref="ObjectResult"/> that represents the 
        /// current <see cref="WebServerFailable"/>
        /// </summary>
        /// <returns></returns>
        public ObjectResult ToObjectResult()
        {
            if (!IsSuccessful)
                return new ObjectResult(ErrorMessage)
                {
                    StatusCode = StatusCode ?? StatusCodes.Status400BadRequest
                };

            return new ObjectResult(Result)
            {
                StatusCode = StatusCode ?? StatusCodes.Status200OK
            };
        }

        #endregion

        #region Operators

        /// <summary>
        /// Creates a <see cref="WebServerFailable{TResult}"/> using the specified string as its error message
        /// </summary>
        /// <param name="s">The string</param>
        public static implicit operator WebServerFailable<TResult>(string s) => new WebServerFailable<TResult>(new Exception(s));

        /// <summary>
        /// Creates a <see cref="WebServerFailable{TResult}"/> using the message of the specified ex and its inner exceptions
        /// as its error message
        /// </summary>
        /// <param name="ex">The exception</param>
        public static implicit operator WebServerFailable<TResult>(Exception ex) => new WebServerFailable<TResult>(ex);

        /// <summary>
        /// Creates a <see cref="WebServerFailable{TResult}"/> using the specified result as its result
        /// </summary>
        /// <param name="result">The result</param>
        public static implicit operator WebServerFailable<TResult>(TResult result) => new WebServerFailable<TResult>(result);

        /// <summary>
        /// Creates a <see cref="WebServerFailable{TResult}"/> from the specified <paramref name="failable"/>
        /// </summary>
        public static implicit operator WebServerFailable<TResult>(UnsuccessfulWebServerFailable failable) => new WebServerFailable<TResult>()
        {
            ErrorMessage = failable.ErrorMessage,
            ErrorType = failable.ErrorType,
            Exception = failable.Exception,
            StatusCode = failable.StatusCode
        };

        /// <summary>
        /// Creates an <see cref="ActionResult{TValue}"/> using the specified <paramref name="result"/>
        /// </summary>
        /// <param name="result">The result</param>
        public static implicit operator ActionResult<TResult>(WebServerFailable<TResult> result)
        {
            return result.ToObjectResult();
        }

        #endregion
    }
}
