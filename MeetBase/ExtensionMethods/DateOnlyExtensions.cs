namespace MeetBase
{
    /// <summary>
    /// The extension for the <see cref="DateOnly"/>
    /// </summary>
    public static class DateOnlyExtensions
    {
        #region Public Methods

        /// <summary>
        /// Create and return the <see cref="DateTime"/> from the specified <paramref name="value"/>
        /// using the 00:00:00:00 as the time value
        /// </summary>
        /// <param name="value">The date value</param>
        /// <returns></returns>
        public static DateTime ToDateTime(this DateOnly value)
            => value.ToDateTime(new TimeOnly(0, 0, 0, 0));

        /// <summary>
        /// Returns a string that represents the specified <paramref name="value"/> using the ISO8601 format
        /// </summary>
        /// <param name="value">The value</param>
        /// <returns></returns>
        public static string ToISO8601String(this DateOnly value)
            => value.ToString(LocalizationConstants.ISO8601Format, LocalizationConstants.Culture);

        #endregion
    }
}
