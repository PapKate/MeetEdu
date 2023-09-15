using System.Drawing;
using System.Globalization;
using System.Text.RegularExpressions;

using static MeetBase.Blazor.PaletteColors;

namespace MeetBase.Blazor
{
    /// <summary>
    /// Extension method for <see cref="string"/>
    /// </summary>
    public static class StringExtensions
    {
        #region Colors

        /// <summary>
        /// Converts the specific <paramref name="s"/> to a <see cref="Color"/>
        /// </summary>
        /// <param name="s">The string to convert to color</param>
        /// <returns></returns>
        public static Color ToColor(this string s)
            => s == null || s.ToLower() == "transparent" ? Color.FromArgb(0, 0, 0, 0) : Color.FromArgb(int.Parse(s.Replace("#", ""), NumberStyles.AllowHexSpecifier));

        /// <summary>
        /// Selects between black and white, the more fitting color to contrast the given <paramref name="s"/>
        /// </summary>
        /// <param name="s">The color to find the more fitting contrast</param>
        /// <returns></returns>
        public static string DarkOrWhite(this string? s)
            => s.IsNullOrEmpty() ? DarkGray : s.ToColor().DarkOrWhite().ToHex();

        public static string ToLighterColor(this string s, uint level) => s.ToColor().ToLighterColor(level).ToHex();
        public static string ToDarkerColor(this string s, uint level) => s.ToColor().ToDarkerColor(level).ToHex();

        /// <summary>
        /// Checks if the <paramref name="value"/> represents a hexadecimal value
        /// </summary>
        /// <param name="value">The value</param>
        /// <returns></returns>
        public static bool IsHexValue(this string? value)
        {
            if (value.IsNullOrEmpty())
                return false;
            // For C-style hex notation (0xFF) you can use @"\A\b(0[xX])?[0-9a-fA-F]+\b\Z"
            return Regex.IsMatch(value, @"\A\b[0-9a-fA-F]+\b\Z");
        }

        #endregion

        #region Day Of Week

        /// <summary>
        /// Gets the short formated name of the day of the week
        /// </summary>
        /// <param name="dayOfWeek">The day of the week</param>
        /// <returns></returns>
        public static string ToShortDayOfWeekName(this DayOfWeek dayOfWeek)
        {
            return dayOfWeek.ToString().Substring(0, 3);
        }




        #endregion
    }
}
