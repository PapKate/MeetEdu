using MeetBase;

using Microsoft.AspNetCore.Components;

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

        /// <summary>
        /// The first day of the week
        /// </summary>
        private DateTime mFirstDateOfWeek = DateTime.Now.GetFirstDayOfWeek();

        /// <summary>
        /// The day of the first date of the week
        /// </summary>
        private int mFirstDayOfWeek = 1;

        /// <summary>
        /// The current date
        /// </summary>
        private DateTime mCurrentDate = DateTime.Now;

        /// <summary>
        /// The displayed date
        /// </summary>
        private DateTime mDisplayedFirstDateOfWeek = default;

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

        /// <summary>
        /// The work hours
        /// </summary>
        [Parameter]
        public IEnumerable<DayOfWeekTimeRange>? WorkHours { get; set; }

        /// <summary>
        /// The work hours
        /// </summary>
        [Parameter]
        public IEnumerable<Lecture>? Lectures { get; set; }

        /// <summary>
        /// A flag indicating whether the schedule can be modified or not
        /// </summary>
        [Parameter]
        public bool IsEditable { get; set; }

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

            mFirstDateOfWeek = mCurrentDate.DayOfWeek == DayOfWeek.Sunday ? mCurrentDate.AddDays(-6) : mCurrentDate.GetFirstDayOfWeek();    

            mFirstDayOfWeek = mFirstDateOfWeek.Day;
            
            mDisplayedFirstDateOfWeek = mFirstDateOfWeek;

            mWeekDays = daysOfWeek;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Gets the css class name according to where the specified <paramref name="date"/> is placed as to the <see cref="mCurrentDate"/>
        /// </summary>
        /// <param name="date">The day</param>
        /// <returns></returns>
        private string GetCssClassForDate(DateTime date)
        {
            string? className;
            var currentDateOnly = mCurrentDate.ToDateOnly();
            var dateOnly = date.ToDateOnly();
            if (currentDateOnly == dateOnly)
                className = "scheduleToday";
            else if(currentDateOnly > dateOnly)
                className = "scheduleBeforeToday";
            else
                className = "scheduleAfterToday";
            
            return className;
        }

        #endregion

        #region Public Events

        /// <summary>
        /// Fires when the edit button is clicked
        /// </summary>
        [Parameter]
        public EventCallback EditButtonOnClick { get; set; }

        #endregion
    }
}
