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
            mFirstDayOfWeek = mFirstDateOfWeek.Day;
            mWeekDays = daysOfWeek;

            WorkHours = new List<DayOfWeekTimeRange>()
            { 
                new DayOfWeekTimeRange(DayOfWeek.Tuesday, new TimeOnly(10, 0), new TimeOnly(13, 0))
                {
                    Text = "Ώρες Γραφείου",
                }
            };

            Lectures = new List<Lecture>()
            {
                new Lecture()
                {
                    Name = "Data Structures",
                    Color = MeetBase.Blazor.PaletteColors.Persimmon,
                    LectureHours = new List<DayOfWeekTimeRange>()
                    {
                        new DayOfWeekTimeRange(DayOfWeek.Tuesday, new TimeOnly(14, 0), new TimeOnly(16, 0))
                        {
                           Text = "Αίθουσα: Γ",
                        },
                        new DayOfWeekTimeRange(DayOfWeek.Friday, new TimeOnly(14, 0), new TimeOnly(16, 0))
                        {
                           Text = "Αίθουσα: Β",
                        },
                    },
                },
                new Lecture()
                {
                    Name = "Artificial Intelligence",
                    Color = MeetBase.Blazor.PaletteColors.Blue,
                    LectureHours = new List<DayOfWeekTimeRange>()
                    {
                        new DayOfWeekTimeRange(DayOfWeek.Thursday, new TimeOnly(11, 0), new TimeOnly(13, 0))
                        {
                           Text = "Αίθουσα: Δ1",
                        },
                        new DayOfWeekTimeRange(DayOfWeek.Wednesday, new TimeOnly(17, 0), new TimeOnly(19, 0))
                        {
                           Text = "Αίθουσα: Δ1",
                        },
                    },
                }
            };
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Gets the css class name according to where the specified <paramref name="day"/> is placed as to the <see cref="mCurrentDate"/>
        /// </summary>
        /// <param name="day">The day</param>
        /// <returns></returns>
        private string GetCssClassForDate(int day)
        {
            var className = string.Empty;
            if (mCurrentDate.Day == day)
                className = "scheduleToday";
            else if(mCurrentDate.Day > day)
                className = "scheduleBeforeToday";
            else
                className = "scheduleAfterToday";
            
            return className;
        }

        #endregion
    }
}
