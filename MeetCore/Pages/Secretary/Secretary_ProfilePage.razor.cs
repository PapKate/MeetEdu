using MeetBase;
using MeetBase.Blazor;
using MeetBase.Web;

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

using MudBlazor;

namespace MeetCore
{
    /// <summary>
    /// The profile page
    /// </summary>
    public partial class Secretary_ProfilePage : BasePage
    {
        #region Private Properties

        /// <summary>
        /// A flag indicating whether the <see cref="MudDialog"/> is open or not
        /// </summary>
        private bool mIsDialogOpen;

        /// <summary>
        /// A flag indicating whether the <see cref="MudDialog"/> for editing the work hours is open or not
        /// </summary>
        private bool mIsScheduleDialogOpen;

        /// <summary>
        /// The <see cref="MudDialog"/> options
        /// </summary>
        private DialogOptions mDialogOptions = new() { FullWidth = true };

        /// <summary>
        /// The request model for updating the user details
        /// </summary>
        private UserRequestModel mUserRequestModel = new();

        /// <summary>
        /// The request model for updating the secretary details
        /// </summary>
        private SecretaryRequestModel mSecretaryRequestModel = new();

        /// <summary>
        /// The location
        /// </summary>
        private Location mLocation = new Location();

        /// <summary>
        /// The weekly schedule
        /// </summary>
        private WeeklySchedule mWeeklySchedule = new WeeklySchedule();

        /// <summary>
        /// The password
        /// </summary>
        private string mPhotoLabel = "Profile photo";

        /// <summary>
        /// The password
        /// </summary>
        private string mPassword = string.Empty;

        /// <summary>
        /// The confirm password
        /// </summary>
        private string mConfirmPassword = string.Empty;

        /// <summary>
        /// The country code
        /// </summary>
        private int mCountryCode = 30;

        /// <summary>
        /// The phone number
        /// </summary>
        private string mPhoneNumber = string.Empty;

        /// <summary>
        /// The birth date
        /// </summary>
        private DateTime? mBirthDate = DateTime.Now;

        #endregion

        #region Public Properties

        /// <summary>
        /// The secretary
        /// </summary>
        public SecretaryResponseModel Secretary => StateManager.Secretary!;

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
        /// The <see cref="MudBlazor"/> snack bar manager
        /// </summary>
        [Inject]
        protected ISnackbar Snackbar { get; set; } = default!;

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public Secretary_ProfilePage() : base()
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
            mPhoneNumber = User.PhoneNumber?.Phone ?? string.Empty;
            mCountryCode = User.PhoneNumber?.CountryCode ?? (int)CountryCode.GR;
            mLocation = User.Location ?? new();
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Opens the dialog
        /// </summary>
        private void OpenDialog()
        {
            mIsDialogOpen = true;

            mUserRequestModel = new()
            {
                Username = User.Username,
                Email = User.Email,
                Color = User.Color.NormalizedColor(),
                FirstName = User.FirstName,
                LastName = User.LastName,
                PhoneNumber = User.PhoneNumber,
                DateOfBirth = User.DateOfBirth,
            };
            mCountryCode = User.PhoneNumber?.CountryCode ?? 30;
            mPhoneNumber = User.PhoneNumber?.Phone ?? string.Empty;

            mSecretaryRequestModel = new()
            {
                Quote = Secretary.Quote
            };
        }

        /// <summary>
        /// Closes the dialog
        /// </summary>
        private void CloseDialog() => mIsDialogOpen = false;

        /// <summary>
        /// Updates the secretary and user info
        /// </summary>
        private async void SaveChanges()
        {
            // Updates the secretary
            var secretaryResponse = await Client.UpdateSecretaryAsync(Secretary.Id, mSecretaryRequestModel);
            
            // If there was an error...
            if(!secretaryResponse.IsSuccessful)
            {
                // Show the error
                Snackbar.Add(secretaryResponse.ErrorMessage, Severity.Error);
                // Return
                return;
            }
            StateManager.Secretary = secretaryResponse.Result;

            mUserRequestModel.Location = mLocation;
            mUserRequestModel.PhoneNumber = new PhoneNumber(mCountryCode, mPhoneNumber);
            mUserRequestModel.Color = mUserRequestModel.Color!.Replace("#", string.Empty);
            // Updates the user
            var userResponse = await Client.UpdateUserAsync(Secretary.UserId, mUserRequestModel);
            
            // If there was an error...
            if (!userResponse.IsSuccessful)
            {
                // Show the error
                Snackbar.Add(userResponse.ErrorMessage, Severity.Error);
                // Return
                return;
            }
            StateManager.User = userResponse.Result;
            CloseDialog();
            StateHasChanged();
        }

        private void CancelChanges()
        {
            CloseDialog();
        }

        private async void BrowserFileUploaded(IBrowserFile file)
        {
            var buffers = new byte[file.Size];
            await file.OpenReadStream().ReadAsync(buffers);
            var imageType = file.ContentType;
            var imgUrl = $"data:{imageType};base64,{Convert.ToBase64String(buffers)}";
        }

        #endregion
    }
}
