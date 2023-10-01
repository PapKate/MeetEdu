using MeetBase;

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

using MudBlazor;

namespace MeetCore
{
    public partial class UpdateDepartmentDialog
    {
        #region Private Members

        /// <summary>
        /// The photo input label
        /// </summary>
        private string mPhotoLabel = "Department logo";

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
        public UpdateDepartmentModel Model { get; set; } = new();

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

            mCountryCode = Model.PhoneNumber?.CountryCode ?? 30;
            mPhoneNumber = Model.PhoneNumber?.Phone ?? string.Empty;
            mLocation = Model.Location ?? new();
        }

        #endregion

        #region Private Methods

        private void Save()
        {
            Model.PhoneNumber = new PhoneNumber(mCountryCode, mPhoneNumber);
            Model.Location = mLocation;

            MudDialog.Close(DialogResult.Ok(Model));
        }

        private void Cancel()
        {
            MudDialog.Cancel();
        }

        private async void BrowserFileUploaded(IBrowserFile file)
        {
            var buffers = new byte[file.Size];
            await file.OpenReadStream().ReadAsync(buffers);
            var imageType = file.ContentType;
            var imgUrl = $"data:{imageType};base64,{Convert.ToBase64String(buffers)}";
            Model.ImageUrl = new Uri(imgUrl);
        }

        #endregion
    }
}
