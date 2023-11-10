﻿using MeetBase.Web;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace MeetCore
{
    /// <summary>
    /// The profile page
    /// </summary>
    public partial class Professor_ProfilePage : BasePage
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
        /// The client
        /// </summary>
        [Inject]
        protected MeetCoreClient Client { get; set; } = default!;

        /// <summary>
        /// The state management service
        /// </summary>
        [Inject]
        protected StateManagerCore StateManager { get; set; } = default!;

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
        public Professor_ProfilePage() : base()
        {

        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Updates the secretary and user info
        /// </summary>
        private async void UpdateProfessor()
        {
            var model = new UpdateProfessorModel()
            {
                Username = User.Username,
                Email = User.Email,
                Color = User.Color,
                FirstName = User.FirstName,
                LastName = User.LastName,
                PhoneNumber = User.PhoneNumber,
                DateOfBirth = User.DateOfBirth,
                Quote = Professor.Quote,
                Rank = Professor.Rank,
                Location = User.Location,
                ImageUrl = User.ImageUrl
            };

            var parameters = new DialogParameters<UpdateStaffMemberDialog<UpdateProfessorModel>> { { x => x.Model, model } };

            // Creates and opens a dialog with the specified type
            var dialog = await DialogService.ShowAsync<UpdateStaffMemberDialog<UpdateProfessorModel>>(null, parameters, mDialogOptions);

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
            if (result.Data is UpdateProfessorModel updatedModel)
            {
                // Creates the request for updating the professor
                var professorRequest = new ProfessorRequestModel()
                {
                    Rank = updatedModel.Rank,
                    Quote = updatedModel.Quote ?? string.Empty
                };

                // Updates the professor
                var professorResponse = await Client.UpdateProfessorAsync(Professor.Id, professorRequest);

                // If there was an error...
                if (!professorResponse.IsSuccessful)
                {
                    // Show the error
                    Snackbar.Add(professorResponse.ErrorMessage, Severity.Error);
                    // Return
                    return;
                }
                StateManager.Professor = professorResponse.Result;

                // Creates the request for updating the user
                var userRequest = new UserRequestModel()
                {
                    Username = updatedModel.Username,
                    FirstName = updatedModel.FirstName,
                    LastName = updatedModel.LastName,
                    PasswordHash = updatedModel.PasswordHash,
                    Email = updatedModel.Email,
                    PhoneNumber = updatedModel.PhoneNumber,
                    DateOfBirth = updatedModel.DateOfBirth,
                    ImageUrl = updatedModel.ImageUrl,
                    Location = updatedModel.Location,
                    Color = updatedModel.Color!.Replace("#", string.Empty)
                };

                // Updates the user
                var userResponse = await Client.UpdateUserAsync(Professor.UserId, userRequest);

                // If there was an error...
                if (!userResponse.IsSuccessful)
                {
                    // Show the error
                    Snackbar.Add(userResponse.ErrorMessage, Severity.Error);
                    // Return
                    return;
                }
                StateManager.User = userResponse.Result;

                StateHasChanged();
            }
        }

        #endregion
    }
}
