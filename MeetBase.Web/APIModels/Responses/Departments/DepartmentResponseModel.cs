namespace MeetBase.Web
{
    /// <summary>
    /// Represents a department 
    /// </summary>
    public class DepartmentResponseModel : StandardResponseModel, IImageable, INoteable, IUniversityIdentifiable<string>
    {
        #region Private Members

        /// <summary>
        /// The member of the <see cref="UniversityId"/> property
        /// </summary>
        private string? mUniversityId;

        /// <summary>
        /// The member of the <see cref="Email"/> property
        /// </summary>
        private string? mEmail;

        /// <summary>
        /// The member of the <see cref="Note"/> property
        /// </summary>
        private string? mNote;

        /// <summary>
        /// The member of the <see cref="Quote"/> property
        /// </summary>
        private string? mQuote;

        /// <summary>
        /// The member of the <see cref="Description"/> property
        /// </summary>
        private string? mDescription;

        /// <summary>
        /// The member of the <see cref="LayoutDescription"/> property
        /// </summary>
        private string? mLayoutDescription;

        /// <summary>
        /// The member of the <see cref="SecretaryDescription"/> property
        /// </summary>
        private string? mSecretaryDescription;

        /// <summary>
        /// The member of the <see cref="Fields"/> property
        /// </summary>
        private IEnumerable<string>? mFields;

        /// <summary>
        /// The member of the <see cref="Labels"/> property
        /// </summary>
        private IEnumerable<EmbeddedLabelResponseModel>? mLabels;

        /// <summary>
        /// The member of the <see cref="Secretaries"/> property
        /// </summary>
        private IEnumerable<EmbeddedSecretaryResponseModel>? mSecretaries;

        /// <summary>
        /// The member of the <see cref="Websites"/> property
        /// </summary>
        private IEnumerable<Website>? mWebsites;

        #endregion

        #region Public Properties

        /// <summary>
        /// The university id
        /// </summary>
        public string UniversityId
        {
            get => mUniversityId ?? string.Empty;
            set => mUniversityId = value;
        }

        /// <summary>
        /// The related websites 
        /// </summary>
        public IEnumerable<Website> Websites
        {
            get => mWebsites ?? Enumerable.Empty<Website>();
            set => mWebsites = value;
        }

        /// <summary>
        /// The email
        /// </summary>
        public string Email
        {
            get => mEmail ?? string.Empty;
            set => mEmail = value;
        }

        /// <summary>
        /// The category
        /// </summary>
        public DepartmentType Category { get; set; }

        /// <summary>
        /// The fields of study
        /// </summary>
        public IEnumerable<string> Fields
        {
            get => mFields ?? Enumerable.Empty<string>();
            set => mFields = value;
        }

        /// <summary>
        /// The quote
        /// </summary>
        public string Quote
        {
            get => mQuote ?? string.Empty;
            set => mQuote = value;
        }

        /// <summary>
        /// The description
        /// </summary>
        public string Description
        {
            get => mDescription ?? string.Empty;
            set => mDescription = value;
        }

        /// <summary>
        /// The layout description
        /// </summary>
        public string LayoutDescription
        {
            get => mLayoutDescription ?? string.Empty;
            set => mLayoutDescription = value;
        }

        /// <summary>
        /// The secretary description
        /// </summary>
        public string SecretaryDescription
        {
            get => mSecretaryDescription ?? string.Empty;
            set => mSecretaryDescription = value;
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
        /// The number of staff members
        /// </summary>
        public uint TotalStaffMembers { get; set; }

        /// <summary>
        /// The work hours
        /// </summary>
        public WeeklySchedule? WorkHours { get; set; }

        /// <summary>
        /// The image URL
        /// </summary>
        public Uri? ImageUrl { get; set; }

        /// <summary>
        /// The location
        /// </summary>
        public Location? Location { get; set; }

        /// <summary>
        /// The phone number
        /// </summary>
        public PhoneNumber? PhoneNumber { get; set; }

        /// <summary>
        /// The contact message template
        /// </summary>
        public DepartmentContactMessageTemplate? ContactMessageTemplate { get; set; }

        /// <summary>
        /// The labels
        /// </summary>
        public IEnumerable<EmbeddedLabelResponseModel> Labels
        {
            get => mLabels ?? Enumerable.Empty<EmbeddedLabelResponseModel>();
            set => mLabels = value;
        }

        /// <summary>
        /// The secretaries
        /// </summary>
        public IEnumerable<EmbeddedSecretaryResponseModel> Secretaries
        {
            get => mSecretaries ?? Enumerable.Empty<EmbeddedSecretaryResponseModel>();
            set => mSecretaries = value;
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public DepartmentResponseModel() : base()
        {

        }

        #endregion
    }


    /// <summary>
    /// A minimal version of the <see cref="DepartmentResponseModel "/> that contains the fields that are 
    /// more frequently used when embedding documents in the MongoDB
    /// </summary>
    public class EmbeddedDepartmentResponseModel : EmbeddedStandardResponseModel, IImageable, IUniversityIdentifiable<string>
    {
        #region Private Members

        /// <summary>
        /// The member of the <see cref="UniversityId"/> property
        /// </summary>
        private string? mUniversityId;

        #endregion

        #region Public Properties

        /// <summary>
        /// The university id
        /// </summary>
        public string UniversityId
        {
            get => mUniversityId ?? string.Empty;
            set => mUniversityId = value;
        }

        /// <summary>
        /// The number of staff members
        /// </summary>
        public uint TotalStaffMembers { get; set; }

        /// <summary>
        /// The image URL
        /// </summary>
        public Uri? ImageUrl { get; set; }

        /// <summary>
        /// The location
        /// </summary>
        public Location? Location { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public EmbeddedDepartmentResponseModel() : base()
        {

        }

        #endregion
    }
}
