namespace MeetCore
{
    /// <summary>
    /// The schedule component
    /// </summary>
    public partial class Schedule
    {
        #region Private Members

        /// <summary>
        /// The days of the week
        /// </summary>
        private IEnumerable<DayOfWeek>? mWeekDays;

        private TimeSpan mDuration;

        #endregion

        #region Public Properties

        /// <summary>
        /// The starting time
        /// </summary>
        public TimeOnly TimeStart { get; set; } = new TimeOnly(7, 0);

        /// <summary>
        /// The ending time
        /// </summary>
        public TimeOnly TimeEnd { get; set; } = new TimeOnly(22, 0);

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public Schedule() : base()
        {

        }

        #endregion

        #region Protected Methods

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        protected override void OnInitialized()
        {
            base.OnInitialized();

            var daysOfWeek = Enum.GetValues<DayOfWeek>().ToList();

            daysOfWeek.Remove(DayOfWeek.Sunday);
            daysOfWeek.Add(DayOfWeek.Sunday);

            mWeekDays = daysOfWeek;
        }

        #endregion
    }
}
