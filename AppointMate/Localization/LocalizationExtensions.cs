namespace AppointMate
{
    /// <summary>
    /// Extension methods associated with localization
    /// </summary>
    public static class LocalizationExtensions
    {
        #region Currency

        /// <summary>
        /// Returns a <see cref="string"/> that represents the specified <paramref name="value"/>
        /// as a localized currency value.
        /// </summary>
        /// <param name="value">The value</param>
        /// <returns></returns>
        public static string ToLocalizedCurrency(this byte value)
            => value + " " + LocalizationHelpers.GetCurrencySymbol();

        /// <summary>
        /// Returns a <see cref="string"/> that represents the specified <paramref name="value"/>
        /// as a localized currency value.
        /// NOTE: null is returned if the <paramref name="value"/> is also null!
        /// </summary>
        /// <param name="value">The value</param>
        /// <returns></returns>
        public static string? ToLocalizedCurrency(this byte? value)
        {
            if (value is null)
                return null;

            return value.Value.ToLocalizedCurrency();
        }

        /// <summary>
        /// Returns a <see cref="string"/> that represents the specified <paramref name="value"/>
        /// as a localized currency value.
        /// </summary>
        /// <param name="value">The value</param>
        /// <returns></returns>
        public static string ToLocalizedCurrency(this sbyte value)
            => value + " " + LocalizationHelpers.GetCurrencySymbol();

        /// <summary>
        /// Returns a <see cref="string"/> that represents the specified <paramref name="value"/>
        /// as a localized currency value.
        /// NOTE: null is returned if the <paramref name="value"/> is also null!
        /// </summary>
        /// <param name="value">The value</param>
        /// <returns></returns>
        public static string? ToLocalizedCurrency(this sbyte? value)
        {
            if (value is null)
                return null;

            return value.Value.ToLocalizedCurrency();
        }

        /// <summary>
        /// Returns a <see cref="string"/> that represents the specified <paramref name="value"/>
        /// as a localized currency value.
        /// </summary>
        /// <param name="value">The value</param>
        /// <returns></returns>
        public static string ToLocalizedCurrency(this int value)
            => value + " " + LocalizationHelpers.GetCurrencySymbol();

        /// <summary>
        /// Returns a <see cref="string"/> that represents the specified <paramref name="value"/>
        /// as a localized currency value.
        /// NOTE: null is returned if the <paramref name="value"/> is also null!
        /// </summary>
        /// <param name="value">The value</param>
        /// <returns></returns>
        public static string? ToLocalizedCurrency(this int? value)
        {
            if (value is null)
                return null;

            return value.Value.ToLocalizedCurrency();
        }

        /// <summary>
        /// Returns a <see cref="string"/> that represents the specified <paramref name="value"/>
        /// as a localized currency value.
        /// </summary>
        /// <param name="value">The value</param>
        /// <returns></returns>
        public static string ToLocalizedCurrency(this uint value)
            => value + " " + LocalizationHelpers.GetCurrencySymbol();

        /// <summary>
        /// Returns a <see cref="string"/> that represents the specified <paramref name="value"/>
        /// as a localized currency value.
        /// NOTE: null is returned if the <paramref name="value"/> is also null!
        /// </summary>
        /// <param name="value">The value</param>
        /// <returns></returns>
        public static string? ToLocalizedCurrency(this uint? value)
        {
            if (value is null)
                return null;

            return value.Value.ToLocalizedCurrency();
        }

        /// <summary>
        /// Returns a <see cref="string"/> that represents the specified <paramref name="value"/>
        /// as a localized currency value.
        /// </summary>
        /// <param name="value">The value</param>
        /// <returns></returns>
        public static string ToLocalizedCurrency(this double value)
            => value.ToString("0.00") + " " + LocalizationHelpers.GetCurrencySymbol();

        /// <summary>
        /// Returns a <see cref="string"/> that represents the specified <paramref name="value"/>
        /// as a localized currency value.
        /// NOTE: null is returned if the <paramref name="value"/> is also null!
        /// </summary>
        /// <param name="value">The value</param>
        /// <returns></returns>
        public static string? ToLocalizedCurrency(this double? value)
        {
            if (value is null)
                return null;

            return value.Value.ToLocalizedCurrency();
        }

        /// <summary>
        /// Returns a <see cref="string"/> that represents the specified <paramref name="value"/>
        /// as a localized currency value.
        /// </summary>
        /// <param name="value">The value</param>
        /// <returns></returns>
        public static string ToLocalizedCurrency(this float value)
            => value.ToString("0.00") + " " + LocalizationHelpers.GetCurrencySymbol();

        /// <summary>
        /// Returns a <see cref="string"/> that represents the specified <paramref name="value"/>
        /// as a localized currency value.
        /// NOTE: null is returned if the <paramref name="value"/> is also null!
        /// </summary>
        /// <param name="value">The value</param>
        /// <returns></returns>
        public static string? ToLocalizedCurrency(this float? value)
        {
            if (value is null)
                return null;

            return value.Value.ToLocalizedCurrency();
        }

        /// <summary>
        /// Returns a <see cref="string"/> that represents the specified <paramref name="value"/>
        /// as a localized currency value.
        /// </summary>
        /// <param name="value">The value</param>
        /// <returns></returns>
        public static string ToLocalizedCurrency(this decimal value)
            => value.ToString("0.00") + " " + LocalizationHelpers.GetCurrencySymbol();

        /// <summary>
        /// Returns a <see cref="string"/> that represents the specified <paramref name="value"/>
        /// as a localized currency value.
        /// NOTE: null is returned if the <paramref name="value"/> is also null!
        /// </summary>
        /// <param name="value">The value</param>
        /// <returns></returns>
        public static string? ToLocalizedCurrency(this decimal? value)
        {
            if (value is null)
                return null;

            return value.Value.ToLocalizedCurrency();
        }

        /// <summary>
        /// Returns a <see cref="string"/> that represents the specified <paramref name="value"/>
        /// as a localized currency value.
        /// </summary>
        /// <param name="value">The value</param>
        /// <returns></returns>
        public static string ToLocalizedCurrency(this long value)
            => value + " " + LocalizationHelpers.GetCurrencySymbol();

        /// <summary>
        /// Returns a <see cref="string"/> that represents the specified <paramref name="value"/>
        /// as a localized currency value.
        /// NOTE: null is returned if the <paramref name="value"/> is also null!
        /// </summary>
        /// <param name="value">The value</param>
        /// <returns></returns>
        public static string? ToLocalizedCurrency(this long? value)
        {
            if (value is null)
                return null;

            return value.Value.ToLocalizedCurrency();
        }

        /// <summary>
        /// Returns a <see cref="string"/> that represents the specified <paramref name="value"/>
        /// as a localized currency value.
        /// </summary>
        /// <param name="value">The value</param>
        /// <returns></returns>
        public static string ToLocalizedCurrency(this ulong value)
            => value + " " + LocalizationHelpers.GetCurrencySymbol();

        /// <summary>
        /// Returns a <see cref="string"/> that represents the specified <paramref name="value"/>
        /// as a localized currency value.
        /// NOTE: null is returned if the <paramref name="value"/> is also null!
        /// </summary>
        /// <param name="value">The value</param>
        /// <returns></returns>
        public static string? ToLocalizedCurrency(this ulong? value)
        {
            if (value is null)
                return null;

            return value.Value.ToLocalizedCurrency();
        }

        /// <summary>
        /// Returns a <see cref="string"/> that represents the specified <paramref name="value"/>
        /// as a localized currency value.
        /// </summary>
        /// <param name="value">The value</param>
        /// <returns></returns>
        public static string ToLocalizedCurrency(this short value)
            => value + " " + LocalizationHelpers.GetCurrencySymbol();

        /// <summary>
        /// Returns a <see cref="string"/> that represents the specified <paramref name="value"/>
        /// as a localized currency value.
        /// NOTE: null is returned if the <paramref name="value"/> is also null!
        /// </summary>
        /// <param name="value">The value</param>
        /// <returns></returns>
        public static string? ToLocalizedCurrency(this short? value)
        {
            if (value is null)
                return null;

            return value.Value.ToLocalizedCurrency();
        }

        /// <summary>
        /// Returns a <see cref="string"/> that represents the specified <paramref name="value"/>
        /// as a localized currency value.
        /// </summary>
        /// <param name="value">The value</param>
        /// <returns></returns>
        public static string ToLocalizedCurrency(this ushort value)
            => value + " " + LocalizationHelpers.GetCurrencySymbol();

        /// <summary>
        /// Returns a <see cref="string"/> that represents the specified <paramref name="value"/>
        /// as a localized currency value.
        /// NOTE: null is returned if the <paramref name="value"/> is also null!
        /// </summary>
        /// <param name="value">The value</param>
        /// <returns></returns>
        public static string? ToLocalizedCurrency(this ushort? value)
        {
            if (value is null)
                return null;

            return value.Value.ToLocalizedCurrency();
        }

        #endregion

        #region Percentage

        /// <summary>
        /// Returns a <see cref="string"/> that represents the specified <paramref name="value"/>
        /// as a percentage value.
        /// </summary>
        /// <param name="value">The value</param>
        /// <returns></returns>
        public static string ToPercentage(this byte value)
            => value + " %";

        /// <summary>
        /// Returns a <see cref="string"/> that represents the specified <paramref name="value"/>
        /// as a percentage value.
        /// NOTE: null is returned if the <paramref name="value"/> is also null!
        /// </summary>
        /// <param name="value">The value</param>
        /// <returns></returns>
        public static string? ToPercentage(this byte? value)
        {
            if (value is null)
                return null;

            return value.Value.ToPercentage();
        }

        /// <summary>
        /// Returns a <see cref="string"/> that represents the specified <paramref name="value"/>
        /// as a percentage value.
        /// </summary>
        /// <param name="value">The value</param>
        /// <returns></returns>
        public static string ToPercentage(this sbyte value)
            => value + " %";

        /// <summary>
        /// Returns a <see cref="string"/> that represents the specified <paramref name="value"/>
        /// as a percentage value.
        /// NOTE: null is returned if the <paramref name="value"/> is also null!
        /// </summary>
        /// <param name="value">The value</param>
        /// <returns></returns>
        public static string? ToPercentage(this sbyte? value)
        {
            if (value is null)
                return null;

            return value.Value.ToPercentage();
        }

        /// <summary>
        /// Returns a <see cref="string"/> that represents the specified <paramref name="value"/>
        /// as a percentage value.
        /// </summary>
        /// <param name="value">The value</param>
        /// <returns></returns>
        public static string ToPercentage(this int value)
            => value + " %";

        /// <summary>
        /// Returns a <see cref="string"/> that represents the specified <paramref name="value"/>
        /// as a percentage value.
        /// NOTE: null is returned if the <paramref name="value"/> is also null!
        /// </summary>
        /// <param name="value">The value</param>
        /// <returns></returns>
        public static string? ToPercentage(this int? value)
        {
            if (value is null)
                return null;

            return value.Value.ToPercentage();
        }

        /// <summary>
        /// Returns a <see cref="string"/> that represents the specified <paramref name="value"/>
        /// as a percentage value.
        /// </summary>
        /// <param name="value">The value</param>
        /// <returns></returns>
        public static string ToPercentage(this uint value)
            => value + " %";

        /// <summary>
        /// Returns a <see cref="string"/> that represents the specified <paramref name="value"/>
        /// as a percentage value.
        /// NOTE: null is returned if the <paramref name="value"/> is also null!
        /// </summary>
        /// <param name="value">The value</param>
        /// <returns></returns>
        public static string? ToPercentage(this uint? value)
        {
            if (value is null)
                return null;

            return value.Value.ToPercentage();
        }

        /// <summary>
        /// Returns a <see cref="string"/> that represents the specified <paramref name="value"/>
        /// as a percentage value.
        /// </summary>
        /// <param name="value">The value</param>
        /// <returns></returns>
        public static string ToPercentage(this float value)
            => NumericHelpers.Round((decimal)value) + " %";

        /// <summary>
        /// Returns a <see cref="string"/> that represents the specified <paramref name="value"/>
        /// as a percentage value.
        /// NOTE: null is returned if the <paramref name="value"/> is also null!
        /// </summary>
        /// <param name="value">The value</param>
        /// <returns></returns>
        public static string? ToPercentage(this float? value)
        {
            if (value is null)
                return null;

            return NumericHelpers.Round((decimal)value.Value).ToPercentage();
        }

        /// <summary>
        /// Returns a <see cref="string"/> that represents the specified <paramref name="value"/>
        /// as a percentage value.
        /// </summary>
        /// <param name="value">The value</param>
        /// <returns></returns>
        public static string? ToPercentage(this double value)
            => NumericHelpers.Round((decimal)value) + " %";

        /// <summary>
        /// Returns a <see cref="string"/> that represents the specified <paramref name="value"/>
        /// as a percentage value.
        /// NOTE: null is returned if the <paramref name="value"/> is also null!
        /// </summary>
        /// <param name="value">The value</param>
        /// <returns></returns>
        public static string? ToPercentage(this double? value)
        {
            if (value is null)
                return null;

            return NumericHelpers.Round((decimal)value.Value).ToPercentage();
        }

        /// <summary>
        /// Returns a <see cref="string"/> that represents the specified <paramref name="value"/>
        /// as a percentage value.
        /// </summary>
        /// <param name="value">The value</param>
        /// <returns></returns>
        public static string ToPercentage(this decimal value)
            => NumericHelpers.Round(value) + " %";

        /// <summary>
        /// Returns a <see cref="string"/> that represents the specified <paramref name="value"/>
        /// as a percentage value.
        /// NOTE: null is returned if the <paramref name="value"/> is also null!
        /// </summary>
        /// <param name="value">The value</param>
        /// <returns></returns>
        public static string? ToPercentage(this decimal? value)
        {
            if (value is null)
                return null;

            return NumericHelpers.Round(value.Value).ToPercentage();
        }

        /// <summary>
        /// Returns a <see cref="string"/> that represents the specified <paramref name="value"/>
        /// as a percentage value.
        /// </summary>
        /// <param name="value">The value</param>
        /// <returns></returns>
        public static string ToPercentage(this long value)
            => value + " %";

        /// <summary>
        /// Returns a <see cref="string"/> that represents the specified <paramref name="value"/>
        /// as a percentage value.
        /// NOTE: null is returned if the <paramref name="value"/> is also null!
        /// </summary>
        /// <param name="value">The value</param>
        /// <returns></returns>
        public static string? ToPercentage(this long? value)
        {
            if (value is null)
                return null;

            return value.Value.ToPercentage();
        }

        /// <summary>
        /// Returns a <see cref="string"/> that represents the specified <paramref name="value"/>
        /// as a percentage value.
        /// </summary>
        /// <param name="value">The value</param>
        /// <returns></returns>
        public static string ToPercentage(this ulong value)
            => value + " %";

        /// <summary>
        /// Returns a <see cref="string"/> that represents the specified <paramref name="value"/>
        /// as a percentage value.
        /// NOTE: null is returned if the <paramref name="value"/> is also null!
        /// </summary>
        /// <param name="value">The value</param>
        /// <returns></returns>
        public static string? ToPercentage(this ulong? value)
        {
            if (value is null)
                return null;

            return value.Value.ToPercentage();
        }

        /// <summary>
        /// Returns a <see cref="string"/> that represents the specified <paramref name="value"/>
        /// as a percentage value.
        /// </summary>
        /// <param name="value">The value</param>
        /// <returns></returns>
        public static string ToPercentage(this short value)
            => value + " %";

        /// <summary>
        /// Returns a <see cref="string"/> that represents the specified <paramref name="value"/>
        /// as a percentage value.
        /// NOTE: null is returned if the <paramref name="value"/> is also null!
        /// </summary>
        /// <param name="value">The value</param>
        /// <returns></returns>
        public static string? ToPercentage(this short? value)
        {
            if (value is null)
                return null;

            return value.Value.ToPercentage();
        }

        /// <summary>
        /// Returns a <see cref="string"/> that represents the specified <paramref name="value"/>
        /// as a percentage value.
        /// </summary>
        /// <param name="value">The value</param>
        /// <returns></returns>
        public static string ToPercentage(this ushort value)
            => value + " %";

        /// <summary>
        /// Returns a <see cref="string"/> that represents the specified <paramref name="value"/>
        /// as a percentage value.
        /// NOTE: null is returned if the <paramref name="value"/> is also null!
        /// </summary>
        /// <param name="value">The value</param>
        /// <returns></returns>
        public static string? ToPercentage(this ushort? value)
        {
            if (value is null)
                return null;

            return value.Value.ToPercentage();
        }

        #endregion
    }
}
