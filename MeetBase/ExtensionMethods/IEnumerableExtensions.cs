using System.Collections;
using System.Diagnostics.CodeAnalysis;

namespace MeetBase
{
    /// <summary>
    /// Extension methods associated with <see cref="IEnumerable"/> and <see cref="IEnumerable{T}"/>
    /// </summary>
    public static class IEnumerableExtensions
    {
        #region Public Methods

        /// <summary>
        /// One-line for each
        /// </summary>
        /// <typeparam name="T">The type of the items</typeparam>
        /// <param name="source">The source</param>
        /// <param name="action">The action to run</param>
        public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
        {
            foreach (var item in source)
                action(item);
        }

        /// <summary>
        /// Checks if the specified <paramref name="enumerable"/> is null or 
        /// if it doesn't have any items.
        /// </summary>
        /// <param name="enumerable">The enumerable</param>
        /// <returns></returns>
        public static bool IsNullOrEmpty([NotNullWhen(false)] this IEnumerable? enumerable)
        {
            if (enumerable is null)
                return true;

            foreach (var _ in enumerable)
                return false;

            return true;
        }

        /// <summary>
        /// Aggregates the specified <paramref name="source"/> into a <see cref="string"/>
        /// </summary>
        /// <typeparam name="T">The type of the items</typeparam>
        /// <param name="source">The source</param>
        /// <param name="extractor">The string extractor that is used to convert an item to a string</param>
        /// <param name="func">Delegate that places the extracted strings in order</param>
        /// <returns></returns>
        public static string AggregateString<T>(this IEnumerable<T> source, Func<T, string> extractor, Func<string, string, string> func)
        {
            var result = string.Empty;

            foreach (var item in source)
            {
                if (result == string.Empty)
                {
                    result = extractor(item);
                    continue;
                }

                result = func(result, extractor(item));
            }

            return result;
        }

        /// <summary>
        /// Aggregates the specified <paramref name="source"/> into a <see cref="string"/>
        /// </summary>
        /// <typeparam name="T">The type of the items</typeparam>
        /// <param name="source">The source</param>
        /// <param name="extractor">The string extractor that is used to convert an item to a string</param>
        /// <param name="separator">The separator</param>
        /// <returns></returns>
        public static string AggregateString<T>(this IEnumerable<T> source, Func<T, string> extractor, string separator = ", ")
            => AggregateString(source, extractor, (value1, value2) => value1 + separator + value2);

        /// <summary>
        /// Aggregates the specified <paramref name="source"/> into a <see cref="string"/>.
        /// NOTE: This method converts the items of the <paramref name="source"/> to <see cref="string"/>
        ///       using the <see cref="object.ToString()"/> method!
        /// </summary>
        /// <typeparam name="T">The type of the items</typeparam>
        /// <param name="source">The source</param>
        /// <param name="func">Delegate that places the extracted strings in order</param>
        /// <returns></returns>
        public static string AggregateString<T>(this IEnumerable<T> source, Func<string, string, string> func)
            => AggregateString(source, item => item?.ToString() ?? string.Empty, func);


        /// <summary>
        /// Aggregates the specified <paramref name="source"/> into a <see cref="string"/>.
        /// NOTE: This method converts the items of the <paramref name="source"/> to <see cref="string"/>
        ///       using the <see cref="object.ToString()"/> method!
        /// NOTE: This method uses ", " as the items separator!
        /// </summary>
        /// <typeparam name="T">The type of the items</typeparam>
        /// <param name="source">The source</param>
        /// <param name="separator">The separator</param>
        /// <returns></returns>
        public static string AggregateString<T>(this IEnumerable<T> source, string separator = ", ")
            => source.AggregateString((s1, s2) => s1 + separator + s2);


        #endregion
    }
}
