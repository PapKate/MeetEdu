using MeetBase.Web;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace MeetCore
{
    /// <summary>
    /// The schedule page
    /// </summary>
    public partial class Professor_SchedulePage : BasePage
    {
        #region Private Members

        /// <summary>
        /// The <see cref="MudDialog"/> options
        /// </summary>
        private DialogOptions mDialogOptions = new() { FullWidth = true };

        #endregion

        #region Public Properties

        /// <summary>
        /// The professor
        /// </summary>
        public ProfessorResponseModel Professor => StateManager.Professor!;

        /// <summary>
        /// The user
        /// </summary>
        public UserResponseModel User => StateManager.User!;

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
        public Professor_SchedulePage() : base()
        {

        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Updates the weekly schedule of the secretary
        /// </summary>
        private async void UpdateSchedule()
        {
            var model = new UpdateScheduleModel()
            {
                Color = User.Color,
                WeeklySchedule = Professor.WeeklySchedule,
            };
            var parameters = new DialogParameters<UpdateScheduleDialog> { { x => x.Model, model } };

            // Creates and opens a dialog with the specified type
            var dialog = await DialogService.ShowAsync<UpdateScheduleDialog>(null, parameters, mDialogOptions);

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
            if (result.Data is UpdateScheduleModel updatedModel)
            {
                // Creates the request for updating the secretary
                var secretaryRequest = new ProfessorRequestModel()
                {
                    WeeklySchedule = updatedModel.WeeklySchedule
                };

                // Updates the secretary
                var secretaryResponse = await Client.UpdateProfessorAsync(Professor.Id, secretaryRequest);

                // If there was an error...
                if (!secretaryResponse.IsSuccessful)
                {
                    Console.WriteLine(secretaryResponse.ErrorMessage);
                    // Show the error
                    Snackbar.Add(secretaryResponse.ErrorMessage, Severity.Error);
                    // Return
                    return;
                }
                StateManager.Professor = secretaryResponse.Result;

                StateHasChanged();
            }
        }

        #endregion
    }
}
