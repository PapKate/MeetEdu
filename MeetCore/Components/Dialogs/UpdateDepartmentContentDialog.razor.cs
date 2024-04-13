using MeetBase.Web;
using MeetBase;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using MudBlazor;

namespace MeetCore
{
    public partial class UpdateDepartmentContentDialog
    {
        #region Private Members

        /// <summary>
        /// The index
        /// </summary>
        protected int mIndex = 0;

        /// <summary>
        /// The fields
        /// </summary>
        protected List<string> mFields = new();

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
        public UpdateDepartmentContentDialog() : base()
        {

        }

        #endregion

        #region Protected Methods

        /// <inheritdoc/>
        protected override void OnInitialized()
        {
            if (Model.Model is not null && !Model.Model.Fields.IsNullOrEmpty())
                mFields = Model.Model.Fields.ToList();
            else
                AddNew();
            base.OnInitialized();
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Sets the new field
        /// </summary>
        /// <param name="oldValue">The previous value</param>
        /// <param name="newValue">The new value</param>
        protected void SetText(string oldValue, string newValue)
        {
            var index = mFields.IndexOf(oldValue);
            mFields[index] = newValue;
        }

        /// <summary>
        /// Adds a new field to the list
        /// </summary>
        protected void AddNew()
        {
            mFields.Insert(0, string.Empty);
            StateHasChanged();
        }

        private void Save()
        {
            Model.Model!.Fields = mFields;

            MudDialog.Close(DialogResult.Ok(Model));
        }

        private void Cancel()
        {
            MudDialog.Cancel();
        }

        #endregion
    }
}
