using System.Globalization;
using System;

namespace MeetBase
{
    /// <summary>
    /// Extension methods for <see cref="DateTime"/>
    /// </summary>
    public static class DateTimeExtensions
    {
        #region Public Methods

        /// <summary>
        /// Returns a string that represents the specified <paramref name="dt"/> using the ISO8601 format
        /// </summary>
        /// <param name="dt">The date time</param>
        /// <returns></returns>
        public static string ToISO8601String(this DateTime dt) => dt.ToString(LocalizationConstants.ISO8601Format, LocalizationConstants.Culture);

        /// <summary>
        /// Returns a string that represents the specified <paramref name="dto"/> using the ISO8601 format
        /// </summary>
        /// <param name="dto">The date time offset</param>
        /// <returns></returns>
        public static string ToISO8601String(this DateTimeOffset dto) => dto.LocalDateTime.ToString(LocalizationConstants.ISO8601Format, LocalizationConstants.Culture);

        /// <summary>
        /// Create and return the <see cref="DateTime"/> from the specified <paramref name="value"/>
        /// using the 00:00:00:00 as the time value
        /// </summary>
        /// <param name="value">The date value</param>
        /// <returns></returns>
        public static DateOnly ToDateOnly(this DateTime value)
            => DateOnly.FromDateTime(value);

        /// <summary>
        /// Returns the first day of the week of the specified <paramref name="date"/>
        /// </summary>
        /// <param name="date">The date</param>
        /// <returns></returns>
        /// <remarks>As first date: <c><see cref="DayOfWeek.Monday"/></c></remarks>
        public static DateTime GetFirstDayOfWeek(this DateTime date)
        {
            var currentDayOfWeek = date.DayOfWeek;

            if (currentDayOfWeek == DayOfWeek.Sunday)
                return date.AddDays(-(int)date.DayOfWeek - 1);

            return date.AddDays(-(int)date.DayOfWeek + 1);
        }

        /// <summary>
        /// Returns the full name of the month of the specified <paramref name="date"/>
        /// </summary>
        /// <param name="date">The date</param>
        /// <returns></returns>
        public static string GetMonthName(this DateTime date)
        {
            return CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(date.Month);
        }

        #endregion
    }
}
