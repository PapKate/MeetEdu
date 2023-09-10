using System.Drawing;

using static MeetBase.Blazor.PaletteColors;

namespace MeetBase.Blazor
{
    /// <summary>
    /// Extensions for <see cref="Color"/>
    /// </summary>
    public static class ColorExtensions
    {
        #region Public Methods

        /// <summary>
        /// Returns the value with a # added if it is a hex color or else the value
        /// </summary>
        /// <param name="value">The value</param>
        /// <returns></returns>
        public static string NormalizedColor(this string value)
        {
            return value.IsHexValue() ? $"#{value}" : value;
        }

        /// <summary>
        /// Converts a hexadecimal value to an RGB and sets an opacity for the color.
        /// </summary>
        /// <param name="hex"> A hex value of a color</param>
        /// <param name="alpha">The desired opacity percentage</param>
        /// <returns></returns>
        public static string HexToRGBA(this string hex, double alpha)
        {
            // Takes the first two characters of the hex value and converts them to an integer
            var r = Convert.ToInt32(hex.Substring(0, 2), 16);
            // Takes the second two characters of the hex value and converts them to an integer
            var g = Convert.ToInt32(hex.Substring(2, 2), 16);
            // Takes the last two characters of the hex value and converts them to an integer
            var b = Convert.ToInt32(hex.Substring(4, 2), 16);

            // Returns a string that is the hex value in RGB and sets the opacity percentage
            return $"rgba({r}, {g}, {b}, {alpha.ToString(LocalizationConstants.Culture)})";
        }

        /// <summary>
        /// Perceives the brightness of the <paramref name="c"/> using a special formula
        /// </summary>
        /// <param name="c">The color to perceive its brightness</param>
        /// <returns></returns>
        public static int PerceivedBrightness(this Color c)
            => (int)Math.Sqrt(c.R * c.R * .241 +
                              c.G * c.G * .691 +
                              c.B * c.B * .068);

        /// <summary>
        /// Selects between black and white, the more fitting color to contrast the given <paramref name="c"/>
        /// </summary>
        /// <param name="c">The color to find the more fitting contrast</param>
        /// <returns></returns>
        public static Color DarkOrWhite(this Color c) => c.PerceivedBrightness() > 192 ? DarkGray.ToColor() : Color.White;

        /// <summary>
        /// Darkens the given <paramref name="c"/>
        /// </summary>
        /// <param name="c">The color to darken</param>
        /// <param name="level">The darken level</param>
        /// <returns></returns>
        public static Color ToDarkerColor(this Color c, uint level = 1)
        {
            var r = ChangeColorComponent(c.R, true, level);
            var g = ChangeColorComponent(c.G, true, level);
            var b = ChangeColorComponent(c.B, true, level);

            return Color.FromArgb(byte.MaxValue, r, g, b);
        }

        /// <summary>
        /// Lightens the given <paramref name="c"/>
        /// </summary>
        /// <param name="c">The color to lighten</param>
        /// <param name="level">The darken level</param>
        /// <returns></returns>
        public static Color ToLighterColor(this Color c, uint level = 1)
        {
            var r = ChangeColorComponent(c.R, false, level);
            var g = ChangeColorComponent(c.G, false, level);
            var b = ChangeColorComponent(c.B, false, level);

            return Color.FromArgb(byte.MaxValue, r, g, b);
        }

        /// <summary>
        /// Gets hex value of the specified <paramref name="c"/>
        /// </summary>
        /// <param name="c">The color</param>
        /// <param name="returnHash">Whether to place "#" in front of the hex value</param>
        /// <returns></returns>
        public static string ToHex(this Color c, bool returnHash = false)
            => (returnHash ? "#" : "") + c.R.ToString("X2") + c.G.ToString("X2") + c.B.ToString("X2");

        /// <summary>
        /// Gets the HUE from the specified <paramref name="c"/>
        /// </summary>
        /// <param name="c">The color</param>
        /// <returns></returns>
        public static float GetHue(this Color c) =>
            c.GetHue();

        /// <summary>
        /// Gets the brightness from the specified <paramref name="c"/>
        /// </summary>
        /// <param name="c">The color</param>
        /// <returns></returns>
        public static float GetBrightness(this Color c) =>
          c.GetBrightness();

        /// <summary>
        /// Gets the saturation from the specified <paramref name="c"/>
        /// </summary>
        /// <param name="c">The color</param>
        /// <returns></returns>
        public static float GetSaturation(this Color c) =>
          c.GetSaturation();

        #endregion

        #region Private Methods

        /// <summary>
        /// Changes the given <paramref name="colorComponent"/> based on the <paramref name="level"/>
        /// </summary>
        /// <param name="colorComponent">The color component to change</param>
        /// <param name="darker">A flag indicating whether the color should become darker</param>
        /// <param name="level">The level of the change</param>
        /// <returns></returns>
        private static byte ChangeColorComponent(int colorComponent, bool darker, uint level)
        {
            var result = darker ? colorComponent - (15 * level) : colorComponent + (15 * level);

            if (result > byte.MaxValue)
                return byte.MaxValue;
            else if (result < 0)
                return 0;

            return (byte)result;
        }

        #endregion
    }
}
