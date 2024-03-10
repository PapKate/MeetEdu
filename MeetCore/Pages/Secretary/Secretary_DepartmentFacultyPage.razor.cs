using MeetBase.Web;
using Microsoft.AspNetCore.Components;

using MudBlazor;

using static MudBlazor.CategoryTypes;

namespace MeetCore
{
    /// <summary>
    /// The faculty page
    /// </summary>
    public partial class Secretary_DepartmentFacultyPage : BasePage
    {
        #region Private Members

        /// <summary>
        /// The <see cref="MudDialog"/> options
        /// </summary>
        private DialogOptions mDialogOptions = new() { FullWidth = true };

        private IEnumerable<ProfessorResponseModel> mProfessors = new List<ProfessorResponseModel>();

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
        public Secretary_DepartmentFacultyPage() : base()
        {

        }

        #endregion

        #region Protected Methods

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        protected override async void OnInitialized()
        {
            base.OnInitialized();

            var response = await Client.GetProfessorsAsync(new() { IncludeDepartments = new List<string>() { StateManager.Department!.Id } });

            // If there was an error...
            if (!response.IsSuccessful)
            {
                // Show the error
                Snackbar.Add(response.ErrorMessage, Severity.Info);
                // Return
                return;
            }
            mProfessors = response.Result;
            
            StateHasChanged();
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Updates the specified <paramref name="professor"/>'s rank
        /// </summary>
        /// <param name="professor">The professor</param>
        private async void EditProfessorRank(ProfessorResponseModel professor)
        {
            // Creates the request for updating the secretary
            var professorRequest = new ProfessorRequestModel()
            {
                Rank = professor.Rank
            };

            // Updates the secretary
            var professorResponse = await Client.UpdateProfessorAsync(professor.Id, professorRequest);

            // If there was an error...
            if (!professorResponse.IsSuccessful)
            {
                // Show the error
                Snackbar.Add(professorResponse.ErrorMessage, Severity.Error);
                // Return
                return;
            }

            StateHasChanged();
        }
        
        /// <summary>
        /// Adds a professor
        /// </summary>
        private async void AddProfessor()
        {
            // Creates and opens a dialog with the specified type
            var dialog = await DialogService.ShowAsync<AddProfessorDialog>(null, mDialogOptions);

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
            if (result.Data is AddProfessorModel model)
            {
                // Creates the request for adding the user
                var userRequest = new UserRequestModel()
                {
                    Username = model.Username,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                    PhoneNumber = model.PhoneNumber,
                    PasswordHash = "newUser",
                    Color = model.Color!.Replace("#", string.Empty)
                };

                // Adding the user
                var userResponse = await Client.AddUserAsync(userRequest);

                // If there was an error...
                if (!userResponse.IsSuccessful)
                {
                    // Show the error
                    Snackbar.Add(userResponse.ErrorMessage, Severity.Error);
                    // Return
                    return;
                }

                // Creates the request for adding the professor
                var professorRequest = new ProfessorRequestModel()
                {
                    UserId = userResponse.Result.Id,
                    DepartmentId = StateManager.Department!.Id,
                    Rank = model.Rank
                };

                // Adds the professor
                var professorResponse = await Client.AddProfessorAsync(professorRequest);

                // If there was an error...
                if (!professorResponse.IsSuccessful)
                {
                    // Show the error
                    Snackbar.Add(professorResponse.ErrorMessage, Severity.Error);
                    // Return
                    return;
                }
                var newList = new List<ProfessorResponseModel>();
                newList.AddRange(mProfessors);
                newList.Add(professorResponse.Result);
                mProfessors = newList;

                StateHasChanged();
            }
        }

        #endregion
    }
}
