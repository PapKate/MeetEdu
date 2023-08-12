using Atom.Blazor;
using Atom.Blazor.Controls;
using static Atom.Personalization;

using Microsoft.AspNetCore.Components;
using System.ComponentModel;

namespace AppointMate
{
    /// <summary>
    /// Constants for CSS
    /// </summary>
    public static class CssConstants
    {
        #region Public Properties

        /// <summary>
        /// The font family
        /// </summary>
        public const string FontFamily = "--fontFamily";

        #region ForeColor

        /// <summary>
        /// The fore color
        /// </summary>
        public const string ForeColor = "--foreColor";

        /// <summary>
        /// The hover fore color
        /// </summary>
        public const string HoverForeColor = "--hoverForeColor";

        /// <summary>
        /// The mouse down fore color
        /// </summary>
        public const string MouseDownForeColor = "--mouseDownForeColor";

        /// <summary>
        /// The is selected fore color
        /// </summary>
        public const string SelectedForeColor = "--selectedForeColor";

        /// <summary>
        /// The disabled fore color
        /// </summary>
        public const string DisabledForeColor = "--disabledForeColor";

        #endregion

        #region BackColor

        /// <summary>
        /// The back color
        /// </summary>
        public const string BackColor = "--backColor";

        #endregion

        #endregion
    }

    /// <summary>
    /// Extension methods for <see cref="CssConstants"/>
    /// </summary>
    public static class CssConstantExtensions
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
        public static string SetCssVariable(this string name, string value)
            => $"{name}: {(value.IsHexValue() ? value.NormalizedColor() : value)};";

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
