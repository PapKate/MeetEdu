using MeetBase;

using Microsoft.AspNetCore.Components;

namespace MeetCore
{
    /// <summary>
    /// The home page
    /// </summary>
    public partial class Index : StateManagablePage
    {
        #region Private Properties

        /// <summary>
        /// The member of the <see cref="IsLoginActive"/>
        /// </summary>
        private bool mIsLoginActive = true;

        /// <summary>
        /// The member of the <see cref="IsForgotPasswordActive"/>
        /// </summary>
        private bool mIsForgotPasswordActive = false;

        /// <summary>
        /// The member of the <see cref="IsResetPasswordActive"/>
        /// </summary>
        private bool mIsResetPasswordActive = false;

        /// <summary>
        /// The button text
        /// </summary>
        private string mButtonText = EnglishLocalization.Login;

        /// <summary>
        /// The alternative text
        /// </summary>
        private string mAlternativeText = EnglishLocalization.ForgotPassword;

        #endregion

        #region Public Properties

        /// <summary>
        /// The username
        /// </summary>
        [Parameter]
        public string? Username { get; set; }

        /// <summary>
        /// The password
        /// </summary>
        public string? Password { get; set; }

        /// <summary>
        /// The confirmed password
        /// </summary>
        public string? ConfirmPassword { get; set; }

        /// <summary>
        /// The temporary password
        /// </summary>
        public string? TemporaryPassword { get; set; }

        /// <summary>
        /// The email
        /// </summary>
        public string? Email { get; set; }

        /// <summary>
        /// A flag indicating whether the login container is visible
        /// </summary>
        public bool IsLoginActive
        {
            get => mIsLoginActive;
            set
            {
                mIsLoginActive = value;

                if(mIsLoginActive)
                {
                    mAlternativeText = EnglishLocalization.ForgotPassword;
                    mButtonText = EnglishLocalization.Login;
                }
                else
                {
                    mAlternativeText = EnglishLocalization.BackToLogin;
                }
            }
        }

        /// <summary>
        /// A flag indicating whether the forgot password container is visible
        /// </summary>
        public bool IsForgotPasswordActive
        {
            get => mIsForgotPasswordActive;
            set
            {
                mIsForgotPasswordActive = value;
                if(mIsForgotPasswordActive)
                    mButtonText = EnglishLocalization.Continue;
            }
        }

        /// <summary>
        /// A flag indicating whether the reset password container is visible
        /// </summary>
        public bool IsResetPasswordActive
        {
            get => mIsResetPasswordActive;
            set
            {
                mIsResetPasswordActive = value;
                if(mIsResetPasswordActive)
                    mButtonText = EnglishLocalization.Reset;
            }
        }

        #endregion

        #region Protected Properties

        /// <summary>
        /// The navigation manager service
        /// </summary>
        [Inject]
        public NavigationManager? NavigationManager { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public Index() : base()
        {

        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Handles the click of the form
        /// </summary>
        private void FormButton_OnClick()
        {
            // If the login is active...
            if(IsLoginActive)
            {
                if(!Password.IsNullOrEmpty() && Password.Contains("123"))
                {
                    Password = string.Empty;
                    IsResetPasswordActive = true;
                    IsLoginActive = false;
                }
                // Login
                // Get user
                // Get user type - secretary or professor
                var isSecretary = true;

                // If the state manager is set...
                if(StateManager is not null)
                    StateManager.SetIsSecretary(isSecretary);

                // If the navigation manager is not null...
                if(NavigationManager is not null)
                {
                    // If the connected user is a secretary...
                    if (isSecretary)
                        NavigationManager.Secretary_NavigateToProfilePage("id");
                    else
                        NavigationManager.Professor_NavigateToProfilePage("id");
                }

            }
            // Else if the forgot password is active...
            else if (IsForgotPasswordActive)
            {
                IsForgotPasswordActive = false;
                IsLoginActive = true;
            }
            // Else if the reset password is active
            else if(IsResetPasswordActive)
            {
                IsLoginActive = true;
                IsResetPasswordActive = false;
            }
        }

        /// <summary>
        /// Navigates throw the login flow
        /// </summary>
        private void AlternativeText_OnClick()
        {
            // If the login is active...
            if (IsLoginActive)
            {
                // Makes the necessary changes to continue to forgot password
                IsForgotPasswordActive = true;
                IsLoginActive = false;
            }
            // Else if the forgot password is active...
            else if(IsForgotPasswordActive)
            {
                // Makes the necessary changes to continue to reset password
                IsLoginActive = true;
                IsForgotPasswordActive = false;
            }
            // Else...
            else if(IsResetPasswordActive)
            {
                // Makes the necessary changes to go back to login
                IsLoginActive = true;
                IsResetPasswordActive = false;
            }
        }

        #endregion
    }
}
