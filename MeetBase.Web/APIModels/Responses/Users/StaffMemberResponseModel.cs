namespace MeetBase.Web
{
    /// <summary>
    /// Represents a staff member
    /// </summary>
    public abstract class StaffMemberResponseModel : DateResponseModel, IUserIdentifiable<string>, IDepartmentIdentifiable<string>
    {
        #region Private Members

        /// <summary>
        /// The member of the <see cref="UserId"/> property
        /// </summary>
        private string? mUserId;

        /// <summary>
        /// The member of the <see cref="DepartmentId"/> property
        /// </summary>
        private string? mDepartmentId;

        /// <summary>
        /// The member of the <see cref="Quote"/> property
        /// </summary>
        private string? mQuote;

        #endregion

        #region Public Properties

        /// <summary>
        /// The user id
        /// </summary>
        public string UserId
        {
            get => mUserId ?? string.Empty;
            set => mUserId = value;
        }

        /// <summary>
        /// The department id
        /// </summary>
        public string DepartmentId
        {
            get => mDepartmentId ?? string.Empty;
            set => mDepartmentId = value;
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
        /// The weekly schedule
        /// </summary>
        public WeeklySchedule? WeeklySchedule { get; set; }

        /// <summary>
        /// The user
        /// </summary>
        public EmbeddedUserResponseModel? User { get; set; }

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
    /// A minimal version of the <see cref="StaffMemberResponseModel"/> that contains the fields that are 
    /// more frequently used when embedding documents in the MongoDB
    /// </summary>
    public abstract class EmbeddedStaffMemberResponseModel : EmbeddedBaseResponseModel, IUserIdentifiable<string>, IDepartmentIdentifiable<string>
    {
        #region Private Members

        /// <summary>
        /// The member of the <see cref="UserId"/> property
        /// </summary>
        private string? mUserId;

        /// <summary>
        /// The member of the <see cref="DepartmentId"/> property
        /// </summary>
        private string? mDepartmentId;

        #endregion

        #region Public Properties

        /// <summary>
        /// The user id
        /// </summary>
        public string UserId
        {
            get => mUserId ?? string.Empty;
            set => mUserId = value;
        }

        /// <summary>
        /// The department id
        /// </summary>
        public string DepartmentId
        {
            get => mDepartmentId ?? string.Empty;
            set => mDepartmentId = value;
        }

        /// <summary>
        /// The weekly schedule
        /// </summary>
        public WeeklySchedule? WeeklySchedule { get; set; }

        /// <summary>
        /// The user
        /// </summary>
        public EmbeddedUserResponseModel? User { get; set; }

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
