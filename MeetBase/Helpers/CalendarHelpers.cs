using Ical.Net.CalendarComponents;
using Ical.Net.DataTypes;

namespace MeetBase
{
    /// <summary>
    /// Provides helper methods for <see cref="Ical.Net"/>
    /// </summary>
    public static class CalendarHelpers
    {
        #region Public Methods

        /// <summary>
        /// Creates an new <see cref="CalendarEvent"/>
        /// </summary>
        /// <param name="title">The title</param>
        /// <param name="description">The description</param>
        /// <param name="dateTime">The date and time</param>
        /// <param name="duration">The duration</param>
        /// <returns></returns>
        public static CalendarEvent CreateCalendarEvent(string title, string? description, DateTime dateTime, TimeSpan duration)
        {
            var icalEvent = new CalendarEvent
            {
                Summary = title,
                Description = description,
                Start = new CalDateTime(dateTime),
                End = new CalDateTime(dateTime + duration)
            };

            return icalEvent;
        }

        /// <summary>
        /// Creates an new <see cref="CalendarEvent"/> and returns the JSON serialized version
        /// </summary>
        /// <param name="title">The title</param>
        /// <param name="description">The description</param>
        /// <param name="dateTime">The date and time</param>
        /// <param name="duration">The duration</param>
        /// <returns></returns>
        public static string GenerateCalendarEvent(string title, string? description, DateTime dateTime, TimeSpan duration)
        {
            var calendar = new Ical.Net.Calendar();
            var icalEvent = new CalendarEvent
            {
                Summary = title,
                Description = description,
                Start = new CalDateTime(dateTime),
                End = new CalDateTime(dateTime + duration)
            };

            calendar.Events.Add(icalEvent);

            return calendar.GetCalendarEvents();
        }

        #endregion
    }
}
