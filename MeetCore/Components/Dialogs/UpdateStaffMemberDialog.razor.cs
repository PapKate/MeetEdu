using MeetBase;
using MeetBase.Web;

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;

using MudBlazor;

namespace MeetCore
{
    /// <summary>
    /// Dialog for updating a professor
    /// </summary>
    public partial class UpdateStaffMemberDialog<TStaffMember>
        where TStaffMember : StaffMemberRequestModel, new()
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
        /// The websites
        /// </summary>
        private List<Website> mWebsites = new();

        /// <summary>
        /// The current website
        /// </summary>
        private Website mWebsite = new();

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
        public UpdateStaffMemberModel<TStaffMember> Model { get; set; } = default!;

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
                mCountryCode = Model.Model!.PhoneNumber?.CountryCode ?? 30;
                mPhoneNumber = Model.Model.PhoneNumber?.Phone ?? string.Empty;
                mBirthDate = Model.Model.DateOfBirth?.ToDateTime() ?? DateTime.Now;
                mLocation = Model.Model.Location ?? new();

                if (Model is UpdateStaffMemberModel<ProfessorRequestModel> professor)
                {
                    if (professor.StaffMember is not null && !professor.StaffMember.Websites.IsNullOrEmpty())
                        mWebsites = professor.StaffMember.Websites.ToList();
                    else
                        AddNew();
                }
            }
        }

        #endregion

        #region Private Methods

        private void AddNew()
        {
            mWebsites.Insert(0, new());
            StateHasChanged();
        }

        private async void Save()
        {
            mWebsites.RemoveAll(x => x == default);
            mWebsites.RemoveAll(x => x.Link is null);
            mWebsites.Where(x => x.Name.IsNullOrEmpty()).ForEach(x => x.Name = x.Link!.ToString());

            if (Model is not null)
            {
                Model.Model!.PhoneNumber = new PhoneNumber(mCountryCode, mPhoneNumber);
                Model.Model.Location = mLocation;
                Model.Model.DateOfBirth = mBirthDate?.ToDateOnly();

                if (!mPassword.IsNullOrEmpty() && mPassword == mConfirmPassword)
                    Model.Model.PasswordHash = mPassword.EncryptPassword();

                if (Model is UpdateStaffMemberModel<ProfessorRequestModel> professor)
                {
                    professor.StaffMember!.Websites = mWebsites;
                }

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
