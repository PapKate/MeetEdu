namespace MeetBase
{
    /// <summary>
    /// Extensions methods for <see cref="Guid"/>
    /// </summary>
    public static class GuidExtensions
    {
        /// <summary>
        /// Returns a string that represents the specified <paramref name="guid"/>
        /// that contains only alphanumeric characters
        /// </summary>
        /// <param name="guid">The guid</param>
        /// <returns></returns>
        public static string ToNormalizedString(this Guid guid) => guid.ToString("N");

        /// <summary>
        /// Returns a string that represents the specified <paramref name="guid"/>
        /// that contains only alphanumeric characters and has a specific length
        /// </summary>
        /// <param name="guid">The guid</param>
        /// <param name="length">The length</param>
        /// <returns></returns>
        public static string ToNormalizedString(this Guid guid, int length) => guid.ToNormalizedString().Substring(0, length);

        /// <summary>
        /// Returns a string that represents the specified <paramref name="guid"/>
        /// that contains only Latin characters
        /// </summary>
        /// <param name="guid">The guid</param>
        /// <returns></returns>
        public static string ToLatinOnlyString(this Guid guid) => string.Concat(guid.ToString("N").Select(c => (char)(c + 17)));

        /// <summary>
        /// Returns a string that represents the specified <paramref name="guid"/>
        /// that contains only Latin characters and has a specific length
        /// </summary>
        /// <param name="guid">The guid</param>
        /// <param name="length">The length</param>
        /// <returns></returns>
        public static string ToLatinOnlyString(this Guid guid, int length) => guid.ToLatinOnlyString().Substring(0, length);
    }
}
