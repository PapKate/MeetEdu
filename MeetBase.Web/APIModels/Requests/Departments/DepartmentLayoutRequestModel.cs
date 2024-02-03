namespace MeetBase.Web
{
    /// <summary>
    /// Request model for a department layout
    /// </summary>
    public class DepartmentLayoutRequestModel : StandardRequestModel, IImageable
    {
        #region Public Properties

        /// <summary>
        /// The department id
        /// </summary>
        public string? DepartmentId { get; set; }

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
        public DepartmentLayoutRequestModel() : base()
        {

        }

        #endregion
    }
}
