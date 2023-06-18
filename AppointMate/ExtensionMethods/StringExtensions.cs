using MongoDB.Bson;

using System.Diagnostics.CodeAnalysis;

namespace AppointMate
{
    /// <summary>
    /// Extension methods for strings
    /// </summary>
    public static class StringExtensions
    {
        #region Public Methods

        /// <summary>
        /// Checks whether the string is null or empty
        /// </summary>
        /// <param name="value">The value</param>
        /// <returns>Returns true if the string is null or empty, false otherwise</returns>
        public static bool IsNullOrEmpty([NotNullWhen(false)] this string? value)
            => string.IsNullOrEmpty(value);

        /// <summary>
        /// Returns the specified <paramref name="value"/> if it's not null or empty,
        /// otherwise it throws an <see cref="ArgumentNullException"/>
        /// </summary>
        /// <param name="value">The value</param>
        /// <returns></returns>
        public static string NotNullOrEmpty(this string? value)
        {
            if (value.IsNullOrEmpty())
                throw new ArgumentNullException(nameof(value));

            return value;
        }

        /// <summary>
        /// Converts the specified <paramref name="s"/> to an <see cref="ObjectId"/>
        /// </summary>
        /// <param name="s">The string to convert</param>
        /// <returns></returns>
        public static ObjectId ToObjectId(this string s) => new ObjectId(s);

        #endregion
    }
}
