using Quartz;

namespace MeetBase
{
    /// <summary>
    /// Helper methods for <see cref="Quartz"/>
    /// </summary>
    public static class QuartzHelpers
    {
        #region Public Methods

        /// <summary>
        /// Calculates the available appointment slots
        /// </summary>
        /// <param name="from">The start date</param>
        /// <param name="to">The end date</param>
        /// <param name="weeklySchedule">The schedule</param>
        /// <param name="duration">The duration of the appointment</param>
        /// <param name="startMinutes">The minutes that the appointment can start</param>
        /// <param name="reservedTimeSlots"></param>
        /// <returns></returns>
        public static IEnumerable<IReadOnlyRangeable<DateTimeOffset>> CalculateAppointmentDates(
            DateTimeOffset from,
            DateTimeOffset to,
            IEnumerable<DayOfWeekTimeRange> weeklySchedule,
            TimeSpan duration,
            IEnumerable<int> startMinutes,
            IEnumerable<IReadOnlyRangeable<DateTimeOffset>> reservedTimeSlots)
        {
            if (to < from)
                return Enumerable.Empty<IReadOnlyRangeable<DateTimeOffset>>();

            var invalidSpans = new List<IReadOnlyRangeable<DateTimeOffset>>() { new Range<DateTimeOffset>(from, to) };

            // Add according to the weekly schedule the ranges that are NOT in the weekly schedule
            foreach (var schedule in weeklySchedule)
            {
                var validSpans = GetDateTimeOffsetRange(from, to, schedule);

                foreach (var validSpan in validSpans)
                {
                    // Get the intersected spans
                    var intersectedSpans = invalidSpans.Where(x => x.OverlapsWith(validSpan)).ToList();

                    // For every intersect...
                    foreach (var intersect in intersectedSpans)
                    {
                        // Remove the previous intersect
                        invalidSpans.Remove(intersect);

                        // Add the new subtracted spans
                        invalidSpans.AddRange(intersect.Subtract<IReadOnlyRangeable<DateTimeOffset>, DateTimeOffset>(validSpan, (min, max)
                            => new Range<DateTimeOffset>(min, max)));
                    }
                }
            }

            invalidSpans.AddRange(reservedTimeSlots);

            // Merge the invalid spans
            invalidSpans = invalidSpans
                .Select(x => (IReadOnlyRangeable<DateTimeOffset>)new Range<DateTimeOffset>(x.Minimum.AddSeconds(1), x.Maximum.AddSeconds(-1)))
                .Where(x => !x.Minimum.Equals(x.Maximum))
                .MergeOverlapping<IReadOnlyRangeable<DateTimeOffset>, DateTimeOffset>((first, second, min, max) => new Range<DateTimeOffset>(min, max))
                .ToList();

            startMinutes = startMinutes.Distinct().Order().Where(x => x >= 0 && x <= 59).ToList();
            var cronExpression = new CronExpression($"0 {(startMinutes.IsNullOrEmpty() ? "*" : startMinutes.AggregateString(","))} * ? * * *");

            return cronExpression.GetRecurrences(from, to)
                .Distinct()
                .Select(x => (IReadOnlyRangeable<DateTimeOffset>)new Range<DateTimeOffset>(x, x.Add(duration)))
                .Where(x => !invalidSpans.Any(y => y.OverlapsWith(x)))
                .ToList();
        }

        /// <summary>
        /// Calculates the available appointment slots
        /// </summary>
        /// <param name="date"></param>
        /// <param name="from">The start date</param>
        /// <param name="to">The end date</param>
        /// <param name="weeklySchedule">The schedule</param>
        /// <param name="duration">The duration of the appointment</param>
        /// <param name="startMinutes">The minutes that the appointment can start</param>
        /// <param name="invalidSpans">The invalid time slots</param>
        /// <returns></returns>
        public static bool IsAppointmentDateValid(
            DateTimeOffset date,
            DateTimeOffset from,
            DateTimeOffset to,
            IEnumerable<DayOfWeekTimeRange> weeklySchedule,
            TimeSpan duration,
            IEnumerable<int> startMinutes,
            List<IReadOnlyRangeable<DateTimeOffset>> invalidSpans)
        {
            // Gets the possible valid time slots for an appointment
            var dates = CalculateAppointmentDates(from, to, weeklySchedule, duration, startMinutes, invalidSpans);

            // Gets the date chem range of the appointment
            var appointmentRange = new Range<DateTimeOffset>(date, date + duration);

            // If the possible time slots contain the appointment date...
            if (dates.Contains(appointmentRange))
                // Returns true
                return true;

            // Returns false
            return false;
        }

        /// <summary>
        /// Converts a <paramref name="value"/> to a list of ranges according to the specified <paramref name="from"/> and <paramref name="to"/> dates
        /// </summary>
        /// <param name="from">The start date</param>
        /// <param name="to">The end date</param>
        /// <param name="value">The schedule</param>
        /// <returns></returns>
        public static IEnumerable<Range<DateTimeOffset>> GetDateTimeOffsetRange(DateTimeOffset from, DateTimeOffset to, DayOfWeekTimeRange value)
        {
            var weeklyDays = Enumerable.Range(0, (to - from).Days + 1)
                                    .Select(offset => from.AddDays(offset))
                                    .Where(date => date.DayOfWeek == value.DayOfWeek);

            var result = new List<Range<DateTimeOffset>>();

            foreach (var day in weeklyDays)
            {
                var range = new Range<DateTimeOffset>(DateTimeOffsetHelpers.CalculateDateTimeOffset(day, value.Start), DateTimeOffsetHelpers.CalculateDateTimeOffset(day, value.End));
                result.Add(range);
            }

            return result;
        }

        #endregion
    }
}
