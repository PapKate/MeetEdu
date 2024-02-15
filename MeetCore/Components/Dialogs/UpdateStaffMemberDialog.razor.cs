using MeetBase;

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.AspNetCore.Http;

using MudBlazor;
using Microsoft.Extensions.Primitives;

namespace MeetCore
{
    /// <summary>
    /// Dialog for updating a professor
    /// </summary>
    public partial class UpdateStaffMemberDialog<T>
        where T : UpdateStaffMemberModel, new()
    {
        #region Private Members

        private IBrowserFile? mFile;

        /// <summary>
        /// The password
        /// </summary>
        private string mPassword = string.Empty;

        /// <summary>
        /// The confirm password
        /// </summary>
        private string mConfirmPassword = string.Empty;

        /// <summary>
        /// The password
        /// </summary>
        private string mPhotoLabel = "Profile photo";

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
        private DateTime? mBirthDate;

        /// <summary>
        /// The location
        /// </summary>
        private Location mLocation = new Location();

        #endregion

        #region Public Properties

        /// <summary>
        /// The dialog instance
        /// </summary>
        [CascadingParameter]
        public MudDialogInstance MudDialog { get; set; } = default!;

        /// <summary>
        /// The model
        /// </summary>
        [Parameter]
        public UpdateModel<T> Model { get; set; } = default!;

        /// <summary>
        /// A flag indicating whether the staff member is a secretary or not
        /// </summary>
        [Parameter]
        public bool IsSecretary { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public UpdateStaffMemberDialog() : base()
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

            if(Model is not null)
            {
                mCountryCode = Model.Model.PhoneNumber?.CountryCode ?? 30;
                mPhoneNumber = Model.Model.PhoneNumber?.Phone ?? string.Empty;
                mBirthDate = Model.Model.DateOfBirth?.ToDateTime() ?? DateTime.Now;
                mLocation = Model.Model.Location ?? new();
            }
        }

        #endregion

        #region Private Methods

        private async void Save()
        {
            if(Model is not null)
            {
                Model.Model.PhoneNumber = new PhoneNumber(mCountryCode, mPhoneNumber);
                Model.Model.Location = mLocation;
                Model.Model.DateOfBirth = mBirthDate?.ToDateOnly();

                if (!mPassword.IsNullOrEmpty() && mPassword == mConfirmPassword)
                    Model.Model.PasswordHash = mPassword.EncryptPassword();

                if (mFile is not null)
                {
                    var type = IsSecretary ? "Secretaries" : "Professors";
                    
                    Model.File = await mFile.ToIFormFileAsync(mPhotoLabel, new HeaderDictionary
                    {
                        { "userType", type }
                    });
                }
            }

            MudDialog.Close(DialogResult.Ok(Model));
        }

        private void Cancel()
        {
            MudDialog.Cancel();
        }

        #endregion
    }
}
