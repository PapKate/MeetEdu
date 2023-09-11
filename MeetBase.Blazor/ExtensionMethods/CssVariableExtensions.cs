namespace MeetBase.Blazor
{
    /// <summary>
    /// Extension methods for <see cref="CssVariables"/>
    /// </summary>
    public static class CssVariableExtensions
    {
        #region Public Methods

        /// <summary>
        /// Sets the <paramref name="value"/> to the CSS variable with the specified <paramref name="name"/>
        /// <code>--name: value;</code>
        /// </summary>
        /// <param name="name">The name</param>
        /// <param name="value">The value</param>
        /// <returns></returns>
        public static string SetCssVariable(this string name, object value)
            => $"{name}: {value};";

        /// <summary>
        /// Sets the <paramref name="value"/> to the CSS variable with the specified <paramref name="name"/>
        /// <code>--name: value;</code>
        /// </summary>
        /// <param name="name">The name</param>
        /// <param name="value">The value</param>
        /// <returns></returns>
        public static string SetCssColor(this string name, string? value)
            => $"{name}: {(!value.IsNullOrEmpty() && value.IsHexValue() ? value.NormalizedColor() : value)};";

        /// <summary>
        /// Gets the CSS variable with the specified <paramref name="name"/>
        /// <code>var(--name)</code>
        /// </summary>
        /// <param name="name">The name</param>
        /// <returns></returns>
        public static string GetCssVariable(this string name)
            => $"var({name})";

        #endregion
    }
}
