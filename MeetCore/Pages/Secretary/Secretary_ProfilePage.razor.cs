using MeetBase.Web;

using Microsoft.AspNetCore.Components;

using MudBlazor;

namespace MeetCore
{
    /// <summary>
    /// The profile page
    /// </summary>
    public partial class Secretary_ProfilePage : BasePage
    {
        #region Private Properties

        /// <summary>
        /// A flag indicating whether the <see cref="MudDialog"/> is open or not
        /// </summary>
        private bool mIsDialogOpen;

        /// <summary>
        /// The <see cref="MudDialog"/> options
        /// </summary>
        private DialogOptions mDialogOptions = new() { FullWidth = true };

        #endregion

        #region Public Properties

        /// <summary>
        /// The secretary
        /// </summary>
        public SecretaryResponseModel Secretary => StateManager.Secretary!;

        /// <summary>
        /// The user
        /// </summary>
        public UserResponseModel User => StateManager.User!;

        #endregion

        #region Protected Properties

        /// <summary>
        /// The state management service
        /// </summary>
        [Inject]
        protected StateManagerCore StateManager { get; set; } = default!;

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public Secretary_ProfilePage() : base()
        {

        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Opens the dialog
        /// </summary>
        private void OpenDialog() => mIsDialogOpen = true;
        
        /// <summary>
        /// Closes the dialog
        /// </summary>
        private void CloseDialog() => mIsDialogOpen = false;

        
        #endregion
    }
}
