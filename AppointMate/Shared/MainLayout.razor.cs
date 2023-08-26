using MudBlazor;

using static AppointMate.Personalization;

namespace AppointMate.Shared
{
    /// <summary>
    /// The main layout
    /// </summary>
    public partial class MainLayout
    {
        #region Private Members

        /// <summary>
        /// The theme provider
        /// </summary>
        private MudTheme? mMeetEduTheme;

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public MainLayout() : base()
        {
            mMeetEduTheme = new MudTheme()
            {
                Typography = new Typography()
                {
                    Default = new Default()
                    {
                        FontFamily = new[] { "Objektiv VF Trial", "sans-serif" }
                    }
                },
                Palette = new()
                {
                    Primary = Blue,
                    PrimaryContrastText = White,
                    Secondary = Green,
                    SecondaryContrastText = DarkGray,
                    Tertiary = Purple,
                    TertiaryContrastText = White,
                    Info = Blue,
                    InfoContrastText = White,
                    Success = Green,
                    SuccessContrastText = White,
                    Warning = Yellow,
                    WarningContrastText = DarkGray,
                    Error = Red,
                    ErrorContrastText = White,
                    Dark = DarkGray,
                    DarkContrastText = White,
                    TextPrimary = DarkGray,
                    TextSecondary = Gray,
                    TextDisabled = LightGray,
                    ActionDisabledBackground = LightGray
                }
            };
        }

        #endregion
    }
}
