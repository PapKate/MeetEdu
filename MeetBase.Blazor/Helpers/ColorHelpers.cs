using System.Drawing;

using static MeetBase.Blazor.PaletteColors;

namespace MeetBase.Blazor
{
    /// <summary>
    /// Helper methods related to colors
    /// </summary>
    public static class ColorHelpers
    {
        #region Private Members

        /// <summary>
        /// Used for generating random colors
        /// </summary>
        private static readonly Random mRandom = new();

        #endregion

        #region Public Methods

        /// <summary>
        /// Generates a random color
        /// </summary>
        /// <returns></returns>
        public static Color RandomColor()
            => Color.FromArgb(mRandom.Next(256), mRandom.Next(256), mRandom.Next(256));

        /// <summary>
        /// Generates a color that represents the specified <paramref name="value"/>
        /// </summary>
        /// <param name="value">The value</param>
        /// <returns></returns>
        public static string FromString(string? value)
        {
            if (value.IsNullOrEmpty())
                return DarkGray;
            var number = FashHash(value);

            return (number % 16777215).ToString("X").PadLeft(6, 'F');
        }

        /// <summary>
        /// Generates a color based on the specified <paramref name="colors"/> the <paramref name="minValue"/>, the <paramref name="maxValue"/>
        /// and the <paramref name="value"/>
        /// </summary>
        /// <param name="value">The value</param>
        /// <param name="minValue">The minimum value</param>
        /// <param name="maxValue">The maximum value</param>
        /// <param name="colors">The colors</param>
        /// <param name="fallbackColor">The fall-back color</param>
        /// <returns></returns>
        public static Color FromColorsRange(int? value, int minValue, int maxValue, IEnumerable<Color> colors, Color fallbackColor)
        {
            static byte Interpolate(byte a, byte b, double p)
            {
                return (byte)(a * (1 - p) + b * p);
            }

            if (!value.HasValue)
                return fallbackColor;

            var numberOfColors = colors.Count();

            var range = maxValue - minValue;
            var step = (double)range / numberOfColors;

            var d = new SortedDictionary<int, Color>();
            for (var i = 0; i <= numberOfColors - 1; i++)
            {
                var color = colors.ElementAt(i);
                if (i == numberOfColors - 1)
                {
                    d.Add(maxValue, color);

                    break;
                }

                d.Add((int)(i * step), color);
            }

            var kvp_previous = new KeyValuePair<int, Color>(-1, fallbackColor);
            foreach (var pair in d)
            {
                if (pair.Key > value)
                {
                    var p = (value.Value - kvp_previous.Key) / (double)(pair.Key - kvp_previous.Key);
                    Color a = kvp_previous.Value;
                    Color b = pair.Value;
                    var c = Color.FromArgb(Interpolate(a.R, b.R, p), Interpolate(a.G, b.G, p), Interpolate(a.B, b.B, p));
                    return c;
                }
                kvp_previous = pair;
            }

            return fallbackColor;
        }

        /// <summary>
        /// Generates a color based on the specified <paramref name="minValue"/>, the <paramref name="maxValue"/> and the <paramref name="value"/>.
        /// </summary>
        /// <param name="value">The value</param>
        /// <param name="minValue">The min value</param>
        /// <param name="maxValue">The max value</param>
        /// <returns></returns>
        public static Color FromColorsRange(int? value, int minValue, int maxValue)
            => FromColorsRange(value, minValue, maxValue, new List<Color>() { FromHex(Blue), FromHex(Orange), FromHex(Purple), FromHex(Green) }, FromHex(DarkGray));

        /// <summary>
        /// Creates and returns a <see cref="Color"/> from the specified <paramref name="hex"/> value
        /// </summary>
        /// <param name="hex"></param>
        /// <returns></returns>
        public static Color FromHex(string? hex)
        {
            if(hex.IsNullOrEmpty())
                return Color.Empty;
            if (!hex.Contains("#"))
                hex = "#" + hex;
            return ColorTranslator.FromHtml(hex);
        }

        #endregion

        #region Private Methods

        private static ulong FashHash(string value)
        {
            var hashedValue = 3074457345618258791ul;
            for (var i = 0; i < value.Length; i++)
            {
                hashedValue += value[i];
                hashedValue *= 3074457345618258799ul;
            }
            return hashedValue;
        }

        #endregion
    }
}
