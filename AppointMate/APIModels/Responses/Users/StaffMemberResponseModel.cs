namespace AppointMate
{
    /// <summary>
    /// The staff response model
    /// </summary>
    public class StaffMemberResponseModel : UserResponseModel
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

        /// <summary>
        /// The member of the <see cref="Schedule"/> property
        /// </summary>
        private IEnumerable<DayOfWeekTimeRange>? mSchedule;

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
        /// The roles
        /// </summary>
        public IEnumerable<string> Roles 
        {
            get => mRoles ?? Enumerable.Empty<string>();
            set => mRoles = value;
        } 

        /// <summary>
        /// The schedule
        /// </summary>
        public IEnumerable<DayOfWeekTimeRange> Schedule 
        { 
            get => mSchedule ?? Enumerable.Empty<DayOfWeekTimeRange>();
            set => mSchedule = value;
        }

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

    public class EmbeddedStaffMemberResponseModel : EmbeddedUserResponseModel
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
