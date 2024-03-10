using MeetBase.Web;

using Microsoft.AspNetCore.Components;

using MudBlazor;

namespace MeetCore
{
    /// <summary>
    /// The department information page
    /// </summary>
    public partial class Secretary_DepartmentInformationPage : BasePage
    {
        #region Private Properties

        /// <summary>
        /// The <see cref="MudDialog"/> options
        /// </summary>
        private DialogOptions mDialogOptions = new() { FullWidth = true };

        #endregion

        #region Public Properties

        /// <summary>
        /// The department
        /// </summary>
        public DepartmentResponseModel Department => StateManager.Department!;

        #endregion

        #region Protected Properties

        /// <summary>
        /// The client
        /// </summary>
        [Inject]
        protected MeetCoreClient Client { get; set; } = default!;

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
        public Secretary_DepartmentInformationPage() : base()
        {

        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Updates the weekly schedule of the department
        /// </summary>
        private async void UpdateSchedule()
        {
            var model = new UpdateScheduleModel()
            {
                Color = Department.Color,
                WeeklySchedule = Department.WorkHours
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
                // Creates the request for updating the department
                var departmentRequest = new DepartmentRequestModel()
                {
                    WorkHours = updatedModel.WeeklySchedule
                };

                // Updates the department
                var departmentResponse = await Client.UpdateDepartmentAsync(Department.Id, departmentRequest);

                // If there was an error...
                if (!departmentResponse.IsSuccessful)
                {
                    Console.WriteLine(departmentResponse.ErrorMessage);
                    // Show the error
                    Snackbar.Add(departmentResponse.ErrorMessage, Severity.Error);
                    // Return
                    return;
                }
                StateManager.Department = departmentResponse.Result;

                StateHasChanged();
            }

        }

        /// <summary>
        /// Updates the department information
        /// </summary>
        private async void UpdateDepartment()
        {
            var model = new UpdateModel<DepartmentRequestModel>(new DepartmentRequestModel()
            {
                Name = Department.Name,
                Category = Department.Category,
                Email = Department.Email,
                PhoneNumber = Department.PhoneNumber,
                ImageUrl = Department.ImageUrl,
                Color = Department.Color,
                Location = Department.Location
            });

            var parameters = new DialogParameters<UpdateDepartmentDialog> { { x => x.Model, model } };

            // Creates and opens a dialog with the specified type
            var dialog = await DialogService.ShowAsync<UpdateDepartmentDialog>(null, parameters, mDialogOptions);

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
            if (result.Data is UpdateModel<DepartmentRequestModel> updatedModel)
            {
                // Creates the request for updating the department
                var departmentRequest = new DepartmentRequestModel()
                {
                    Name = updatedModel.Model!.Name,
                    Category = updatedModel.Model.Category,
                    Email = updatedModel.Model.Email,
                    PhoneNumber = updatedModel.Model.PhoneNumber,
                    ImageUrl = updatedModel.Model.ImageUrl,
                    Color = updatedModel.Model.Color,
                    Location = updatedModel.Model.Location
                };

                // Updates the department
                var departmentResponse = await Client.UpdateDepartmentAsync(Department.Id, departmentRequest);

                // If there was an error...
                if (!departmentResponse.IsSuccessful)
                {
                    // Show the error
                    Snackbar.Add(departmentResponse.ErrorMessage, Severity.Error);
                    // Return
                    return;
                }
                StateManager.Department = departmentResponse.Result;

                StateHasChanged();
            }
        }

        #endregion
    }
}
