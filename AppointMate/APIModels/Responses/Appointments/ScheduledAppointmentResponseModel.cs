namespace AppointMate
{
    /// <summary>
    /// The scheduled appointment response model
    /// </summary>
    public class ScheduledAppointmentResponseModel : DateResponseModel
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
        public ScheduledAppointmentResponseModel() : base()
        {

        }

        #endregion
    }
}
