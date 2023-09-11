using Microsoft.AspNetCore.Components;

namespace MeetBase.Blazor
{
    /// <summary>
    /// A button that contains only a vector image
    /// </summary>
    public partial class IconButton : IVectorImagable
    {
        #region Public Properties

        /// <summary>
        /// The vector source
        /// </summary>
        [Parameter]
        public VectorSource? VectorSource { get; set; }

        /// <summary>
        /// The icon's CSS style
        /// </summary>
        [Parameter]
        public string? IconStyle { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public IconButton() : base()
        {

        }

        #endregion
    }
}
