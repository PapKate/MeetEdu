namespace MeetBase.Web
{
    /// <summary>
    /// Request model for a layout
    /// </summary>
    public abstract class LayoutRequestModel : StandardRequestModel, IImageable
    {
        #region Public Properties

        /// <summary>
        /// The description
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// The note
        /// </summary>
        public string? Note { get; set; }

        /// <summary>
        /// The image URL
        /// </summary>
        public Uri? ImageUrl { get; set; }

        /// <summary>
        /// The display theme
        /// </summary>
        public ImageDisplayTheme? DisplayTheme { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public LayoutRequestModel() : base()
        {

        }

        #endregion
    }
}
