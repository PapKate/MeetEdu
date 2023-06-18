using System.Collections;
using System.Diagnostics.CodeAnalysis;

namespace AppointMate
{
    /// <summary>
    /// Extension methods associated with <see cref="IEnumerable"/> and <see cref="IEnumerable{T}"/>
    /// </summary>
    public static class IEnumerableExtensions
    {
        #region Public Methods

        /// <summary>
        /// Checks if the specified <paramref name="enumerable"/> is <see cref="null"/> or 
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

        #endregion
    }
}
