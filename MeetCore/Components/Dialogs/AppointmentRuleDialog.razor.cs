using MeetBase.Web;

using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace MeetCore
{
    /// <summary>
    /// The dialog for creating and editing an appointment rule
    /// </summary>
    public partial class AppointmentRuleDialog
    {
        #region Private Members

        private bool mHasRemoteOption = false;

        private DateTime? mDateFrom;

        private DateTime? mDateTo;

        private List<int>? mStartMinutes;

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
            mDateFrom = Model.DateFrom.DateTime;
            mDateTo = Model.DateTo.DateTime;
            mStartMinutes = Model.StartMinutes?.ToList();
        }

        #endregion

        #region Private Methods

        private void SaveButton_OnClick()
        {
            if (mDateTo is null || mDateFrom is null)
            {
                return;
            }

            Model.DateFrom = new DateTimeOffset(mDateFrom.Value);
            Model.DateTo = new DateTimeOffset(mDateTo.Value);
            Model.HasRemoteOption = mHasRemoteOption;
            MudDialog.Close(DialogResult.Ok(Model));
        }

        private void CancelButton_OnClick()
        {
            MudDialog.Cancel();
        }

        #endregion
    }
}
