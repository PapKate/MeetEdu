using Microsoft.AspNetCore.Components;

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
        /// The navigation manager service
        /// </summary>
        [Inject]
        public NavigationManager? NavigationManager { get; set; }

        /// <summary>
        /// A flag indicating whether the staff member is a secretary
        /// </summary>
        [Parameter]
        public bool IsSecretary { get; set; }

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
                // Navigates to the secretary profile page
                NavigationManager.Secretary_NavigateToProfilePage("1");
                // Navigates to the professor profile page
            NavigationManager.Professor_NavigateToProfilePage("id");
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
                // Navigates to the secretary data form page
                NavigationManager.Secretary_NavigateToFormPage("1");
            // Navigates to the professor data form page
            NavigationManager.Professor_NavigateToFormPage("id");
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
                // Navigates to the secretary profile page
                NavigationManager.Secretary_NavigateToInboxPage("1");
            // Navigates to the professor profile page
            NavigationManager.Professor_NavigateToInboxPage("id");
        }

        #endregion
    }
}
