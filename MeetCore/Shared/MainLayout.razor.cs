using MeetBase.Web;
using Microsoft.AspNetCore.Components;

using MudBlazor;

using static MeetBase.Blazor.PaletteColors;

namespace MeetCore.Shared
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
        private MudTheme? mMeetCoreTheme;

        #endregion

        #region Protected Properties

        /// <summary>
        /// The <see cref="MudBlazor"/> snack bar manager
        /// </summary>
        [Inject]
        protected ISnackbar Snackbar { get; set; } = default!;

        /// <summary>
        /// The <see cref="MeetCoreHubClient"/>
        /// </summary>
        [Inject]
        protected MeetCoreHubClient HubClient { get; set; } = default!;

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public MainLayout() : base()
        {
            mMeetCoreTheme = new MudTheme()
            {
                Typography = new Typography()
                {
                    Default = new Default()
                    {
                        FontFamily = new[] { "Objektiv VF Trial", "Calibri", "sans-serif" }
                    }
                },
                Palette = new()
                {
                    Primary = Amber,
                    PrimaryContrastText = DarkGray,
                    Secondary = Persimmon,
                    SecondaryContrastText = White,
                    Tertiary = Pink,
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
                    ActionDisabledBackground = LightGray,
                }
            };
        }

        #endregion

        #region Protected Methods

        /// <inheritdoc/>
        protected override void OnAfterRender(bool firstRender)
        {
            base.OnAfterRender(firstRender);
            if (firstRender)
                HubClient.AppointmentsCreated += ShowNotification;

        }

        #endregion

        #region Private Methods

        private void ShowNotification(object? sender, IEnumerable<AppointmentResponseModel> appointments)
        {
            Snackbar.Add("Notification!");
        }

        #endregion
    }
}
