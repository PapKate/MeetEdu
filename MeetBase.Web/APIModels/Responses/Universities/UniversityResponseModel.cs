namespace MeetBase.Web
{
    /// <summary>
    /// Represents a university 
    /// </summary>
    public class UniversityResponseModel : StandardResponseModel, IImageable
    {
        #region Private Members

        /// <summary>
        /// The member of the <see cref="Labels"/> property
        /// </summary>
        private IEnumerable<EmbeddedLabelResponseModel>? mLabels;

        #endregion

        #region Public Properties

        /// <summary>
        /// The image
        /// </summary>
        public Uri? ImageUrl { get; set; }

        /// <summary>
        /// The labels
        /// </summary>
        public IEnumerable<EmbeddedLabelResponseModel> Labels
        {
            get => mLabels ?? Enumerable.Empty<EmbeddedLabelResponseModel>();
            set => mLabels = value;
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public UniversityResponseModel() : base()
        {

        }

        #endregion
    }

    /// <summary>
    /// A minimal version of the <see cref="UniversityResponseModel"/> that contains the fields that are 
    /// more frequently used when embedding documents in the MongoDB
    /// </summary>
    public class EmbeddedUniversityResponseModel : EmbeddedStandardResponseModel, IImageable
    {
        #region Public Properties

        /// <summary>
        /// The image
        /// </summary>
        public Uri? ImageUrl { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public EmbeddedUniversityResponseModel() : base()
        {

        }

        #endregion
    }
}
