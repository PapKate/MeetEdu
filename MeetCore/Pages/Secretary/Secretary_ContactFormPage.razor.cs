using MeetBase.Web;

using Microsoft.AspNetCore.Components;

using MudBlazor;

namespace MeetCore
{
    /// <summary>
    /// The contact form page
    /// </summary>
    public partial class Secretary_ContactFormPage : BasePage
    {
        #region Private Members
       
        private string mText = string.Empty;

        private bool mIsFirstNameChecked = false;

        private int mSelectedIndex = 0;

        #endregion

        #region Public Properties

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
        public Secretary_ContactFormPage() : base()
        {

        }

        #endregion

        #region Protected Methods

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        protected override async void OnInitialized()
        {
            //var response = await Client.GetDepartmentContactMessagesAsync(new() { IncludeDepartments = new List<string>() { StateManager.Department!.Id } });

            //if (!response.IsSuccessful)
            //{
            //    Console.WriteLine(response.ErrorMessage);
            //    // Show the error
            //    Snackbar.Add(response.ErrorMessage, Severity.Info);
            //    return;
            //}

            //if (!response.Result.Any())
            //{
            //    Snackbar.Add("No layouts", Severity.Error);
            //    return;
            //}
            //var layout = response.Result.First();
            //mLayoutDescription = layout.Description;
        }

        #endregion

        #region Private Methods

        private void SaveFormDescription(string? value)
        {
            var test = value;
        }

        private void SetSelectedIndex(int index)
        {
            mSelectedIndex = index;
        }

        #endregion
    }
}
