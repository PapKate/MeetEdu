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
        /// The state management service
        /// </summary>
        [Inject]
        protected StateManagerCore StateManager { get; set; } = default!;

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public Secretary_ProfilePage() : base()
        {

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
