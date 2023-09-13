using MeetBase;
using MeetBase.Blazor;

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.WebUtilities;

namespace MeetCore
{
    /// <summary>
    /// Extension methods for the <see cref="NavigationManager"/>
    /// </summary>
    public static class CoreNavigationManagerExtensions
    {
        #region Public Methods

        #region Secretary

        /// <summary>
        /// Navigates the secretary with the specified <paramref name="id"/> to the specified <paramref name="route"/>
        /// </summary>
        /// <param name="navigationManager">The navigation manager</param>
        /// <param name="id">The secretary id</param>
        /// <param name="route">The member route</param>
        /// <param name="value">The route value</param>
        /// <param name="queryParams">The optional query parameter values</param>
        public static void Secretary_NavigateTo(this NavigationManager navigationManager, string id, string route, string? value = null, Dictionary<string, string>? queryParams = null)
        {
            var completeRoute = Routes.SecretariesRoute + $"/{id}" + route;

            if (!value.IsNullOrEmpty())
                completeRoute += $"/{value}";

            if (!queryParams.IsNullOrEmpty())
            {
                completeRoute = QueryHelpers.AddQueryString(completeRoute, queryParams);
            }

            navigationManager.NavigateTo(completeRoute);
        }

        /// <summary>
        /// Navigates the secretary with the specified <paramref name="id"/> to their profile page
        /// <code>/secretaries/1/profile</code>
        /// </summary>
        /// <param name="navigationManager">The navigation manager</param>
        /// <param name="id">The secretary id</param>
        /// <param name="value">The route value</param>
        /// <param name="queryParams">The optional query parameter values</param>
        public static void Secretary_NavigateToProfilePage(this NavigationManager navigationManager, string id, string? value = null, Dictionary<string, string>? queryParams = null) 
            => navigationManager.Secretary_NavigateTo(id, Routes.ProfileRoute, value, queryParams);

        /// <summary>
        /// Navigates the secretary with the specified <paramref name="id"/> to the secretary page
        /// <code>/secretaries/1/secretary</code>
        /// </summary>
        /// <param name="navigationManager">The navigation manager</param>
        /// <param name="id">The secretary id</param>
        /// <param name="value">The route value</param>
        /// <param name="queryParams">The optional query parameter values</param>
        public static void Secretary_NavigateToSecretaryPage(this NavigationManager navigationManager, string id, string? value = null, Dictionary<string, string>? queryParams = null)
            => navigationManager.Secretary_NavigateTo(id, Routes.SecretaryRoute, value, queryParams);

        /// <summary>
        /// Navigates the secretary with the specified <paramref name="id"/> to the information page
        /// <code>/secretaries/1/information</code>
        /// </summary>
        /// <param name="navigationManager">The navigation manager</param>
        /// <param name="id">The secretary id</param>
        /// <param name="value">The route value</param>
        /// <param name="queryParams">The optional query parameter values</param>
        public static void Secretary_NavigateToInformationPage(this NavigationManager navigationManager, string id, string? value = null, Dictionary<string, string>? queryParams = null)
            => navigationManager.Secretary_NavigateTo(id, Routes.InformationRoute, value, queryParams);

        /// <summary>
        /// Navigates the secretary with the specified <paramref name="id"/> to the faculty page
        /// <code>/secretaries/1/faculty</code>
        /// </summary>
        /// <param name="navigationManager">The navigation manager</param>
        /// <param name="id">The secretary id</param>
        /// <param name="value">The route value</param>
        /// <param name="queryParams">The optional query parameter values</param>
        public static void Secretary_NavigateToFacultyPage(this NavigationManager navigationManager, string id, string? value = null, Dictionary<string, string>? queryParams = null)
            => navigationManager.Secretary_NavigateTo(id, Routes.FacultyRoute, value, queryParams);

        /// <summary>
        /// Navigates the secretary with the specified <paramref name="id"/> to the faculty page
        /// <code>/secretaries/1/layout</code>
        /// </summary>
        /// <param name="navigationManager">The navigation manager</param>
        /// <param name="id">The secretary id</param>
        /// <param name="value">The route value</param>
        /// <param name="queryParams">The optional query parameter values</param>
        public static void Secretary_NavigateToLayoutPage(this NavigationManager navigationManager, string id, string? value = null, Dictionary<string, string>? queryParams = null)
            => navigationManager.Secretary_NavigateTo(id, Routes.LayoutRoute, value, queryParams);

        /// <summary>
        /// Navigates the secretary with the specified <paramref name="id"/> to the faculty page
        /// <code>/secretaries/1/form</code>
        /// </summary>
        /// <param name="navigationManager">The navigation manager</param>
        /// <param name="id">The secretary id</param>
        /// <param name="value">The route value</param>
        /// <param name="queryParams">The optional query parameter values</param>
        public static void Secretary_NavigateToFormPage(this NavigationManager navigationManager, string id, string? value = null, Dictionary<string, string>? queryParams = null)
            => navigationManager.Secretary_NavigateTo(id, Routes.FormRoute, value, queryParams);

        /// <summary>
        /// Navigates the secretary with the specified <paramref name="id"/> to the faculty page
        /// <code>/secretaries/1/inbox</code>
        /// </summary>
        /// <param name="navigationManager">The navigation manager</param>
        /// <param name="id">The secretary id</param>
        /// <param name="value">The route value</param>
        /// <param name="queryParams">The optional query parameter values</param>
        public static void Secretary_NavigateToInboxPage(this NavigationManager navigationManager, string id, string? value = null, Dictionary<string, string>? queryParams = null)
            => navigationManager.Secretary_NavigateTo(id, Routes.InboxRoute, value, queryParams);

        #endregion

        #region Professor

        /// <summary>
        /// Navigates the professor with the specified <paramref name="id"/> to the specified <paramref name="route"/>
        /// </summary>
        /// <param name="navigationManager">The navigation manager</param>
        /// <param name="id">The secretary id</param>
        /// <param name="route">The member route</param>
        /// <param name="value">The route value</param>
        /// <param name="queryParams">The optional query parameter values</param>
        public static void Professor_NavigateTo(this NavigationManager navigationManager, string id, string route, string? value = null, Dictionary<string, string>? queryParams = null)
        {
            var completeRoute = Routes.ProfessorsRoute + $"/{id}" + route;

            if (!value.IsNullOrEmpty())
                completeRoute += $"/{value}";

            if (!queryParams.IsNullOrEmpty())
            {
                completeRoute = QueryHelpers.AddQueryString(completeRoute, queryParams);
            }

            navigationManager.NavigateTo(completeRoute);
        }

        /// <summary>
        /// Navigates the professor with the specified <paramref name="id"/> to their profile page
        /// <code>/professors/1/profile</code>
        /// </summary>
        /// <param name="navigationManager">The navigation manager</param>
        /// <param name="id">The professor id</param>
        /// <param name="value">The route value</param>
        /// <param name="queryParams">The optional query parameter values</param>
        public static void Professor_NavigateToProfilePage(this NavigationManager navigationManager, string id, string? value = null, Dictionary<string, string>? queryParams = null)
            => navigationManager.Professor_NavigateTo(id, Routes.ProfileRoute, value, queryParams);

        /// <summary>
        /// Navigates the professor with the specified <paramref name="id"/> to their schedule page
        /// <code>/professors/1/schedule</code>
        /// </summary>
        /// <param name="navigationManager">The navigation manager</param>
        /// <param name="id">The professor id</param>
        /// <param name="value">The route value</param>
        /// <param name="queryParams">The optional query parameter values</param>
        public static void Professor_NavigateToSchedulePage(this NavigationManager navigationManager, string id, string? value = null, Dictionary<string, string>? queryParams = null)
            => navigationManager.Professor_NavigateTo(id, Routes.ScheduleRoute, value, queryParams);

        /// <summary>
        /// Navigates the professor with the specified <paramref name="id"/> to their office page
        /// <code>/professors/1/office</code>
        /// </summary>
        /// <param name="navigationManager">The navigation manager</param>
        /// <param name="id">The professor id</param>
        /// <param name="value">The route value</param>
        /// <param name="queryParams">The optional query parameter values</param>
        public static void Professor_NavigateToOfficePage(this NavigationManager navigationManager, string id, string? value = null, Dictionary<string, string>? queryParams = null)
            => navigationManager.Professor_NavigateTo(id, Routes.OfficeRoute, value, queryParams);

        /// <summary>
        /// Navigates the professor with the specified <paramref name="id"/> to their active appointments page
        /// <code>/professors/1/active_appointments</code>
        /// </summary>
        /// <param name="navigationManager">The navigation manager</param>
        /// <param name="id">The professor id</param>
        /// <param name="value">The route value</param>
        /// <param name="queryParams">The optional query parameter values</param>
        public static void Professor_NavigateToActiveAppointmentsPage(this NavigationManager navigationManager, string id, string? value = null, Dictionary<string, string>? queryParams = null)
            => navigationManager.Professor_NavigateTo(id, Routes.ActiveAppointmentsRoute, value, queryParams);

        /// <summary>
        /// Navigates the professor with the specified <paramref name="id"/> to their appointments history page
        /// <code>/professors/1/appointments_history</code>
        /// </summary>
        /// <param name="navigationManager">The navigation manager</param>
        /// <param name="id">The professor id</param>
        /// <param name="value">The route value</param>
        /// <param name="queryParams">The optional query parameter values</param>
        public static void Professor_NavigateToAppointmentsHistoryPage(this NavigationManager navigationManager, string id, string? value = null, Dictionary<string, string>? queryParams = null)
            => navigationManager.Professor_NavigateTo(id, Routes.AppointmentsHistoryRoute, value, queryParams);

        /// <summary>
        /// Navigates the professor with the specified <paramref name="id"/> to their form page
        /// <code>/professors/1/form</code>
        /// </summary>
        /// <param name="navigationManager">The navigation manager</param>
        /// <param name="id">The professor id</param>
        /// <param name="value">The route value</param>
        /// <param name="queryParams">The optional query parameter values</param>
        public static void Professor_NavigateToFormPage(this NavigationManager navigationManager, string id, string? value = null, Dictionary<string, string>? queryParams = null)
            => navigationManager.Professor_NavigateTo(id, Routes.FormRoute, value, queryParams);

        /// <summary>
        /// Navigates the professor with the specified <paramref name="id"/> to their inbox page
        /// <code>/professors/1/inbox</code>
        /// </summary>
        /// <param name="navigationManager">The navigation manager</param>
        /// <param name="id">The professor id</param>
        /// <param name="value">The route value</param>
        /// <param name="queryParams">The optional query parameter values</param>
        public static void Professor_NavigateToInboxPage(this NavigationManager navigationManager, string id, string? value = null, Dictionary<string, string>? queryParams = null)
            => navigationManager.Professor_NavigateTo(id, Routes.InboxRoute, value, queryParams);

        #endregion

        #endregion
    }
}
