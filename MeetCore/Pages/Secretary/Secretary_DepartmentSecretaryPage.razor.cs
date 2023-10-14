using MeetBase.Web;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace MeetCore
{
    /// <summary>
    /// The department secretary page
    /// </summary>
    public partial class Secretary_DepartmentSecretaryPage : BasePage
    {
        #region Private Members

        /// <summary>
        /// The <see cref="MudDialog"/> options
        /// </summary>
        private DialogOptions mDialogOptions = new() { FullWidth = true };

        private IEnumerable<SecretaryResponseModel> mSecretaries = new List<SecretaryResponseModel>();

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
        public Secretary_DepartmentSecretaryPage() : base()
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

            var response = await Client.GetSecretariesAsync(new() { IncludeDepartments = new List<string>() { StateManager.Department!.Id } });

            // If there was an error...
            if (!response.IsSuccessful)
            {
                // Show the error
                Snackbar.Add(response.ErrorMessage, Severity.Info);
                // Return
                return;
            }
            mSecretaries = response.Result;

            StateHasChanged();
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Updates the specified <paramref name="secretary"/>'s rank
        /// </summary>
        /// <param name="secretary">The professor</param>
        private async void EditSecretaryRole(SecretaryResponseModel secretary)
        {
            // Creates the request for updating the secretary role
            var secretaryRequest = new SecretaryRequestModel()
            {
                Role = secretary.Role
            };

            // Updates the secretary
            var secretaryResponse = await Client.UpdateSecretaryAsync(secretary.Id, secretaryRequest);

            // If there was an error...
            if (!secretaryResponse.IsSuccessful)
            {
                // Show the error
                Snackbar.Add(secretaryResponse.ErrorMessage, Severity.Error);
                // Return
                return;
            }

            StateHasChanged();
        }

        /// <summary>
        /// Adds a secretary
        /// </summary>
        private async void AddSecretary()
        {
            // Creates and opens a dialog with the specified type
            var dialog = await DialogService.ShowAsync<AddSecretaryDialog>(null, mDialogOptions);

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
            if (result.Data is AddSecretaryModel model)
            {
                // Creates the request for updating the user
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

                // Updates the user
                var userResponse = await Client.AddUserAsync(userRequest);

                // If there was an error...
                if (!userResponse.IsSuccessful)
                {
                    // Show the error
                    Snackbar.Add(userResponse.ErrorMessage, Severity.Error);
                    // Return
                    return;
                }

                // Creates the request for updating the secretary
                var secretaryRequest = new SecretaryRequestModel()
                {
                    UserId = userResponse.Result.Id,
                    DepartmentId = StateManager.Department!.Id,
                    Role = model.Role
                };

                // Updates the secretary
                var secretaryResponse = await Client.AddSecretaryAsync(secretaryRequest);

                // If there was an error...
                if (!secretaryResponse.IsSuccessful)
                {
                    // Show the error
                    Snackbar.Add(secretaryResponse.ErrorMessage, Severity.Error);
                    // Return
                    return;
                }
                var newList = new List<SecretaryResponseModel>();
                newList.AddRange(mSecretaries);
                newList.Add(secretaryResponse.Result);
                mSecretaries = newList;

                StateHasChanged();
            }
        }

        #endregion
    }
}
