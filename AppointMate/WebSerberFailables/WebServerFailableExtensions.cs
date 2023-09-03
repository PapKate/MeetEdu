using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using System;

using static MeetEdu.WebServerFailable;

namespace MeetEdu
{
    /// <summary>
    /// Extension methods for <see cref="WebServerFailable{TResult}"/>
    /// </summary>
    public static class WebServerFailableExtensions
    {
        /// <summary>
        /// Creates and returns a <see cref="ActionResult"/> that represents the specified <paramref name="failable"/>
        /// </summary>
        /// <typeparam name="TResult">The initial type of the result of the failable</typeparam>
        /// <typeparam name="TOtherResult">The projected type of the result of the failable</typeparam>
        /// <param name="failable">The failable</param>
        /// <param name="projector">The projector</param>
        /// <returns></returns>
        public static ActionResult<TOtherResult> ToActionResult<TResult, TOtherResult>(this WebServerFailable<TResult> failable, Func<TResult, TOtherResult> projector)
        {
            if (!failable.IsSuccessful || failable.Result is null)
                return failable.ToObjectResult();

            return new ObjectResult(projector(failable.Result))
            {
                StatusCode = failable.StatusCode ?? StatusCodes.Status200OK
            };
        }

        /// <summary>
        /// Creates and returns a <see cref="UnsuccessfulWebServerFailable"/> from the <paramref name="failable"/>
        /// </summary>
        /// <param name="failable">The failable</param>
        /// <param name="customErrorMessage">The custom error message</param>
        /// <param name="customStatusCode">The custom status code</param>
        /// <returns></returns>
        public static UnsuccessfulWebServerFailable ToUnsuccessfulWebServerFailable(this WebServerFailable failable, string? customErrorMessage = null, int? customStatusCode = null)
            => new UnsuccessfulWebServerFailable()
            {
                ErrorMessage = customErrorMessage ?? failable.ErrorMessage,
                ErrorType = failable.ErrorType,
                Exception = failable.Exception,
                StatusCode = customStatusCode ?? failable.StatusCode
            };
    }
}
