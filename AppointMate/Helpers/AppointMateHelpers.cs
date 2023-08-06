using Microsoft.AspNetCore.Components.Forms;

namespace AppointMate
{
    /// <summary>
    /// Helper methods
    /// </summary>
    public static class AppointMateHelpers
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
    }
}
