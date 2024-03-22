using MeetBase.Web;

using Microsoft.AspNetCore.Components;

using MudBlazor;

namespace MeetCore
{
    /// <summary>
    /// The profile page
    /// </summary>
    public partial class Secretary_ProfilePage : BasePage
    {
        #region Private Members

        /// <summary>
        /// The <see cref="MudDialog"/> options
        /// </summary>
        private DialogOptions mDialogOptions = new() { FullWidth = true };

        #endregion

        #region Public Properties

        /// <summary>
        /// The secretary
        /// </summary>
        public SecretaryResponseModel Secretary => StateManager.Secretary!;

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
        public Secretary_ProfilePage() : base()
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
                WeeklySchedule = Secretary.WeeklySchedule,
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
            if(result.Data is UpdateScheduleModel updatedModel)
            {
                // Creates the request for updating the secretary
                var secretaryRequest = new SecretaryRequestModel()
                {
                    WeeklySchedule = updatedModel.WeeklySchedule
                };

                // Updates the secretary
                var secretaryResponse = await Client.UpdateSecretaryAsync(Secretary.Id, secretaryRequest);

                // If there was an error...
                if (!secretaryResponse.IsSuccessful)
                {
                    Console.WriteLine(secretaryResponse.ErrorMessage);
                    // Show the error
                    Snackbar.Add(secretaryResponse.ErrorMessage, Severity.Error);
                    // Return
                    return;
                }
                StateManager.Secretary = secretaryResponse.Result;

                StateHasChanged();
            }
        }

        /// <summary>
        /// Updates the secretary and user info
        /// </summary>
        private async void UpdateSecretary()
        {
            var model = new UpdateStaffMemberModel<SecretaryRequestModel> (
            new()
            {
                Username = User.Username,
                Email = User.Email,
                Color = User.Color,
                FirstName = User.FirstName,
                LastName = User.LastName,
                PhoneNumber = User.PhoneNumber,
                DateOfBirth = User.DateOfBirth,
                Location = User.Location, 
                ImageUrl = User.ImageUrl
            },
            new() 
            {
                Quote = Secretary.Quote,
                Role = Secretary.Role
            });

            var parameters = new DialogParameters<UpdateStaffMemberDialog<SecretaryRequestModel>> { { x => x.Model, model }, { x => x.IsSecretary, true } };

            // Creates and opens a dialog with the specified type
            var dialog = await DialogService.ShowAsync<UpdateStaffMemberDialog<SecretaryRequestModel>>(null, parameters, mDialogOptions);
            
            // Once the dialog is closed...
            // Gets the result
            var result = await dialog.Result;

            // If there is no result or the dialog was closed by canceling the inner actions...
            if(result is null || result.Canceled)
            {
                // Return
                return;
            }

            // If the result is of the specified type...
            if(result.Data is UpdateStaffMemberModel<SecretaryRequestModel> updatedModel)
            {
                // Creates the request for updating the secretary
                var secretaryRequest = new SecretaryRequestModel()
                {
                    Role = updatedModel.StaffMember!.Role,
                    Quote = updatedModel.StaffMember!.Quote ?? string.Empty
                };

                // Updates the secretary
                var secretaryResponse = await Client.UpdateSecretaryAsync(Secretary.Id, secretaryRequest);

                // If there was an error...
                if (!secretaryResponse.IsSuccessful)
                {
                    // Show the error
                    Snackbar.Add(secretaryResponse.ErrorMessage, Severity.Error);
                    // Return
                    return;
                }
                StateManager.Secretary = secretaryResponse.Result;

                // Creates the request for updating the user
                var userRequest = new UserRequestModel()
                {
                    Username = updatedModel.Model!.Username,
                    FirstName = updatedModel.Model.FirstName,
                    LastName = updatedModel.Model.LastName,
                    PasswordHash = updatedModel.Model.PasswordHash,
                    Email = updatedModel.Model.Email,
                    PhoneNumber = updatedModel.Model.PhoneNumber,
                    DateOfBirth = updatedModel.Model.DateOfBirth,
                    Location = updatedModel.Model.Location,
                    Color = updatedModel.Model.Color!.Replace("#", string.Empty)
                };

                // Updates the user
                var userResponse = await Client.UpdateUserAsync(Secretary.UserId, userRequest);

                // If there was an error...
                if (!userResponse.IsSuccessful)
                {
                    // Show the error
                    Snackbar.Add(userResponse.ErrorMessage, Severity.Error);
                    // Return
                    return;
                }

                StateManager.User = userResponse.Result;

                // If an image was set...
                if (updatedModel.File is not null)
                {
                    // Adds the model
                    var responseWithImage = await Client.SetUserImageAsync(Secretary.UserId, updatedModel.File);

                    // If there was an error...
                    if (!responseWithImage.IsSuccessful)
                    {
                        Console.WriteLine(responseWithImage.ErrorMessage);
                        // Show the error
                        Snackbar.Add(responseWithImage.ErrorMessage, Severity.Error);
                        // Return
                        return;
                    }
                    StateManager.User = responseWithImage.Result;
                }

                StateHasChanged();
            }
        }

        #endregion
    }
}
