using Ical.Net.Serialization;

namespace MeetBase
{
    /// <summary>
    /// Provides extension methods for <see cref="Ical.Net.Calendar"/>
    /// </summary>
    public static class CalendarExtensions
    {
        #region Public Methods

        /// <summary>
        /// Gets the calendar events of the specified <paramref name="calendar"/>
        /// </summary>
        /// <param name="calendar">The calendar</param>
        /// <returns></returns>
        public static string GetCalendarEvents(this Ical.Net.Calendar calendar)
        {
            var iCalSerializer = new CalendarSerializer();
            string result = iCalSerializer.SerializeToString(calendar);

            return result;
        }

        #endregion
    }
}
