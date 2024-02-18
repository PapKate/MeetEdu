using MeetBase.Web;

using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace MeetCore
{
    public partial class AppointmentRuleDialog
    {
        #region Private Members

        private bool mHasRemoteOption = false;

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
        public AppointmentRuleRequestModel Model { get; set; } = default!;

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public AppointmentRuleDialog() : base()
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

            mHasRemoteOption = Model.HasRemoteOption ?? false;
        }

        #endregion

        #region Private Methods

        private async void SaveButton_OnClick()
        {
            Model.HasRemoteOption = mHasRemoteOption;
            await SaveOnClick.InvokeAsync(Model);
        }

        private void CancelButton_OnClick()
        {
            MudDialog.Cancel();
        }

        #endregion

        #region Public Events

        /// <summary>
        /// Fires when the reply button is clicked
        /// </summary>
        public EventCallback<AppointmentRuleRequestModel> SaveOnClick { get; set; }

        #endregion
    }
}
