using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.WebUtilities;

namespace MeetEdu
{
    /// <summary>
    /// Extension methods for the <see cref="NavigationManager"/>
    /// </summary>
    public static class EduNavigationManagerExtensions
    {
        /// <summary>
        /// Navigates to the home page
        /// <code>/</code>
        /// </summary>
        /// <param name="navigationManager">The navigation manager</param>
        public static void NavigateToHomePage(this NavigationManager navigationManager)
            => navigationManager.NavigateTo("/");

        #region Departments

        /// <summary>
        /// Navigates to the departments page
        /// </summary>
        /// <param name="navigationManager">The navigation manager</param>
        /// <param name="queryParams">The optional query parameter values</param>
        public static void NavigateToDepartmentsPage(this NavigationManager navigationManager, Dictionary<string, string?>? queryParams = null)
        {
            var completeRoute = Routes.DepartmentsRoute;

            if (!queryParams.IsNullOrEmpty())
            {
                completeRoute = QueryHelpers.AddQueryString(completeRoute, queryParams);
            }

            navigationManager.NavigateTo(completeRoute);
        }

        /// <summary>
        /// Navigates to the department page of the one with the specified <paramref name="id"/>
        /// </summary>
        /// <param name="navigationManager">The navigation manager</param>
        /// <param name="id">The department id</param>
        /// <param name="queryParams">The optional query parameter values</param>
        public static void NavigateToDepartmentPage(this NavigationManager navigationManager, string id, Dictionary<string, string?>? queryParams = null)
        {
            var completeRoute = Routes.DepartmentsRoute + $"/{id}";

            if (!queryParams.IsNullOrEmpty())
            {
                completeRoute = QueryHelpers.AddQueryString(completeRoute, queryParams);
            }

            navigationManager.NavigateTo(completeRoute);
        }


        #endregion

        #region Faculty

        /// <summary>
        /// Navigates to the faculty page
        /// </summary>
        /// <param name="navigationManager">The navigation manager</param>
        /// <param name="queryParams">The optional query parameter values</param>
        public static void NavigateToFacultyPage(this NavigationManager navigationManager, Dictionary<string, string?>? queryParams = null)
        {
            var completeRoute = Routes.FacultyRoute;

            if (!queryParams.IsNullOrEmpty())
            {
                completeRoute = QueryHelpers.AddQueryString(completeRoute, queryParams);
            }

            navigationManager.NavigateTo(completeRoute);
        }

        #endregion

        #region Professor

        /// <summary>
        /// Navigates to the professor page
        /// </summary>
        /// <param name="navigationManager">The navigation manager</param>
        /// <param name="id">The professor id</param>
        /// <param name="queryParams">The optional query parameter values</param>
        public static void NavigateToprofessorPage(this NavigationManager navigationManager, string id, Dictionary<string, string?>? queryParams = null)
        {
            var completeRoute = Routes.ProfessorsRoute + $"/{id}";

            if (!queryParams.IsNullOrEmpty())
            {
                completeRoute = QueryHelpers.AddQueryString(completeRoute, queryParams);
            }

            navigationManager.NavigateTo(completeRoute);
        }

        #endregion
    }
}
