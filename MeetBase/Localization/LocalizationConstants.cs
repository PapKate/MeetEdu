using System.Globalization;

namespace MeetBase
{
    /// <summary>
    /// Constants related to localization
    /// </summary>
    public static class LocalizationConstants
    {
        /// <summary>
        /// The culture info that is used by the ToString methods
        /// </summary>
        public static readonly CultureInfo Culture = CultureInfo.InvariantCulture;

        /// <summary>
        /// The format that is used when represent values using the ISO8601 standard
        /// </summary>
        public const string ISO8601Format = "o";
    }
}
