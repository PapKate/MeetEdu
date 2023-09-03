namespace MeetEdu
{
    /// <summary>
    /// Represents a weekly schedule
    /// </summary>
    public class WeeklyScheduleResponseModel : BaseResponseModel, INoteable
    {
        #region Private Members

        /// <summary>
        /// The member of the <see cref="WorkHours"/> property
        /// </summary>
        private IEnumerable<DayOfWeekTimeRange>? mWorkHours;

        /// <summary>
        /// The member of the <see cref="Note"/> property
        /// </summary>
        private string? mNote;

        #endregion

        #region Public Properties

        /// <summary>
        /// The work hours
        /// </summary>
        public IEnumerable<DayOfWeekTimeRange> WorkHours
        {
            get => mWorkHours ?? Enumerable.Empty<DayOfWeekTimeRange>();
            set => mWorkHours = value;
        }

        /// <summary>
        /// The note
        /// </summary>
        public string Note
        {
            get => mNote ?? string.Empty;
            set => mNote = value;
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public WeeklyScheduleResponseModel() : base()
        {

        }

        #endregion
    }
}
