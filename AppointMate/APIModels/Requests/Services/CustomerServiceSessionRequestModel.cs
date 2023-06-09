namespace AppointMate
{
    /// <summary>
    /// Request model used for a customer service session
    /// </summary>
    public class CustomerServiceSessionRequestModel : StandardRequestModel
    {
        #region Public Properties

        /// <summary>
        /// The number of the session
        /// </summary>
        public uint? Index { get; set; }

        /// <summary>
        /// The date and time
        /// </summary>
        public DayOfWeekTimeRange? DateAndTime { get; set; }

        /// <summary>
        /// The description
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// The duration
        /// </summary>
        public TimeSpan? Duration { get; set; }

        /// <summary>
        /// A flag indicating whether it is confirmed or not
        /// </summary>
        public bool? IsConfirmed { get; set; }

        /// <summary>
        /// The session status
        /// </summary>
        public SessionStatus? SessionStatus { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public CustomerServiceSessionRequestModel() : base()
        {

        }

        #endregion
    }
}
