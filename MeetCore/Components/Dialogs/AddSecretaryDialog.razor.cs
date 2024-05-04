using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace MeetCore
{
    /// <summary>
    /// The dialog for adding a secretary
    /// </summary>
    public partial class AddSecretaryDialog
    {
        #region Private Members

        /// <summary>
        /// The country code
        /// </summary>
        private string mCountryCode = "30";

        /// <summary>
        /// The phone number
        /// </summary>
        private string mPhoneNumber = string.Empty;

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
        public AddSecretaryModel Model { get; set; } = new();

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public AddSecretaryDialog() : base()
        {

        }

        #endregion

        #region Private Methods

        private void Save()
        {
            Model.PhoneNumber = new(mCountryCode, mPhoneNumber);
            MudDialog.Close(DialogResult.Ok(Model));
        }

        private void Cancel()
        {
            MudDialog.Cancel();
        }

        #endregion
    }
}
