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

        private IEnumerable<DepartmentContactMessageResponseModel> mContactMessages = new List<DepartmentContactMessageResponseModel>();

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
        public Secretary_InboxPage() : base()
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

            //var response = await Client.GetDepartmentContactMessagesAsync(new() { IncludeDepartments = new List<string>() { StateManager.Department!.Id } });

            //// If there was an error...
            //if (!response.IsSuccessful)
            //{
            //    // Show the error
            //    Snackbar.Add(response.ErrorMessage, Severity.Info);
            //    // Return
            //    return;
            //}
            //mContactMessages = response.Result;

            mContactMessages = new List<DepartmentContactMessageResponseModel>()
            {
                new()
                {
                    FirstName = "Katherine",
                    LastName = "Papadopoulou",
                    Email = "mail@mail.com",
                    Message = "This is a message"
                }
            };

            StateHasChanged();
        }

        #endregion

        #region Private Methods

        private void ViewMessage()
        {

        }

        #endregion
    }
}
