namespace MeetBase.Web
{
    /// <summary>
    /// Represents a professor 
    /// </summary>
    public class ProfessorResponseModel : StaffMemberResponseModel
    {
        #region Private Members

        /// <summary>
        /// The member of the <see cref="Field"/> property
        /// </summary>
        private string? mField;

        /// <summary>
        /// The member of the <see cref="ResearchInterests"/> property
        /// </summary>
        private string? mResearchInterests;

        /// <summary>
        /// The member of the <see cref="Websites"/> property
        /// </summary>
        private IEnumerable<Uri>? mWebsites;

        #endregion

        #region Public Properties

        /// <summary>
        /// The rank
        /// </summary>
        public ProfessorRank Rank { get; set; }

        /// <summary>
        /// The personal websites 
        /// </summary>
        public IEnumerable<Uri> Websites
        {
            get => mWebsites ?? Enumerable.Empty<Uri>();
            set => mWebsites = value;
        }

        /// <summary>
        /// The field of study
        /// </summary>
        public string Field
        {
            get => mField ?? string.Empty;
            set => mField = value;
        }

        /// <summary>
        /// The research interests
        /// </summary>
        public string ResearchInterests
        {
            get => mResearchInterests ?? string.Empty;
            set => mResearchInterests = value;
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public ProfessorResponseModel() : base()
        {

        }

        #endregion
    }

    /// <summary>
    /// A minimal version of the <see cref="ProfessorResponseModel"/> that contains the fields that are 
    /// more frequently used when embedding documents in the MongoDB
    /// </summary>
    public class EmbeddedProfessorResponseModel : EmbeddedStaffMemberResponseModel
    {
        #region Private Members

        /// <summary>
        /// The member of the <see cref="Field"/> property
        /// </summary>
        private string? mField;

        #endregion

        #region Public Properties

        /// <summary>
        /// The rank
        /// </summary>
        public ProfessorRank Rank { get; set; }

        /// <summary>
        /// The field of study
        /// </summary>
        public string Field
        {
            get => mField ?? string.Empty;
            set => mField = value;
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public EmbeddedProfessorResponseModel() : base()
        {

        }

        #endregion
    }
}
