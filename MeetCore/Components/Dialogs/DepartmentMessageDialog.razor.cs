using Microsoft.AspNetCore.Components;
using MeetBase.Web;

using MudBlazor;
using MeetBase;

namespace MeetCore
{
    /// <summary>
    /// The dialog for adding and editing a <see cref="DepartmentContactMessageResponseModel"/>
    /// </summary>
    public partial class DepartmentMessageDialog
    {
        #region Private Members

        /// <summary>
        /// A flag indicating whether the action buttons are visible or not
        /// </summary>
        private bool mAreActionButtonsVisible = true;

        /// <summary>
        /// A flag indicating whether the message has a reply or not
        /// </summary>
        private bool mHasReply = false;

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
        public DepartmentMesageModel Model { get; set; } = default!;

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public DepartmentMessageDialog() : base()
        {

        }

        #endregion

        #region Protected Methods

        /// <inheritdoc/>
        protected override void OnInitialized()
        {
            base.OnInitialized();

            mHasReply = !Model.Model!.Reply.IsNullOrEmpty();
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Saves the changes
        /// </summary>
        private void Save()
        {
            MudDialog.Close(DialogResult.Ok(Model));
        }

        /// <summary>
        /// Closes the dialog
        /// </summary>
        protected void Cancel()
        {
            MudDialog.Cancel();
        }

        #endregion
    }
}
