using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;

using System.Drawing;

namespace MeetBase.Blazor
{
    /// <summary>
    /// Helper methods for the controls
    /// </summary>
    public static class ControlHelpers
    {
        #region Placement

        /// <summary>
        /// Sets the correct flex direction according to the <see cref="ContentPlacement"/> of the image
        /// </summary>
        /// <returns></returns>
        public static string SetFlexDirectionForContainer(ContentPlacement contentPlacement)
        {
            if (contentPlacement == ContentPlacement.Left)
                return "row";
            else if (contentPlacement == ContentPlacement.Right)
                return "row-reverse";
            else if (contentPlacement == ContentPlacement.Top)
                return "column";
            else
                return "column-reverse";
        }

        #endregion

        #region Image
       
        /// <summary>
        /// Creates a Base64 image from the selected file
        /// </summary>
        /// <param name="file">The file</param>
        /// <returns></returns>
        public static async Task<string> UploadBrowserFile(IBrowserFile file)
        {
            var buffers = new byte[file.Size];
            using var stream = file.OpenReadStream(maxAllowedSize: 1024 * 1024);
            await stream.ReadAsync(buffers);

            var imageType = file.ContentType;
            var image = $"data:{imageType};base64,{Convert.ToBase64String(buffers)}";

            return image;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        public static async void SetBrowserFileToModelImage<T>(IBrowserFile file, T model)
            where T : class, IImageable
        {
            var img = await UploadBrowserFile(file);
            model.ImageUrl = new Uri(img);
        }

        #endregion
    }
}
