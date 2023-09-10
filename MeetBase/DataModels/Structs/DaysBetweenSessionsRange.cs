namespace MeetBase
{
    /// <summary>
    /// Represents a number of days between sessions range
    /// </summary>
    public record struct DaysBetweenSessionsRange : IReadOnlyRangeable<uint>
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
        public DaysBetweenSessionsRange(uint value1, uint value2)
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
