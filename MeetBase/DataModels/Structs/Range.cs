namespace MeetBase
{
    /// <summary>
    /// Represents a range of values
    /// </summary>
    /// <typeparam name="T">The type of the values</typeparam>
    public record struct Range<T> : IReadOnlyRangeable<T>
        where T : IComparable<T>
    {
        #region Public Properties

        /// <summary>
        /// The minimum value
        /// </summary>
        public T Minimum { get; }

        /// <summary>
        /// The maximum value
        /// </summary>
        public T Maximum { get; }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="value1">The first value</param>
        /// <param name="value2">The second value</param>
        public Range(T value1, T value2) : this()
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
