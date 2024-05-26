using MeetBase;
using MeetBase.Web;
using Microsoft.AspNetCore.Components;

using MudBlazor;

using static MeetBase.Blazor.PaletteColors;
using static MudBlazor.Defaults.Classes;

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
            {
                HubClient.AppointmentsCreated += ShowAppointmentNotifications;
                HubClient.MessagesCreated += ShowMessageNotifications;
            }

        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Shows the notification for each appointment in the list of <paramref name="appointments"/>
        /// </summary>
        private void ShowAppointmentNotifications(object? sender, IEnumerable<AppointmentResponseModel> appointments)
        {
            // Change the notification position to top right
            Snackbar.Configuration.PositionClass = Defaults.Classes.Position.TopRight;
            
            // For each appointment...
            foreach (var appointment in appointments)
            {
                // Show a notification
                Snackbar.Add($"New {appointment.Rule!.Name} appointment created for the {appointment.DateStart.ToString(FormatConstants.DateTimeFormat)}!", Severity.Info);
            }

            // Change the notification position to bottom center
            Snackbar.Configuration.PositionClass = Defaults.Classes.Position.BottomCenter;
        }

        /// <summary>
        /// Shows the notification for each message in the list of<paramref name="messages"/>
        /// </summary>
        private void ShowMessageNotifications(object? sender, IEnumerable<DepartmentContactMessageResponseModel> messages)
        {
            // Change the notification position to top right
            Snackbar.Configuration.PositionClass = Defaults.Classes.Position.TopRight;

            // For each message...
            foreach (var message in messages)
            {
                // Show a notification
                Snackbar.Add($"New message for {message.Role.ToString().BreakWords()} sent!", Severity.Info);
            }

            // Change the notification position to bottom center
            Snackbar.Configuration.PositionClass = Defaults.Classes.Position.BottomCenter;
        }

        #endregion
    }
}
