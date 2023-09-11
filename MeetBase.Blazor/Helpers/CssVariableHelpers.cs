using static MeetBase.Blazor.CssVariables;

namespace MeetBase.Blazor
{
    /// <summary>
    /// Extension methods for <see cref="CssVariables"/>
    /// </summary>
    public static class CssVariableHelpers
    {
        #region Public Methods

        /// <summary>
        /// Sets the css variables for the <see cref="BaseControl"/>
        /// <code>--width: width;</code>
        /// </summary>
        /// <param name="width">The width</param>
        /// <param name="height">The height</param>
        /// <returns></returns>
        public static string SetBaseControlCssVariables(string? width, string? height)
        {
            var value = string.Empty;

            // If the width is set...
            if (!width.IsNullOrEmpty())
                value += $"{WidthVariable.SetCssVariable(width)}; ";

            // If the height is set...
            if (!height.IsNullOrEmpty())
                value += $"{HeightVariable.SetCssVariable(height)}; ";

            return value;
        }

        /// <summary>
        /// Sets the css variables for the <see cref="BackgroundControl"/>
        /// <code>--width: width; --background: background;</code>
        /// </summary>
        /// <param name="width">The width</param>
        /// <param name="height">The height</param>
        /// <param name="background">The background</param>
        /// <returns></returns>
        public static string SetBackgroundControlCssVariables(string? width, string? height, string? background)
        {
            // If the background is set...
            if (!background.IsNullOrEmpty())
                return $"{SetBaseControlCssVariables(width, height)} {BackgroundVariable.SetCssColor(background)};";

            return $"{SetBaseControlCssVariables(width, height)}";
        }

        /// <summary>
        /// Sets the css variables for the <see cref="ForegroundControl"/>
        /// <code>--background: background; --foreground: foreground;</code>
        /// </summary>
        /// <param name="width">The width</param>
        /// <param name="height">The height</param>
        /// <param name="background">The background</param>
        /// <param name="foreground">The foreground</param>
        /// <returns></returns>
        public static string SetForegroundControlCssVariables(string? width, string? height, string? background, string? foreground)
        {
            // If the foreground is set...
            if (!foreground.IsNullOrEmpty())
                return $"{SetBackgroundControlCssVariables(width, height, background)} {ForegroundVariable.SetCssColor(foreground)};";

            return $"{SetBackgroundControlCssVariables(width, height, background)}";
        }

        /// <summary>
        /// Sets the css variables for the <see cref="BorderControl"/>
        /// <code>--background: background; --foreground: foreground; --borderVar: borderVal; ...</code>
        /// </summary>
        /// <param name="width">The width</param>
        /// <param name="height">The height</param>
        /// <param name="background">The background</param>
        /// <param name="foreground">The foreground</param>
        /// <param name="borderBrush">The border brush</param>
        /// <param name="borderRadius">The border radius</param>
        /// <param name="borderStyle">The border style</param>
        /// <param name="borderThickness">The border thickness</param>
        /// <param name="boxShadow">The box shadow</param>
        /// <returns></returns>
        public static string SetBorderControlCssVariables(string? width, string? height, string? background, string? foreground, string? boxShadow, string? borderRadius, BorderStyle? borderStyle, string? borderBrush, string? borderThickness)
        {
            var value = $"{SetForegroundControlCssVariables(width, height, background, foreground)} ";

            if (!borderRadius.IsNullOrEmpty())
                value += $"{BorderRadiusVariable.SetCssVariable(borderRadius)}; ";

            if (!boxShadow.IsNullOrEmpty())
                value += $"{BoxShadowVariable.SetCssVariable(boxShadow)}; ";

            if (borderStyle is not null)
                value += $"{BorderStyleVariable.SetCssVariable(borderStyle)}; ";

            if (!borderBrush.IsNullOrEmpty())
                value += $"{BorderBrushVariable.SetCssColor(borderBrush)}; ";

            if (!borderThickness.IsNullOrEmpty())
                value += $"{BorderThicknessVariable.SetCssVariable(borderThickness)}; ";

            return value;
        }

        /// <summary>
        /// Sets the css variables for the <see cref="ForegroundControl"/>
        /// <code>--background: background; --foreground: foreground; --borderVar: borderVal; ...</code>
        /// </summary>
        /// <returns></returns>
        public static string SetTypographyCssVariables(string? fontFammiy, string? fontSize, string? fontWeight)
        {
            var value = string.Empty;
            if (!fontFammiy.IsNullOrEmpty())
                value += $"{FontFamilyVariable.SetCssVariable(fontFammiy)}; ";

            if (!fontSize.IsNullOrEmpty())
                value += $"{FontSizeVariable.SetCssVariable(fontSize)}; ";

            if (!fontWeight.IsNullOrEmpty())
                value += $"{FontWeightVariable.SetCssVariable(fontWeight)}; ";

            return value;
        }

        #endregion
    }

}
