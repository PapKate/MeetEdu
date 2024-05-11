using MeetBase;
using MeetBase.Web;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace MeetCore
{
    /// <summary>
    /// The active appointments page
    /// </summary>
    public partial class Professor_AciveAppointmentsPage : BasePage
    {
        #region Private Members

        /// <summary>
        /// The <see cref="MudDialog"/> options
        /// </summary>
        private DialogOptions mDialogOptions = new() { FullWidth = true };

        private List<AppointmentResponseModel> mAppointments = new();

        private MudDataGrid<AppointmentResponseModel>? mDataGrid;

        #endregion

        #region Public Properties

        /// <summary>
        /// The professor
        /// </summary>
        public ProfessorResponseModel Professor => StateManager.Professor!;

        /// <summary>
        /// The department
        /// </summary>
        public DepartmentResponseModel Department => StateManager.Department!;

        #endregion

        #region Protected Properties

        /// <summary>
        /// The dialog service
        /// </summary>
        [Inject]
        protected IDialogService DialogService { get; set; } = default!;

        /// <summary>
        /// The <see cref="MudBlazor"/> snack bar manager
        /// </summary>
        [Inject]
        protected ISnackbar Snackbar { get; set; } = default!;

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public Professor_AciveAppointmentsPage() : base()
        {

        }

        #endregion

        #region Protected Methods

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();

            var response = await Client.GetAppointmentsAsync(new()
            {
                IncludeProfessors = new List<string>() { StateManager.Professor!.Id },
                IncludeStatuses = new List<AppointmentStatus>() { AppointmentStatus.Pending, AppointmentStatus.Confirmed }
            });

            // If there was an error...
            if (!response.IsSuccessful)
            {
                // Show the error
                Snackbar.Add(response.ErrorMessage, Severity.Info);
                // Return
                return;
            }

            if (response.Result.IsNullOrEmpty())
            {
                // Show the error
                Snackbar.Add("No appointments", Severity.Info);
                // Return
                return;
            }

            mAppointments.AddRange(response.Result.OrderByDescending(x => x.DateStart));

            StateHasChanged();
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Edits the specified <paramref name="appointment"/>
        /// </summary>
        /// <param name="appointment">The rule</param>
        private async Task EditAppointmetAsync(AppointmentResponseModel appointment)
        {
            var model = new AppointmentRequestModel()
            {
                DateStart = appointment.DateStart,
                Status = appointment.Status,
                CalendarEvent = appointment.CalendarEvent,
                MeetLink = appointment.MeetLink,
            };

            var parameters = new DialogParameters<AppointmentDialog> { { x => x.Model, model }, { x => x.Appointment, appointment } };

            // Creates and opens a dialog with the specified type
            var dialog = await DialogService.ShowAsync<AppointmentDialog>(null, parameters, mDialogOptions);

            // Once the dialog is closed...
            // Gets the result
            var result = await dialog.Result;

            // If there is no result or the dialog was closed by canceling the inner actions...
            if (result is null || result.Canceled)
            {
                // Return 
                return;
            }

            // If the result is of the specified type...
            if (result.Data is AppointmentRequestModel updatedModel)
            {
                // Updates the rule
                var appointmentResponse = await Client.UpdateAppointmentAsync(appointment.Id, updatedModel);

                // If there was an error...
                if (!appointmentResponse.IsSuccessful)
                {
                    Console.WriteLine(appointmentResponse.ErrorMessage);
                    // Show the error
                    Snackbar.Add(appointmentResponse.ErrorMessage, Severity.Error);
                    // Return
                    return;
                }

                mAppointments.First(x => x.Id == appointmentResponse.Result.Id).Status = appointmentResponse.Result.Status;
                mAppointments.First(x => x.Id == appointmentResponse.Result.Id).MeetLink = appointmentResponse.Result.MeetLink;

                //((IMudStateHasChanged)mDatagrid).StateHasChanged();
            }
        }

        #endregion
    }
}
