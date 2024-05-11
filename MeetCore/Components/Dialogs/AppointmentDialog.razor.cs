using MeetBase;
using MeetBase.Blazor;
using MeetBase.Web;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using MudBlazor;

namespace MeetCore
{
    /// <summary>
    /// The dialog for editing an appointment 
    /// </summary>
    public partial class AppointmentDialog
    {
        #region Private Members

        #endregion

        #region Public Properties

        /// <summary>
        /// The dialog instance
        /// </summary>
        [CascadingParameter]
        public MudDialogInstance MudDialog { get; set; } = default!;

        /// <summary>
        /// The model
        /// </summary>
        [Parameter]
        public AppointmentRequestModel Model { get; set; } = default!;

        /// <summary>
        /// The appointment
        /// </summary>
        [Parameter]
        public AppointmentResponseModel Appointment { get; set; } = default!;

        #endregion

        #region Protected Properties

        /// <summary>
        /// The <see cref="IJSRuntime"/> service
        /// </summary>
        [Inject]
        protected IJSRuntime JSRuntime { get; set; } = default!;

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public AppointmentDialog() : base()
        {

        }

        #endregion

        #region Protected Methods

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        protected override void OnInitialized()
        {
            base.OnInitialized();

        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Generates and downloads the calendar event
        /// </summary>
        private async void DownloadEvent_OnClick()
        {
            var eventContent = CalendarHelpers.GenerateCalendarEvent(Appointment.Rule!.Name, Appointment.Message, Appointment.DateStart.DateTime, Appointment.Rule!.Duration);

            await JSRunTimeHelpers.DownloadCalendarEventsAsync(JSRuntime, eventContent, Appointment.Rule!.Name);
        }

        private void SaveButton_OnClick()
        {
            MudDialog.Close(DialogResult.Ok(Model));
        }

        private void CancelButton_OnClick()
        {
            MudDialog.Cancel();
        }

        #endregion
    }
}
