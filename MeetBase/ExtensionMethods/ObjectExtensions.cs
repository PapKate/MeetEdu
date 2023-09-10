namespace MeetBase
{
    /// <summary>
    /// Extension methods for <see cref="object"/>
    /// </summary>
    public static class ObjectExtensions
    {
        #region Public Methods

        /// <summary>
        /// Casts and returns the given object to the given type
        /// </summary>
        /// <typeparam name="T">The given type</typeparam>
        /// <param name="obj">The given object</param>
        /// <returns></returns>
        public static T CastTo<T>(this object obj) => (T)obj;

        /// <summary>
        /// Returns a string that represents the specified <paramref name="obj"/> and a <see cref="string.Empty"/> if the <paramref name="obj"/> is null
        /// </summary>
        /// <param name="obj">The object</param>
        /// <param name="fallBackValue">The value that is returned when the <paramref name="obj"/> is null</param>
        /// <returns></returns>
        public static string ToNonNullString(this object? obj, string fallBackValue = "")
        {
            if (obj is null)
                return fallBackValue;

            return obj.ToString() ?? fallBackValue;
        }

        /// <summary>
        /// Returns the string that represents the specified <paramref name="value"/>
        /// and null if there isn't a value
        /// </summary>
        /// <typeparam name="T">The type of the nullable struct</typeparam>
        /// <param name="value">The value</param>
        /// <param name="fallBackValue">The value that is returned when the <paramref name="value"/> is null</param>
        /// <returns></returns>
        public static string ToNonNullString<T>(this T? value, string fallBackValue = "")
            where T : struct
        {
            if (value.HasValue)
                return value.ToString() ?? fallBackValue;

            return fallBackValue;
        }

        #endregion
    }
}
