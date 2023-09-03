using System.Text.RegularExpressions;

namespace MeetBase
{
    /// <summary>
    /// Helper methods
    /// </summary>
    public static class MeetBaseHelpers
    {
        /// <summary>
        /// Gets the first day of the week date from the specified <paramref name="date"/>
        /// </summary>
        /// <param name="date">The date</param>
        /// <returns></returns>
        public static DateOnly GetFirstDayOfWeekDate(this DateTimeOffset date)
        {
            var daysUntilMonday = ((int)date.DayOfWeek - (int)DayOfWeek.Monday + 7) % 7;
            var firstDateOfWeek = date.AddDays(-daysUntilMonday);
            return DateOnly.FromDateTime(firstDateOfWeek.Date);
        }

        /// <summary>
        /// Splits the specified <paramref name="value"/> by uppercase 
        /// </summary>
        /// <param name="value">The value</param>
        /// <returns></returns>
        public static List<string> SplitCamelCase(string value)
        {
            var upperCaseRegx = @"([A-Z]?[a-z]+)";

            return Regex.Split(value, upperCaseRegx).Where(str => !string.IsNullOrEmpty(str)).ToList();
        }
    }
}
