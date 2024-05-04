using MeetBase;
using MeetBase.Web;

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;

using MudBlazor;

using Newtonsoft.Json;

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
        private string mCountryCode = "30";

        /// <summary>
        /// The phone number
        /// </summary>
        private string mPhoneNumber = string.Empty;

        /// <summary>
        /// The location
        /// </summary>
        private Location mLocation = new Location();

        /// <summary>
        /// The websites
        /// </summary>
        private List<Website> mWebsites = new();

        /// <summary>
        /// The current website
        /// </summary>
        private Website mWebsite = new();

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

        #region Protected Properties

        /// <summary>
        /// The JS runtime service
        /// </summary>
        [Inject]
        protected IJSRuntime JSRuntime { get; set; } = default!;

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

        /// <inheritdoc/>
        protected override void OnInitialized()
        {
            base.OnInitialized();

            mCountryCode = Model.Model!.PhoneNumber?.CountryCode ?? "30";
            mPhoneNumber = Model.Model.PhoneNumber?.Phone ?? string.Empty;
            mLocation = Model.Model.Location ?? new();
            mCategory = Model.Model.Category ?? DepartmentType.HealthSciences;
            if (!Model.Model.Websites.IsNullOrEmpty())
                mWebsites = Model.Model.Websites.ToList();
            else
                AddNew();
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

            Model.Model!.PhoneNumber = new PhoneNumber(mCountryCode, mPhoneNumber);
            Model.Model.Location = mLocation;
            Model.Model.Category = mCategory;
            Model.Model.Websites = mWebsites;

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
