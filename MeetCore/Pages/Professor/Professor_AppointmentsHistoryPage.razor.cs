using MeetBase;
using MeetBase.Web;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace MeetCore
{
    /// <summary>
    /// The appointments history page
    /// </summary>
    public partial class Professor_AppointmentsHistoryPage : BasePage
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
        public Professor_AppointmentsHistoryPage() : base()
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
                IncludeStatuses = new List<AppointmentStatus>() { AppointmentStatus.Completed, AppointmentStatus.Canceled }
            });

            // If there was an error...
            if (!response.IsSuccessful)
            {
                // Show the error
                Snackbar.Add(response.ErrorMessage, Severity.Error);
                // Return
                return;
            }

            if (response.Result.IsNullOrEmpty())
            {
                // Show the error
                Snackbar.Add("No appointments", Severity.Error);
                // Return
                return;
            }

            mAppointments.AddRange(response.Result.OrderByDescending(x => x.DateStart));

            StateHasChanged();
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Views the specified <paramref name="appointment"/> details
        /// </summary>
        /// <param name="appointment">The rule</param>
        private async Task ViewAppointmetAsync(AppointmentResponseModel appointment)
        {
            var model = new AppointmentRequestModel()
            {
                DateStart = appointment.DateStart,
                Status = appointment.Status,
                CalendarEvent = appointment.CalendarEvent,
                MeetLink = appointment.MeetLink,
            };

            var parameters = new DialogParameters<AppointmentDialog> { { x => x.Model, model }, { x => x.Appointment, appointment }, { x => x.IsEditable, false } };

            // Creates and opens a dialog with the specified type
            var dialog = await DialogService.ShowAsync<AppointmentDialog>(null, parameters, mDialogOptions);
        }

        #endregion
    }
}
