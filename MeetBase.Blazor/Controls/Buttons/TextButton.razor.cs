using Microsoft.AspNetCore.Components;

namespace MeetBase.Blazor
{
    public partial class TextButton : ITextConfiguration
    {
        #region Public Properties

        /// <summary>
        /// The button's text
        /// </summary>
        [Parameter]
        public string? Text { get; set; }

        /// <summary>
        /// The font family
        /// </summary>
        [Parameter]
        public string? FontFamily { get; set; }

        /// <summary>
        /// The font size
        /// </summary>
        [Parameter]
        public string? FontSize { get; set; }

        /// <summary>
        /// The font weight
        /// </summary>
        [Parameter]
        public string? FontWeight { get; set; }

        #endregion
    }
}
