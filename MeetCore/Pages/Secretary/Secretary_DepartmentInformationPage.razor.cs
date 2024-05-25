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
        public DepartmentResponseModel Model => StateManager.Department!;

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
                Color = Model.Color,
                WeeklySchedule = Model.WorkHours
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
                var departmentResponse = await Client.UpdateDepartmentAsync(Model.Id, departmentRequest);

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
        private async void UpdateDepartmentContent()
        {
            var model = new UpdateModel<DepartmentRequestModel>(new DepartmentRequestModel()
            {
                Color = Model.Color,
                Description = Model.Description,
                Note = Model.Note,
                Fields = Model.Fields
            });

            var parameters = new DialogParameters<UpdateDepartmentContentDialog> { { x => x.Model, model } };

            // Creates and opens a dialog with the specified type
            var dialog = await DialogService.ShowAsync<UpdateDepartmentContentDialog>(null, parameters, mDialogOptions);

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
                    Description = updatedModel.Model!.Description,
                    Note = updatedModel.Model.Note,
                    Fields = updatedModel.Model.Fields,
                };

                // Updates the department
                var departmentResponse = await Client.UpdateDepartmentAsync(Model.Id, departmentRequest);

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
                Name = Model.Name,
                Category = Model.Category,
                Email = Model.Email,
                PhoneNumber = Model.PhoneNumber,
                ImageUrl = Model.ImageUrl,
                Color = Model.Color,
                Location = Model.Location,
                Quote = Model.Quote,
                Websites = Model.Websites
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
                    Location = updatedModel.Model.Location,
                    Quote = updatedModel.Model.Quote,
                    Websites = updatedModel.Model.Websites
                };

                // Updates the department
                var departmentResponse = await Client.UpdateDepartmentAsync(Model.Id, departmentRequest);

                // If there was an error...
                if (!departmentResponse.IsSuccessful)
                {
                    // Show the error
                    Snackbar.Add(departmentResponse.ErrorMessage, Severity.Error);
                    // Return
                    return;
                }
                StateManager.Department = departmentResponse.Result;

                // If an image was set...
                if (updatedModel.File is not null)
                {
                    // Adds the model
                    var responseWithImage = await Client.SetDepartmentImageAsync(Model.Id, updatedModel.File);

                    // If there was an error...
                    if (!responseWithImage.IsSuccessful)
                    {
                        Console.WriteLine(responseWithImage.ErrorMessage);
                        // Show the error
                        Snackbar.Add(responseWithImage.ErrorMessage, Severity.Error);
                        // Return
                        return;
                    }
                    StateManager.Department = responseWithImage.Result;
                }

                StateHasChanged();
                StateHasChanged();
            }
        }

        #endregion
    }
}
