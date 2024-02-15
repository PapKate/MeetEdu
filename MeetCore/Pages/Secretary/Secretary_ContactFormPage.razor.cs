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

        private bool mIsFirstNameChecked = true;
        private bool mIsLastNameChecked = true;
        private bool mIsEmailChecked = true;
        private bool mIsPhoneNumberChecked = false;

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

        #region Private Methods

        private void EmailCheckbox_IsCheckedChanged(bool value)
        {
            mIsEmailChecked = value;

            if (!mIsEmailChecked)
                mIsPhoneNumberChecked = true;
        }

        private void PhoneNumberCheckbox_IsCheckedChanged(bool value)
        {
            mIsPhoneNumberChecked = value;

            if (!mIsPhoneNumberChecked)
                mIsEmailChecked = true;
        }

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
