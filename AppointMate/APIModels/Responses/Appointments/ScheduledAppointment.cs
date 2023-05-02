namespace AppointMate
{
    /// <summary>
    /// The scheduled appointment response model
    /// </summary>
    public class ScheduledAppointment : DateResponseModel
    {
        #region Private Members

        #endregion

        #region Public Properties

        // Embedded staff member

        // Embedded service

        // Embedded customer

        /// <summary>
        /// The service
        /// </summary>
        public ServiceResponseModel? Service { get; set; }

        /// <summary>
        /// The date and time
        /// </summary>
        public DayOfWeekTimeRange DateAndTime { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public ScheduledAppointment() : base()
        {

        }

        #endregion
    }
}
