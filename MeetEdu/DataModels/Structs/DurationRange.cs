namespace MeetEdu
{
    /// <summary>
    /// Represents a duration range
    /// </summary>
    public record struct DurationRange : IReadOnlyRangeable<TimeSpan>
    {
        #region Public Properties

        /// <summary>
        /// The minimum value
        /// </summary>
        public TimeSpan Minimum { get; set; }

        /// <summary>
        /// The maximum value
        /// </summary>
        public TimeSpan Maximum { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="value1">The first value</param>
        /// <param name="value2">The second value</param>
        public DurationRange(TimeSpan value1, TimeSpan value2)
        {
            if (value2.TotalSeconds.CompareTo(value1.TotalSeconds) >= 0)
            {
                Minimum = value1;
                Maximum = value2;
            }
            else
            {
                Minimum = value2;
                Maximum = value1;
            }
        }

        #endregion
    }
}
