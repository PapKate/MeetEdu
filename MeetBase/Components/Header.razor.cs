using Microsoft.AspNetCore.Components;

namespace MeetBase
{
    /// <summary>
    /// The header component
    /// </summary>
    public partial class Header 
    {
        #region Public Properties

        /// <summary>
        /// The app name
        /// </summary>
        [Parameter]
        public string? AppName { get; set; }

        /// <summary>
        /// The back color
        /// </summary>
        [Parameter]
        public string? BackColor { get; set; }

        /// <summary>
        /// The fore color
        /// </summary>
        [Parameter]
        public string? ForeColor { get; set; }

        /// <summary>
        /// The username
        /// </summary>
        [Parameter]
        public string? Username { get; set; }

        /// <summary>
        /// The user image URL
        /// </summary>
        [Parameter]
        public Uri? UserImageUrl { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public Header() : base()
        {

        }

        #endregion
    }
}
