namespace MeetBase
{
    /// <summary>
    /// Extension methods for <see cref="TimeOnly"/>
    /// </summary>
    public static class TimeOnlyExtensions
    {
        #region Constants

        /// <summary>
        /// The short time format
        /// <code>ex. 13:25 AM</code>
        /// </summary>
        public const string ShortTimeFormat = "HH:mm tt";

        #endregion

        #region Public Methods

        /// <summary>
        /// Returns a string that represents the specified <paramref name="value"/> using the ISO8601 format
        /// </summary>
        /// <param name="value">The value</param>
        /// <returns></returns>
        public static string ToISO8601String(this TimeOnly value)
            => value.ToString(LocalizationConstants.ISO8601Format, LocalizationConstants.Culture);

        /// <summary>
        /// Returns a string that represents the specified <paramref name="value"/> using the ISO8601 format
        /// </summary>
        /// <param name="value">The value</param>
        /// <returns></returns>
        public static TimeSpan? ToNullableTimeSpan(this TimeOnly value)
            => value == default ? null : value.ToTimeSpan();

        /// <summary>
        /// Returns a string that represents the specified <paramref name="value"/> using the specified <paramref name="format"/>
        /// </summary>
        /// <param name="value">The value</param>
        /// <param name="format">The format</param>
        /// <returns></returns>
        public static string ToFormatedString(this TimeOnly value, string format)
            => value.ToString(format);

        #endregion
    }
}
