using Microsoft.JSInterop;
using System.Text;

namespace MeetBase.Blazor
{
    /// <summary>
    /// Provides helper methods regarding <see cref="IJSRuntime"/>
    /// </summary>
    public static class JSRunTimeHelpers
    {
        #region Constants

        /// <summary>
        /// The file extension of a calendar event
        /// </summary>
        public const string CalendarEventFileExtension = ".ics";

        #endregion

        #region Public Methods

        /// <summary>
        /// Creates and downloads a file with the specified <paramref name="fileName"/> that has the specified <paramref name="content"/>
        /// </summary>
        /// <param name="jSRuntime">The instance of the <see cref="IJSRuntime"/> service</param>
        /// <param name="content">The content</param>
        /// <param name="fileName">The name of the file plus its extension to save</param>
        /// <returns></returns>
        public static async Task DownloadData(IJSRuntime jSRuntime, string content, string fileName)
        {
            var fileStream = new MemoryStream(new UTF8Encoding(true).GetBytes(content));
            using var streamRef = new DotNetStreamReference(stream: fileStream);
            await jSRuntime.InvokeVoidAsync("downloadFileFromStream", fileName, streamRef);
        }

        /// <summary>
        /// Creates and downloads a file with the specified <paramref name="fileName"/> that has the specified <paramref name="content"/>
        /// </summary>
        /// <param name="jSRuntime">The instance of the <see cref="IJSRuntime"/> service</param>
        /// <param name="content">The content</param>
        /// <param name="fileName">The name of the file plus its extension to save</param>
        /// <returns></returns>
        public static async Task DownloadCalendarEventsAsync(IJSRuntime jSRuntime, string content, string fileName)
        {
            var fileStream = new MemoryStream(new UTF8Encoding(true).GetBytes(content));
            using var streamRef = new DotNetStreamReference(stream: fileStream);
            await jSRuntime.InvokeVoidAsync("downloadFileFromStream", $"{fileName}{CalendarEventFileExtension}", streamRef);
        }

        #endregion
    }
}
