namespace AppointMate
{
    /// <summary>
    /// Request model used for a weekly schedule
    /// </summary>
    public class WeeklyScheduleRequestModel : BaseRequestModel
    {
        #region Public Properties

        /// <summary>
        /// The work hours
        /// </summary>
        public IEnumerable<DayOfWeekTimeRange>? WorkHours { get; set; }

        /// <summary>
        /// The note
        /// </summary>
        public string? Note { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public WeeklyScheduleRequestModel() : base()
        {

        }

        #endregion
    }
}
