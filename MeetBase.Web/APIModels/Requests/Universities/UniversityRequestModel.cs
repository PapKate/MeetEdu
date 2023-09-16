namespace MeetBase.Web
{
    /// <summary>
    /// Request model used for a university
    /// </summary>
    public class UniversityRequestModel : StandardRequestModel
    {
        #region Public Properties

        /// <summary>
        /// The image
        /// </summary>
        public Uri? ImageUrl { get; set; }

        /// <summary>
        /// The label ids
        /// </summary>
        public IEnumerable<string>? LabelIds { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public UniversityRequestModel() : base()
        {

        }

        #endregion
    }
}
