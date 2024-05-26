using MeetBase;
using MeetBase.Blazor;
using MeetBase.Web;

using Microsoft.AspNetCore.Components;

using MudBlazor;

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
        protected NavigationManager NavigationManager { get; set; } = default!;

        /// <summary>
        /// The <see cref="MudBlazor"/> snack bar manager
        /// </summary>
        [Inject]
        protected ISnackbar Snackbar { get; set; } = default!;

        /// <summary>
        /// The <see cref="MeetCoreHubClient"/>
        /// </summary>
        [Inject]
        protected MeetCoreHubClient HubClient { get; set; } = default!;

        /// <summary>
        /// The header manager for displaying the user data
        /// </summary>
        [Inject]
        protected HeaderUserManager HeaderManager { get; set; } = default!;

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public Index() : base()
        {

        }

        #endregion

        #region Protected Methods

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        protected override async void OnInitializedCore()
        {
            // Resets the state manager values
            StateManager.ResetManager();
            await SessionStorageManager.ClearAsync();
            StateManager.OnStateChange += StateHasChanged;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Handles the click of the form
        /// </summary>
        private async void FormButton_OnClick()
        {
            // If the login is active...
            if(IsLoginActive)
            {
                // Gets the user
                var response = await Client.LoginAsync(new LogInRequestModel() 
                {
                    Username = Username,
                    Password = Password
                });

                Password = string.Empty;

                // If there was an error...
                if (!response.IsSuccessful || response.Result is null || response.Result.User is null)
                {
                    Console.WriteLine(response.ErrorMessage);
                    // Show the error
                    Snackbar.Add(response.ErrorMessage, Severity.Error);
                    // Return
                    return;
                }

                // If a member tried to login...
                if (response.Result.Member is not null)
                {
                    // Show the error
                    Snackbar.Add("Access denied. Members cannot access the application.", Severity.Error);
                    // Return
                    return;
                }
                
                var user = response.Result.User;
                await SessionStorageManager.SetValueAsync(SessionStorageManager.UserId, response.Result.User.Id);

                // Get user type - secretary or professor
                var isSecretary = response.Result.Secretary is not null;
                await SessionStorageManager.SetValueAsync(SessionStorageManager.IsSecretary, isSecretary);

                HeaderManager.SetValues(user.Username, user.ImageUrl, user.Color);

                // If the connected user is a secretary...
                if (isSecretary)
                {
                    var departmentResponse = await Client.GetDepartmentAsync(response.Result.Secretary!.DepartmentId);

                    // If there was an error...
                    if (!departmentResponse.IsSuccessful)
                    {
                        // Show the error
                        Snackbar.Add(departmentResponse.ErrorMessage, Severity.Error);
                        // Return
                        return;
                    }

                    Client.DepartmentId = departmentResponse.Result.Id;
                    
                    await SessionStorageManager.SetValueAsync(SessionStorageManager.DepartmentId, departmentResponse.Result.Id);
                    await SessionStorageManager.SetValueAsync(SessionStorageManager.SecretaryId, response.Result.Secretary.Id);

                    StateManager.SetLoggedInUserData(isSecretary, response.Result.User, response.Result.Secretary, departmentResponse.Result);
                    NavigationManager.Secretary_NavigateToProfilePage(response.Result.Secretary!.Id);
                }
                else
                {
                    var departmentResponse = await Client.GetDepartmentAsync(response.Result.Professor!.DepartmentId);

                    // If there was an error...
                    if (!departmentResponse.IsSuccessful)
                    {
                        // Show the error
                        Snackbar.Add(departmentResponse.ErrorMessage, Severity.Error);
                        // Return
                        return;
                    }

                    Client.DepartmentId = departmentResponse.Result.Id;

                    await SessionStorageManager.SetValueAsync(SessionStorageManager.DepartmentId, departmentResponse.Result.Id);
                    await SessionStorageManager.SetValueAsync(SessionStorageManager.ProfessorId, response.Result.Professor.Id);

                    StateManager.SetLoggedInUserData(isSecretary, response.Result.User, response.Result.Professor, departmentResponse.Result);
                    NavigationManager.Professor_NavigateToProfilePage(response.Result.Professor!.Id);
                }

                if(!HubClient.IsConnected)
                    await HubClient.ConnectAsync();
            }
            // Else if the forgot password is active...
            else if (IsForgotPasswordActive)
            {
                // If the provided email is not in correct format...
                if (!Email.IsEmail())
                {
                    // Show the error
                    Snackbar.Add("Incorrect email format. Please try again.", Severity.Error);
                    // Return
                    return;
                }

                Console.WriteLine(Email);
                Console.WriteLine("Temp password = TempCore123!@");

                IsForgotPasswordActive = false;
                IsResetPasswordActive = true;
            }
            // Else if the reset password is active
            else if(IsResetPasswordActive)
            {
                var response = await Client.ResetUserPasswordAsync(new ResetPasswordRequestModel(Email, TemporaryPassword, Password, ConfirmPassword));

                // If there was an error...
                if (!response.IsSuccessful)
                {
                    // Show the error
                    Snackbar.Add(response.ErrorMessage, Severity.Error);
                    // Return
                    return;
                }

                Snackbar.Add("Success", Severity.Success);

                Email = string.Empty; 
                TemporaryPassword = string.Empty; 
                Password = string.Empty; 
                ConfirmPassword = string.Empty;
                
                IsLoginActive = true;
                IsResetPasswordActive = false;
                StateHasChanged();
            }
        }

        /// <summary>
        /// Navigates throw the login flow
        /// </summary>
        private void AlternativeText_OnClick()
        {
            Password = string.Empty;

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
