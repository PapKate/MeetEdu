namespace AppointMate
{
    /// <summary>
    /// Helper methods for numbers
    /// </summary>
    public static class NumericHelpers
    {
        #region Constants

        /// <summary>
        /// The default decimal precision
        /// </summary>
        public const uint DefaultDecimalPrecision = 2;

        /// <summary>
        /// The default mode of <see cref="MidpointRounding"/>
        /// </summary>
        public const MidpointRounding DefaultMode = MidpointRounding.AwayFromZero;

        #endregion
        
        #region Public Methods

        /// <summary>
        /// Rounds the specified <paramref name="amount"/> using the specified options
        /// </summary>
        /// <param name="amount">The amount</param>
        /// <param name="decimalsPrecision">The decimals precision</param>
        /// <param name="mode">The midpoint rounding mode</param>
        /// <returns></returns>
        public static decimal Round(decimal amount, uint decimalsPrecision, MidpointRounding mode)
        {
            return Math.Round(amount, (int)decimalsPrecision, mode);
        }

        /// <summary>
        /// Rounds the specified <paramref name="amount"/> using the specified options
        /// </summary>
        /// <param name="amount">The amount</param>
        /// <returns></returns>
        public static decimal Round(decimal amount)
            => Round(amount, DefaultDecimalPrecision, DefaultMode);

        #endregion
    }
}
