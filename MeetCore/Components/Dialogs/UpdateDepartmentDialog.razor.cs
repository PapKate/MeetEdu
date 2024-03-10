using MeetBase;
using MeetBase.Web;

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

using MudBlazor;

namespace MeetCore
{
    /// <summary>
    /// The dialog for updating a department
    /// </summary>
    public partial class UpdateDepartmentDialog
    {
        #region Private Members

        /// <summary>
        /// The photo input label
        /// </summary>
        private string mPhotoLabel = "Department logo";
        private IBrowserFile? mFile;
        private DepartmentType mCategory;
        /// <summary>
        /// The country code
        /// </summary>
        private int mCountryCode = 30;

        /// <summary>
        /// The phone number
        /// </summary>
        private string mPhoneNumber = string.Empty;

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
        public UpdateModel<DepartmentRequestModel> Model { get; set; } = default!;

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public UpdateDepartmentDialog() : base()
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

            mCountryCode = Model.Model!.PhoneNumber?.CountryCode ?? 30;
            mPhoneNumber = Model.Model.PhoneNumber?.Phone ?? string.Empty;
            mLocation = Model.Model.Location ?? new();
            mCategory = Model.Model.Category ?? DepartmentType.Medicine;
        }

        #endregion

        #region Private Methods

        private async void Save()
        {
            Model.Model!.PhoneNumber = new PhoneNumber(mCountryCode, mPhoneNumber);
            Model.Model.Location = mLocation;
            Model.Model.Category = mCategory;

            if (mFile is not null)
            {
                Model.File = await mFile.ToIFormFileAsync(mPhotoLabel);
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
