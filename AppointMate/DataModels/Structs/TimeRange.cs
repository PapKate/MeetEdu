namespace AppointMate
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

    /// <summary>
    /// Represents a price range
    /// </summary>
    public record struct PriceRange : IReadOnlyRangeable<double>
    {
        #region Public Properties

        /// <summary>
        /// The minimum value
        /// </summary>
        public double Minimum { get; set; }

        /// <summary>
        /// The maximum value
        /// </summary>
        public double Maximum { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="value1">The first value</param>
        /// <param name="value2">The second value</param>
        public PriceRange(double value1, double value2)
        {
            if (value2.CompareTo(value1) >= 0)
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

    /// <summary>
    /// Represents a number of sessions range
    /// </summary>
    public record struct SessionsRange : IReadOnlyRangeable<uint>
    {
        #region Public Properties

        /// <summary>
        /// The minimum value
        /// </summary>
        public uint Minimum { get; set; }

        /// <summary>
        /// The maximum value
        /// </summary>
        public uint Maximum { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="value1">The first value</param>
        /// <param name="value2">The second value</param>
        public SessionsRange(uint value1, uint value2)
        {
            if (value2.CompareTo(value1) >= 0)
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
