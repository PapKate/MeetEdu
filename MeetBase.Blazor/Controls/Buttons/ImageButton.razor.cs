using Microsoft.AspNetCore.Components;

namespace MeetBase.Blazor
{
    public partial class ImageButton
    {
        #region Public Properties

        /// <summary>
        /// The image's URI
        /// </summary>
        [Parameter]
        public Uri? ImageUri { get; set; }

        /// <summary>
        /// The image's label
        /// </summary>
        [Parameter]
        public string? ImageLabel { get; set; }

        /// <summary>
        /// The image's style
        /// </summary>
        [Parameter]
        public string? ImageStyle { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public ImageButton() : base()
        {

        }

        #endregion
    }
}
