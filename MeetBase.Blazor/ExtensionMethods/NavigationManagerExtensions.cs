
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.WebUtilities;

namespace MeetBase.Blazor
{
    /// <summary>
    /// Extension methods for the <see cref="NavigationManager"/>
    /// </summary>
    public static class NavigationManagerExtensions
    {
        /// <summary>
        /// Navigates to the specified <paramref name="route"/>
        /// </summary>
        /// <param name="navigationManager">The navigation manager</param>
        /// <param name="route">The route</param>
        /// <param name="value">The route value</param>
        public static void NavigateTo(this NavigationManager navigationManager, string route, string? value = null)
        {
            if (!value.IsNullOrEmpty())
                // Call the navigate to method
                navigationManager.NavigateTo(route + $"/{value}");
            else
                navigationManager.NavigateTo(route);
        }

        /// <summary>
        /// Navigates to the specified <paramref name="route"/>
        /// </summary>
        /// <param name="navigationManager">The navigation manager</param>
        /// <param name="route">The member route</param>
        /// <param name="value">The route value</param>
        /// <param name="queryParams">The optional query parameter values</param>
        public static void NavigateTo(this NavigationManager navigationManager, string route, string? value, Dictionary<string, string>? queryParams = null)
        {
            if (!value.IsNullOrEmpty())
                route += $"/{value}";

            if(!queryParams.IsNullOrEmpty())
            {
                route = QueryHelpers.AddQueryString(route, queryParams);
            }

            navigationManager.NavigateTo(route);
        }

        /// <summary>
        /// Navigates to the specified <paramref name="route"/>
        /// </summary>
        /// <param name="navigationManager">The navigation manager</param>
        /// <param name="route">The member route</param>
        /// <param name="value">The route value</param>
        /// <param name="queryKey">The query parameter key</param>
        /// <param name="queryValue">The query parameter value</param>
        public static void NavigateTo(this NavigationManager navigationManager, string route, string? value, string? queryKey = null, string? queryValue = null)
        {
            if (!value.IsNullOrEmpty())
                route += $"/{value}";

            if (!queryKey.IsNullOrEmpty() && !queryValue.IsNullOrEmpty())
                route = QueryHelpers.AddQueryString(route, queryKey, queryValue);

            navigationManager.NavigateTo(route);
        }
    }
}
