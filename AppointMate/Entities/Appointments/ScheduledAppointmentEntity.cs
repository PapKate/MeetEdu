namespace AppointMate
{
    /// <summary>
    /// The scheduled appointment response model
    /// </summary>
    public class ScheduledAppointmentEntity : DateEntity
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
        public ScheduledAppointmentEntity() : base()
        {

        }

        #endregion
    }
}
