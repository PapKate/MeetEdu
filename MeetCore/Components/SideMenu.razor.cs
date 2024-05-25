using MeetBase.Web;

using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

using System.Net;

namespace MeetCore
{
    /// <summary>
    /// The side menu component
    /// </summary>
    public partial class SideMenu
    {
        #region Private Members

        #endregion

        #region Public Properties

        /// <summary>
        /// The secretary id
        /// </summary>
        public string SecretaryId => StateManager!.Secretary?.Id ?? string.Empty;
        
        /// <summary>
        /// The professor id
        /// </summary>
        public string ProfessorId => StateManager!.Professor?.Id ?? string.Empty;

        /// <summary>
        /// A flag indicating whether the staff member is a secretary
        /// </summary>
        [Parameter]
        public bool IsSecretary { get; set; } = false;

        #endregion

        #region Protected Properties

        /// <summary>
        /// The navigation manager service
        /// </summary>
        [Inject]
        protected NavigationManager? NavigationManager { get; set; }

        /// <summary>
        /// The state management service
        /// </summary>
        [Inject]
        protected StateManagerCore StateManager { get; set; } = default!;

        /// <summary>
        /// 
        /// </summary>
        [Inject]
        protected IJSRuntime JSRuntime { get; set; } = default!;

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public SideMenu() : base()
        {

        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Navigates the connected staff member to their profile page
        /// </summary>
        public void NavigateToProfilePage()
        {
            // If no navigation manager is found...
            if (NavigationManager is null)
                // Returns
                return;

            // If the connected staff member is a secretary...
            if (IsSecretary)
            {
                // Navigates to the secretary profile page
                NavigationManager.Secretary_NavigateToProfilePage(SecretaryId);
                // Returns
                return;
            }
            
            // Navigates to the professor profile page
            NavigationManager.Professor_NavigateToProfilePage(ProfessorId);
        }

        /// <summary>
        /// Navigates the connected staff member to their data form page
        /// </summary>
        public void NavigateToFormPage()
        {
            // If no navigation manager is found...
            if (NavigationManager is null)
                // Returns
                return;

            // If the connected staff member is a secretary...
            if (IsSecretary)
            {
                // Navigates to the secretary data form page
                NavigationManager.Secretary_NavigateToFormPage(SecretaryId);
                // Returns
                return;
            }
            // Navigates to the professor data form page
            NavigationManager.Professor_NavigateToFormPage(ProfessorId);
        }

        /// <summary>
        /// Navigates the connected staff member to their inbox page
        /// </summary>
        public void NavigateToInboxPage()
        {
            // If no navigation manager is found...
            if (NavigationManager is null)
                // Returns
                return;

            // If the connected staff member is a secretary...
            if (IsSecretary)
            {
                // Navigates to the secretary profile page
                NavigationManager.Secretary_NavigateToInboxPage(SecretaryId);
                // Returns
                return;
            }
        }

        /// <summary>
        /// Navigates the connected professor to their form rules page
        /// </summary>
        public void NavigateToFormRulesPage()
        {
            // If no navigation manager is found...
            if (NavigationManager is null)
                // Returns
                return;

            // Navigates to the professor form rules page
            NavigationManager.Professor_NavigateToFormRulesPage(ProfessorId);
            // Returns
            return;
        }

        /// <summary>
        /// Logs out the connected staff member and navigates to the login page
        /// </summary>
        public async void NavigateToLoginPage()
        {
            // If no navigation manager is found...
            if (NavigationManager is null)
                // Returns
                return;

            // Navigates to the login page
            NavigationManager.NavigateToLoginPage();
        }

        #endregion
    }
}
