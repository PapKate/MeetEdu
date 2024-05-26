using Microsoft.AspNetCore.Components;

namespace MeetBase.Blazor
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

        #endregion

        #region Protected Properties

        /// <summary>
        /// The header manager for displaying the user data
        /// </summary>
        [Inject]
        protected HeaderUserManager? Manager { get; set; }

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
