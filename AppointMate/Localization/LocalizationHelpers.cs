using System.Globalization;

namespace AppointMate
{
    /// <summary>
    /// Helpers related to localization
    /// </summary>
    public class LocalizationHelpers
    {
        #region Public Properties

        /// <summary>
        /// Gets the currency symbol of the local machine
        /// </summary>
        /// <returns></returns>
        public static string GetCurrencySymbol() => RegionInfo.CurrentRegion.CurrencySymbol;

        /// <summary>
        /// Gets the ISO currency symbol of the local machine
        /// </summary>
        /// <returns></returns>
        public static string GetIsoCurrencySymbol() => RegionInfo.CurrentRegion.ISOCurrencySymbol;

        #endregion
    }
}
