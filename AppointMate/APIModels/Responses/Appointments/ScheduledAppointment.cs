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

        // Embedded service session

        // Embedded customer

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
