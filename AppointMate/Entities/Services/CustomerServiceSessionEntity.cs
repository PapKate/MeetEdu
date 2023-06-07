namespace AppointMate
{
    /// <summary>
    /// The session response model
    /// </summary>
    public class CustomerServiceSessionEntity : StandardResponseModel, IDescriptable
    {
        #region Private Members

        /// <summary>
        /// The member of the <see cref="Description"/> property
        /// </summary>
        private string? mDescription;

        #endregion

        #region Public Properties

        /// <summary>
        /// The number of the session
        /// </summary>
        public uint Index { get; set; }

        /// <summary>
        /// The date and time
        /// </summary>
        public DayOfWeekTimeRange DateAndTime { get; set; }

        /// <summary>
        /// The description
        /// </summary>
        public string Description
        {
            get => mDescription ?? string.Empty;
            set => mDescription = value;
        }

        /// <summary>
        /// The duration
        /// </summary>
        public TimeSpan Duration { get; set; }

        /// <summary>
        /// A flag indicating whether it is confirmed or not
        /// </summary>
        public bool IsConfirmed { get; set; }

        /// <summary>
        /// The session status
        /// </summary>
        public SessionStatus SessionStatus { get; set; }

        /// <summary>
        /// The staff member
        /// </summary>
        public EmbeddedStaffMemberEntity? StaffMember { get; set; }

        /// <summary>
        /// The service
        /// </summary>
        public EmbeddedCustomerServiceEntity? CustomerService { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public CustomerServiceSessionEntity() : base()
        {

        }

        #endregion
    }

    /// <summary>
    /// The session response model
    /// </summary>
    public class EmbeddedCustomerServiceSessionEntity : EmbeddedStandardEntity
    {
        #region Public Properties

        /// <summary>
        /// The number of the session
        /// </summary>
        public uint Index { get; set; }

        /// <summary>
        /// The date and time
        /// </summary>
        public DayOfWeekTimeRange DateAndTime { get; set; }

        /// <summary>
        /// The duration
        /// </summary>
        public TimeSpan Duration { get; set; }

        /// <summary>
        /// A flag indicating whether it is confirmed or not
        /// </summary>
        public bool IsConfirmed { get; set; }

        /// <summary>
        /// The session status
        /// </summary>
        public SessionStatus SessionStatus { get; set; }

        /// <summary>
        /// The service
        /// </summary>
        public EmbeddedCustomerServiceEntity? CustomerService { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public EmbeddedCustomerServiceSessionEntity() : base()
        {

        }

        #endregion
    }
}
