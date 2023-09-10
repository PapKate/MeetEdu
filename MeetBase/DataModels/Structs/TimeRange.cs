namespace MeetBase
{
    /// <summary>
    /// Represents a time range
    /// </summary>
    public record struct TimeRange : IReadOnlyRangeable<TimeOnly>
    {
        #region Public Properties

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
        /// <param name="value1">The first value</param>
        /// <param name="value2">The second value</param>
        public TimeRange(TimeOnly value1, TimeOnly value2)
        {
            if (value2.CompareTo(value1) >= 0)
            {
                Start = value1;
                End = value2;
            }
            else
            {
                Start = value2;
                End = value1;
            }
        }

        #endregion
    }
}
