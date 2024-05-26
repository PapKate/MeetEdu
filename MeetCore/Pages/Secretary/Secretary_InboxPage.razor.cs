using MeetBase;
using MeetBase.Web;

using Microsoft.AspNetCore.Components;

using MudBlazor;

namespace MeetCore
{
    /// <summary>
    /// The inbox page
    /// </summary>
    public partial class Secretary_InboxPage : BasePage
    {
        #region Private Members

        /// <summary>
        /// The <see cref="MudDialog"/> options
        /// </summary>
        private DialogOptions mDialogOptions = new() { FullWidth = true };

        private IEnumerable<DepartmentContactMessageResponseModel> mDepartmentMessages = new List<DepartmentContactMessageResponseModel>();

        private IEnumerable<DepartmentContactMessageResponseModel> mSecretaryMessages = new List<DepartmentContactMessageResponseModel>();

        #endregion

        #region Public Properties

        /// <summary>
        /// The secretary
        /// </summary>
        public SecretaryResponseModel Secretary => StateManager.Secretary!;

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
        public Secretary_InboxPage() : base()
        {

        }

        #endregion

        #region Protected Methods

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        protected override async void OnInitializedCore()
        {
            var response = await Client.GetDepartmentContactMessagesAsync(new() { IncludeDepartments = new List<string>() { StateManager.Department!.Id } });

            // If there was an error...
            if (!response.IsSuccessful)
            {
                // Show the error
                Snackbar.Add(response.ErrorMessage, Severity.Info);
                // Return
                return;
            }

            var messages = response.Result.ToList();

            mDepartmentMessages = messages.Where(x => x.Reply.IsNullOrEmpty()).ToList();

            mSecretaryMessages = messages.Where(x => x.SecretaryId == Secretary.Id).ToList();

            StateHasChanged();
        }

        #endregion

        #region Private Methods

        private async void ViewMessage(DepartmentContactMessageResponseModel message)
        {
            var model = new DepartmentMesageModel()
            {
                Color = Department.Color,
                Model = message,
            };

            var parameters = new DialogParameters<DepartmentMessageDialog> { { x => x.Model, model } };

            // Creates and opens a dialog with the specified type
            var dialog = await DialogService.ShowAsync<DepartmentMessageDialog>(null, parameters, mDialogOptions);

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
            if (result.Data is DepartmentMesageModel updatedModel)
            {
                // Creates the request for updating the secretary
                var messageRequest = new DepartmentContactMessageRequestModel()
                {
                    SecretaryId = Secretary.Id,
                    DepartmentId = StateManager.Department!.Id,
                    Reply = updatedModel.Model!.Reply,
                    Role = updatedModel.Model.Role
                };

                // Updates the message
                var messageResponse = await Client.UpdateDepartmentContactMessageAsync(updatedModel.Model.Id, messageRequest);

                // If there was an error...
                if (!messageResponse.IsSuccessful)
                {
                    Console.WriteLine(messageResponse.ErrorMessage);
                    // Show the error
                    Snackbar.Add(messageResponse.ErrorMessage, Severity.Error);
                    // Return
                    return;
                }
                
                Snackbar.Add("Changes saved", Severity.Success);
                
                StateHasChanged();
            }
        }

        #endregion
    }
}
