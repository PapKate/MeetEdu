using Quartz;

namespace MeetBase
{
    /// <summary>
    /// Extension methods for <see cref="Quartz"/>
    /// </summary>
    public static class QuartzExtensions
    {
        #region Public Methods

        /// <summary>
        /// Merges the rangeables that are overlapping to each other and creates new rangeables.
        /// Input: 2     5      10     15
        ///        |-----|       |-----|
        ///            3     8         15    20
        ///            |-----|         |-----|
        /// Output: 2         8 10           20
        ///         |---------|  |-----------|
        /// </summary>
        /// <typeparam name="TRangeable">The type of the rangeable</typeparam>
        /// <typeparam name="T">The type of the values of the rangeables</typeparam>
        /// <param name="source">The rangeables to merge</param>
        /// <param name="implementationFactory">Creates and returns a rangeable using the specified minimum and maximum values</param>
        /// <returns></returns>
        public static IEnumerable<TRangeable> MergeOverlapping<TRangeable, T>(this IEnumerable<TRangeable> source, Func<TRangeable, TRangeable, T, T, TRangeable> implementationFactory)
            where TRangeable : IReadOnlyRangeable<T>
            where T : IComparable<T>
        {
            if (source.IsNullOrEmpty())
                return Enumerable.Empty<TRangeable>();

            source = source.OrderBy(x => x.Minimum).ToList();

            var result = new List<TRangeable>();

            var previousRangeable = source.First();
            foreach (var nextRangeable in source.Skip(1))
            {
                if (!previousRangeable.OverlapsWith(nextRangeable))
                {
                    result.Add(previousRangeable);
                    previousRangeable = nextRangeable;
                }
                else
                    previousRangeable = previousRangeable.MergeWith<TRangeable, T>(nextRangeable, (min, max) => implementationFactory(previousRangeable, nextRangeable, min, max));
            }

            result.Add(previousRangeable);

            return result;
        }

        /// <summary>
        /// Determines if the specified <paramref name="second"/> rangeable is overlapping with the <paramref name="first"/> rangeable.
        /// A range overlaps with another range either if they intersect or if the one range is contained in the other.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="first">The first rangeable</param>
        /// <param name="second">The second rangeable</param>
        /// <returns></returns>
        public static bool OverlapsWith<T>(this IReadOnlyRangeable<T> first, IReadOnlyRangeable<T> second)
            where T : IComparable<T>
            => first.Contains(second) || second.Contains(first) || first.IntersectsWith(second);

        /// <summary>
        /// Determines if the specified <paramref name="second"/> rangeable is inside the bounds of the <paramref name="first"/> rangeable.
        /// </summary>
        /// <param name="first">The first rangeable</param>
        /// <param name="second">The second rangeable</param>
        public static bool Contains<T>(this IReadOnlyRangeable<T> first, IReadOnlyRangeable<T> second)
            where T : IComparable<T>
            => first.ContainsValue(second.Minimum) && first.ContainsValue(second.Maximum);

        /// <summary>
        /// Determines if the specified <paramref name="second"/> rangeable intersects with the <paramref name="first"/> rangeable.
        /// </summary>
        /// <param name="first">The first rangeable</param>
        /// <param name="second">The second rangeable</param>
        /// <returns></returns>
        public static bool IntersectsWith<T>(this IReadOnlyRangeable<T> first, IReadOnlyRangeable<T> second)
            where T : IComparable<T>
            => (first.ContainsValue(second.Minimum) && !first.ContainsValue(second.Maximum)) || (first.ContainsValue(second.Maximum) && !first.ContainsValue(second.Minimum));
        /// <summary>
        /// Determines if the provided value is inside the <paramref name="rangeable"/>.
        /// </summary>
        /// <param name="rangeable">The rangeable</param>
        /// <param name="value">The range to test</param>
        public static bool ContainsValue<T>(this IReadOnlyRangeable<T> rangeable, T value)
            where T : IComparable<T>
            => (rangeable.Minimum.CompareTo(value) <= 0) && (value.CompareTo(rangeable.Maximum) <= 0);

        /// <summary>
        /// Merges the <paramref name="first"/> rangeable with the <paramref name="second"/> rangeable.
        /// </summary>
        /// <typeparam name="TRangeable">The type of the rangeable</typeparam>
        /// <typeparam name="T">The </typeparam>
        /// <param name="first">The first rangeable</param>
        /// <param name="second">The second rangeable</param>
        /// <param name="implementationFactory">Creates and returns a rangeable using the specified minimum and maximum values</param>
        /// <returns></returns>
        public static TRangeable MergeWith<TRangeable, T>(this IReadOnlyRangeable<T> first, IReadOnlyRangeable<T> second, Func<T, T, TRangeable> implementationFactory)
            where TRangeable : IReadOnlyRangeable<T>
            where T : IComparable<T>
        {
            return implementationFactory(first.GetMin(second), first.GetMax(second));
        }

        /// <summary>
        /// Gets the minimum value between the specified rangeables
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="first">The first rangeable</param>
        /// <param name="second">The second rangeable</param>
        /// <returns></returns>
        public static T GetMin<T>(this IReadOnlyRangeable<T> first, IReadOnlyRangeable<T> second)
            where T : IComparable<T>
        {
            if (first.Minimum.CompareTo(second.Minimum) <= 0)
                return first.Minimum;

            return second.Minimum;
        }

        /// <summary>
        /// Gets the maximum value between the specified rangeables
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="first">The first rangeable</param>
        /// <param name="second">The second rangeable</param>
        /// <returns></returns>
        public static T GetMax<T>(this IReadOnlyRangeable<T> first, IReadOnlyRangeable<T> second)
            where T : IComparable<T>
        {
            if (first.Maximum.CompareTo(second.Maximum) >= 0)
                return first.Maximum;

            return second.Maximum;
        }

        /// <summary>
        /// Subtracts the <paramref name="second"/> rangeable from the <paramref name="first"/> one
        /// </summary>
        /// <typeparam name="TRangeable">The type of the rangeable</typeparam>
        /// <typeparam name="T">The type of the values of the rangeables</typeparam>
        /// <param name="first">The first rangeable</param>
        /// <param name="second">The second rangeable</param>
        /// <param name="implementationFactory">Creates and returns a rangeable using the specified minimum and maximum values</param>
        /// <returns></returns>
        public static IEnumerable<TRangeable> Subtract<TRangeable, T>(this TRangeable first, TRangeable second, Func<T, T, TRangeable> implementationFactory)
            where TRangeable : IReadOnlyRangeable<T>
            where T : IComparable<T>
        {
            // If the spans aren't intersected...
            if (!first.OverlapsWith(second))
                // Return the current span
                return new List<TRangeable>() { first };

            // Declare a list for the results
            var results = new List<TRangeable>();

            // If the current start is after the span start...
            if (first.Minimum.CompareTo(second.Minimum) < 0)
                // Add the first part of the span
                results.Add(implementationFactory(first.Minimum, second.Minimum));

            // If the current end is after the span end...
            if (first.Maximum.CompareTo(second.Maximum) > 0)
                // Add the latter part of the span
                results.Add(implementationFactory(second.Maximum, first.Maximum));

            // Return the results
            return results;
        }

        /// <summary>
        /// Gets all the execution dates between the <paramref name="from"/> and the <paramref name="to"/> day span
        /// using the specified <paramref name="cronExpression"/>
        /// </summary>
        /// <param name="cronExpression">The CRON expression</param>
        /// <param name="from">The from date</param>
        /// <param name="to">The to date</param>
        /// <returns></returns>
        public static IEnumerable<DateTimeOffset> GetRecurrences(this CronExpression cronExpression, DateTimeOffset from, DateTimeOffset to)
        {
            // Create the result
            var result = new List<DateTimeOffset>();

            from = from.AddMilliseconds(-1);

            while (from <= to)
            {
                // Get the next fire time
                var date = cronExpression.GetNextValidTimeAfter(from);

                // If there is a date and it is before the ending date...
                if (date.HasValue && date.Value <= to)
                {
                    // Add it to the result
                    result.Add(date.Value);

                    // Update the from variable
                    from = date.Value;

                    // Continue
                    continue;
                }

                // Break
                break;
            }

            // Return the result
            return result;
        }

        #endregion
    }
}
