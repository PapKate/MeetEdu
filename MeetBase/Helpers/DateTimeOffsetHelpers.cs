namespace MeetBase
{
    /// <summary>
    /// Helper methods for <see cref="DateTimeOffset"/>
    /// </summary>
    public static class DateTimeOffsetHelpers
    {
        #region Public Properties

        /// <summary>
        /// The offset
        /// </summary>
        public static TimeSpan? Offset = null;

        #endregion

        #region Public Methods

        /// <summary>
        /// Calculates the offset for the country
        /// </summary>
        /// <returns></returns>
        public static TimeSpan? CalculateOffset()
        {
            // Get the time zone info for Greece
            TimeZoneInfo greeceTimeZone = TimeZoneInfo.FindSystemTimeZoneById("GTB Standard Time");

            // Get the current date and time
            DateTime now = DateTime.Now;

            // Get the offset for the current time in Greece
            Offset = greeceTimeZone.GetUtcOffset(now);

            return Offset;
        }

        /// <summary>
        /// Calculates a <see cref="DateTimeOffset"/> from the specified <paramref name="date"/> and <paramref name="time"/>
        /// </summary>
        /// <param name="date">The date</param>
        /// <param name="time">The time</param>
        /// <returns></returns>
        public static DateTimeOffset CalculateDateTimeOffset(DateTimeOffset date, TimeOnly time)
        {
            if (Offset is null)
                CalculateOffset();

            return new DateTimeOffset(date.Year, date.Month, date.Day, time.Hour, time.Minute, time.Second, Offset!.Value);
        }

        #endregion
    }
}
