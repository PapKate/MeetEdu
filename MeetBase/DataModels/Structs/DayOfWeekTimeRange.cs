namespace MeetBase
{
    /// <summary>
    /// Represents a time range of a specific week day
    /// </summary>
    public record struct DayOfWeekTimeRange : IReadOnlyRangeable<TimeOnly>
    {
        #region Public Properties

        /// <summary>
        /// The text
        /// </summary>
        public string? Text { get; set; }

        /// <summary>
        /// The day of week
        /// </summary>
        public DayOfWeek DayOfWeek { get; }

        /// <summary>
        /// The start
        /// </summary>
        public TimeOnly Start { get; }

        /// <summary>
        /// The end
        /// </summary>
        public TimeOnly End { get; }

        /// <summary>
        /// The minimum value
        /// </summary>
        TimeOnly IReadOnlyRangeable<TimeOnly>.Minimum => Start;

        /// <summary>
        /// The maximum value
        /// </summary>
        TimeOnly IReadOnlyRangeable<TimeOnly>.Maximum => End;

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="dayOfWeek">The day of week</param>
        /// <param name="start">The first value</param>
        /// <param name="end">The second value</param>
        public DayOfWeekTimeRange(DayOfWeek dayOfWeek, TimeOnly start, TimeOnly end)
        {
            DayOfWeek = dayOfWeek;
            if (end.CompareTo(start) >= 0)
            {
                Start = start;
                End = end;
            }
            else
            {
                Start = end;
                End = start;
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Converts the specified <paramref name="values"/> to a <see cref="Dictionary{TKey, TValue}"/>
        /// that maps the <see cref="System.DayOfWeek"/> values with the related <see cref="TimeRange"/>s
        /// </summary>
        /// <param name="values">The values</param>
        /// <returns></returns>
        public static Dictionary<DayOfWeek, IEnumerable<TimeRange>> Convert(IEnumerable<DayOfWeekTimeRange> values)
            => values.GroupBy(x => x.DayOfWeek).ToDictionary(x => x.Key, x => x.Select(y => new TimeRange(y.Start, y.End)));

        /// <summary>
        /// Converts a dictionary that maps the <see cref="System.DayOfWeek"/> values with the related <see cref="TimeRange"/>s
        /// to a collection of <see cref="DayOfWeekTimeRange"/>s
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        public static IEnumerable<DayOfWeekTimeRange> Convert(IReadOnlyDictionary<DayOfWeek, IEnumerable<TimeRange>> values)
            => values.SelectMany(x => x.Value.Select(y => new DayOfWeekTimeRange(x.Key, y.Start, y.End))).ToList();

        #endregion
    }
}
