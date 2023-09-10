namespace MeetBase
{
    /// <summary>
    /// Extension methods for <see cref="TimeOnly"/>
    /// </summary>
    public static class TimeOnlyExtensions
    {
        #region Public Methods

        /// <summary>
        /// Returns a string that represents the specified <paramref name="value"/> using the ISO8601 format
        /// </summary>
        /// <param name="value">The value</param>
        /// <returns></returns>
        public static string ToISO8601String(this TimeOnly value)
            => value.ToString(LocalizationConstants.ISO8601Format, LocalizationConstants.Culture);

        #endregion
    }
}
