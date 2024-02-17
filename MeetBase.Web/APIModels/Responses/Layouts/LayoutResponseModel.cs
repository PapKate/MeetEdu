namespace MeetBase.Web
{
    /// <summary>
    /// Represents a company layout response model
    /// </summary>
    public class LayoutResponseModel : StandardResponseModel, IImageable, IDescriptable, INoteable
    {
        #region Private Members

        /// <summary>
        /// The member of the <see cref="Description"/> property
        /// </summary>
        private string? mDescription;

        /// <summary>
        /// The member of the <see cref="Note"/> property
        /// </summary>
        private string? mNote;

        #endregion

        #region Public Properties

        /// <summary>
        /// The description
        /// </summary>
        public string Description
        {
            get => mDescription ?? string.Empty;
            set => mDescription = value;
        }

        /// <summary>
        /// The note
        /// </summary>
        public string Note
        {
            get => mNote ?? string.Empty;
            set => mNote = value;
        }

        /// <summary>
        /// The image URL
        /// </summary>
        public Uri? ImageUrl { get; set; }

        /// <summary>
        /// The display theme
        /// </summary>
        public ImageDisplayTheme DisplayTheme { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public LayoutResponseModel() : base()
        {

        }

        #endregion
    }
}
