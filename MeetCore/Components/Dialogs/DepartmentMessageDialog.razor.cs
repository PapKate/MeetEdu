using Microsoft.AspNetCore.Components;

using MudBlazor;

namespace MeetCore
{
    public partial class DepartmentMessageDialog
    {
        #region Private Members

        /// <summary>
        /// A flag indicating whether the reply button is visible
        /// </summary>
        private bool mIsReplyButtonVisible = true;

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

        #region Private Methods

        private async void SendReplyButton_OnClick()
        {
            await ReplyOnClick.InvokeAsync();
        }

        #endregion

        #region Public Events

        /// <summary>
        /// Fires when the reply button is clicked
        /// </summary>
        public EventCallback ReplyOnClick { get; set; }

        #endregion
    }
}
