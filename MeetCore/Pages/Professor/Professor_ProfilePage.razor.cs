using MeetBase.Web;
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

        /// <summary>
        /// The <see cref="MarkDownInput"/> text
        /// </summary>
        private string? mText;

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

        #region Protected Methods

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        protected override void OnInitialized()
        {
            base.OnInitialized();

            mText = Professor.ResearchInterests ?? string.Empty;
        }

        #endregion

        #region Private Methods

        private async void SaveResearchInterests(string? value)
        {
            mText = value;
            // Creates the request for updating the professor
            var professorRequest = new ProfessorRequestModel()
            {
                ResearchInterests = mText
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

            StateHasChanged();
        }

        /// <summary>
        /// Updates the secretary and user info
        /// </summary>
        private async void UpdateProfessor()
        {
            var model = new UpdateStaffMemberModel<ProfessorRequestModel>(
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
                Quote = Professor.Quote,
                Rank = Professor.Rank,
                Websites = Professor.Websites
            });

            var parameters = new DialogParameters<UpdateStaffMemberDialog<ProfessorRequestModel>> { { x => x.Model, model }, { x => x.IsSecretary, false } };

            // Creates and opens a dialog with the specified type
            var dialog = await DialogService.ShowAsync<UpdateStaffMemberDialog<ProfessorRequestModel>>(null, parameters, mDialogOptions);

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
            if (result.Data is UpdateStaffMemberModel<ProfessorRequestModel> updatedModel)
            {
                // Creates the request for updating the professor
                var professorRequest = new ProfessorRequestModel()
                {
                    Rank = updatedModel.StaffMember!.Rank,
                    Websites = updatedModel.StaffMember!.Websites,
                    Quote = updatedModel.StaffMember.Quote ?? string.Empty
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
                    Username = updatedModel.Model!.Username,
                    FirstName = updatedModel.Model.FirstName,
                    LastName = updatedModel.Model.LastName,
                    PasswordHash = updatedModel.Model.PasswordHash,
                    Email = updatedModel.Model.Email,
                    PhoneNumber = updatedModel.Model.PhoneNumber,
                    DateOfBirth = updatedModel.Model.DateOfBirth,
                    ImageUrl = updatedModel.Model.ImageUrl,
                    Location = updatedModel.Model.Location,
                    Color = updatedModel.Model.Color!.Replace("#", string.Empty)
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

                // If an image was set...
                if (updatedModel.File is not null)
                {
                    // Adds the model
                    var responseWithImage = await Client.SetUserImageAsync(Professor.UserId, updatedModel.File);

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
