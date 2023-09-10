namespace MeetBase.Blazor
{
    public static class Personalization
    {
        #region Buttons

        public const string TinyButtonSize = "24px";

        public const string VeryVerySmallButtonSize = "28px";

        public const string VerySmallButtonSize = "32px";

        public const string SmallButtonSize = "40px";
        
        public const string NormalButtonSize = "48px";

        public const string MediumButtonSize = "60px";
        
        public const string VeryLargeButtonSize = "80px";

        #endregion

        #region Size

        public const string SmallSize = "8px";

        public const string MediumSize = "1rem";

        public const string LargeSize = "2rem";

        #endregion

        #region Distance

        public const string SmallDistance = "1rem";
        
        public const string NormalDistance = "24px";

        public const string LargeDistance = "32px";

        #endregion

        #region Height

        public const string VerySmallHeight = "4px";

        #endregion

        #region Padding

        public const string SmallPadding = "4px";

        public const string NormalPadding = "8px";

        public const string LargePadding = "12px";

        public const string VeryLargePadding = "1rem";

        public const string VeryVeryLargePadding = "24px";

        public const string XXLargePadding = "32px";

        #endregion

        #region Margin

        public const string SmallMargin = "4px";

        public const string NormalMargin = "8px";

        public const string LargeMargin = "12px";

        #endregion

        #region Gap

        public const string SmallGap = "4px";
        
        public const string MediumGap = "8px";

        public const string LargeGap = "12px";

        public const string VeryLargeGap = "1rem";

        public const string VeryVeryLargeGap = "24px";
        
        public const string XXLargeGap = "32px";

        #endregion

        #region Border Radius

        public const string VerySmallBorderRadius = "2px";

        public const string SmallBorderRadius = "4px";

        public const string NormalBorderRadius = "8px";

        public const string LargeBorderRadius = "12px";

        public const string VeryLargeBorderRadius = "16px";

        public const string CircleBorderRadius = "50%";

        #endregion

        #region Font

        /// <summary>
        /// The font family
        /// </summary>
        public const string FontFamily = "Calibri,sans-serif";

        /// <summary>
        /// The bold font weight
        /// </summary>
        public const int BoldFont = 600;

        /// <summary>
        /// The normal font weight
        /// </summary>
        public const int NormalFont = 400;

        /// <summary>
        /// The light font weight
        /// </summary>
        public const int LightFont = 300;

        public const string SmallFontSize = "100%";

        public const string MediumFontSize = "120%";

        public const string LargeFontSize = "160%";
        public const string XXLargeFontSize = "200%";

        public const string FixedVerySmallFontSize = "16px";
        
        public const string FixedSmallFontSize = "20px";

        public const string FixedMediumFontSize = "24px";
        
        public const string FixedLargeFontSize = "28px";

        #endregion

        #region Border

        public const string VerySmallBorderThickness = "1px";

        public const string SmallBorderThickness = "2px";

        public const string NormalBorderThickness = "4px";

        public const string LargeBorderThickness = "8px";

        #endregion

        #region Shadows

        public const string BoxShadow = "0px 3px 1px -2px rgb(0 0 0 / 20%), 0px 2px 2px 0px rgb(0 0 0 / 14%), 0px 1px 5px 0px rgb(0 0 0 / 12%)";

        public const string RightBoxShadow = "2px 2px 4px rgb(0 0 0 / 30%);";
        
        public const string LightFilterDropShadow = "drop-shadow(2px 1px 2px rgba(0, 0, 0, 0.2))";

        public const string FilterDropShadow = "drop-shadow(4px 4px 2px rgba(0, 0, 0, 0.5))";

        #endregion

        #region Opacity

        public const double SmallOpacity = 0.4;

        public const double SemiTransparentOpacity = 0.5;

        public const double LargeOpacity = 0.8;

        #endregion

        #region Animations

        /// <summary>
        /// The loading duration for an animation
        /// Value : 1200 ms
        /// </summary>
        public static TimeSpan LoadingAnimationDuration = new TimeSpan(0, 0, 0, 1, 200);

        /// <summary>
        /// The duration for a ripple animation
        /// Value : 800 ms
        /// </summary>
        public static TimeSpan RippleAnimationDuration = new TimeSpan(0, 0, 0, 0, 800);

        /// <summary>
        /// The delay for a ripple animation
        /// Value : 790 ms
        /// </summary>
        public static TimeSpan RippleAnimationDelay = new TimeSpan(0, 0, 0, 0, 750);

        /// <summary>
        /// The basic duration for an animation
        /// Value : 400 ms
        /// </summary>
        public static TimeSpan BasicAnimationDuration = new TimeSpan(0, 0, 0, 0, 400);

        /// <summary>
        /// The basic delay for an animation
        /// Value : 350 ms
        /// </summary>
        public static TimeSpan BasicAnimationDelay = new TimeSpan(0, 0, 0, 0, 350);

        /// <summary>
        /// The small duration for an animation
        /// Value : 200 ms
        /// </summary>
        public static TimeSpan SmallAnimationDuration = new TimeSpan(0, 0, 0, 0, 200);

        /// <summary>
        /// The small delay for an animation
        /// Value : 150 ms
        /// </summary>
        public static TimeSpan SmallAnimationDelay = new TimeSpan(0, 0, 0, 0, 150);

        /// <summary>
        /// The very small delay for an animation
        /// Value : 50 ms
        /// </summary>
        public static TimeSpan VerySmallAnimationDelay = new TimeSpan(0, 0, 0, 0, 50);

        /// <summary>
        /// The delay for rendering
        /// Value : 10 ms
        /// </summary>
        public static TimeSpan RenderDelay = new TimeSpan(0, 0, 0, 0, 10);

        /// <summary>
        /// The very small delay for an animation
        /// Value : 4 seconds
        /// </summary>
        public static TimeSpan HintDialogDuration = new TimeSpan(0, 0, 0, 4);

        #endregion

        #region Dataform 

        /// <summary>
        /// The minimum width of the forms presenter to the user
        /// </summary>
        public const double FormMinimumWidth = 400;

        /// <summary>
        /// The width of most of input elements contained in a dataform
        /// </summary>
        public const double StandardInputElementWidth = 400;

        /// <summary>
        /// The minimum width of the drop down buttons contained in a dataform
        /// </summary>
        public const double DropDownButtonMinWidth = 200;

        #endregion
    }
}
