using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.AspNetCore.Http;

namespace MeetCore
{
    /// <summary>
    /// 
    /// </summary>
    public static class BrowserFileExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="file"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static async Task<IFormFile> ToIFormFileAsync(this IBrowserFile file, string name)
        {
            using var stream = file.OpenReadStream(512000000);
            var memoryStream = new MemoryStream();
            await stream.CopyToAsync(memoryStream);

            return new FormFile(memoryStream, 0, stream.Length, null, name)
            {
                Headers = new HeaderDictionary(),
                ContentType = "image/jpeg"
            };
        }
    }
}
