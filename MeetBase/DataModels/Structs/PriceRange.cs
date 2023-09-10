namespace MeetBase
{
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
}
