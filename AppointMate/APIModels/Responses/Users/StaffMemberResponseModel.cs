namespace AppointMate
{
    /// <summary>
    /// Represents a staff member
    /// </summary>
    public class StaffMemberResponseModel : DateResponseModel
    {
        #region Private Members

        /// <summary>
        /// The member of the <see cref="Quote"/> property
        /// </summary>
        private string? mQuote;

        /// <summary>
        /// The member of the <see cref="Labels"/> property
        /// </summary>
        private IEnumerable<LabelResponseModel>? mLabels;

        #endregion

        #region Public Properties

        /// <summary>
        /// The quote
        /// </summary>
        public string Quote 
        { 
            get => mQuote ?? string.Empty;
            set => mQuote = value;
        }

        /// <summary>
        /// A flag indicating whether they are an owner
        /// </summary>
        public bool IsOwner { get; set; }

        /// <summary>
        /// The labels
        /// </summary>
        public IEnumerable<LabelResponseModel> Labels 
        {
            get => mLabels ?? Enumerable.Empty<LabelResponseModel>();
            set => mLabels = value;
        }

        /// <summary>
        /// The work hours
        /// </summary>
        public WeeklyScheduleEntity? WorkHours { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public StaffMemberResponseModel() : base()
        {

        }

        #endregion
    }

    /// <summary>
    /// A minimal version of the <see cref="StaffMemberResponseModel"/> used when embedding
    /// </summary>
    public class EmbeddedStaffMemberResponseModel : EmbeddedBaseEntity
    {
        #region Private Members

        /// <summary>
        /// The member of the <see cref="Quote"/> property
        /// </summary>
        private string? mQuote;

        /// <summary>
        /// The member of the <see cref="Roles"/> property
        /// </summary>
        private IEnumerable<string>? mRoles;

        #endregion

        #region Public Properties

        /// <summary>
        /// The quote
        /// </summary>
        public string Quote
        {
            get => mQuote ?? string.Empty;
            set => mQuote = value;
        }

        /// <summary>
        /// A flag indicating whether they are an owner
        /// </summary>
        public bool IsOwner { get; set; }

        /// <summary>
        /// The roles
        /// </summary>
        public IEnumerable<string> Roles
        {
            get => mRoles ?? Enumerable.Empty<string>();
            set => mRoles = value;
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public EmbeddedStaffMemberResponseModel() : base()
        {

        }

        #endregion
    }
}
