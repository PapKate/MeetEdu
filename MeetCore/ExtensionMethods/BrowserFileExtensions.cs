using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.AspNetCore.Http;

namespace MeetCore
{
    /// <summary>
    /// Extension methods for <see cref="IBrowserFile"/>s
    /// </summary>
    public static class BrowserFileExtensions
    {
        /// <summary>
        /// Converts the specified <paramref name="file"/> to an <see cref="IFormFile"/>
        /// </summary>
        /// <param name="file">The file</param>
        /// <param name="name">The name</param>
        /// <returns></returns>
        public static Task<IFormFile> ToIFormFileAsync(this IBrowserFile file, string name)
        {
            return file.ToIFormFileAsync(name, new HeaderDictionary());
        }

        /// <summary>
        /// Converts the specified <paramref name="file"/> to an <see cref="IFormFile"/>
        /// </summary>
        /// <param name="file">The file</param>
        /// <param name="name">The name</param>
        /// <param name="headers">The headers</param>
        /// <returns></returns>
        public static async Task<IFormFile> ToIFormFileAsync(this IBrowserFile file, string name, HeaderDictionary headers)
        {
            using var stream = file.OpenReadStream(512000000);
            var memoryStream = new MemoryStream();
            await stream.CopyToAsync(memoryStream);

            return new FormFile(memoryStream, 0, stream.Length, null, name)
            {
                Headers = headers,
                ContentType = "image/jpeg"
            };
        }
    }
}
