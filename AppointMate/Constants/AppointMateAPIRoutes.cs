namespace AppointMate
{
    /// <summary>
    /// The AppontMate related API routes
    /// </summary>
    public static class AppointMateAPIRoutes
    {
        /// <summary>
        /// The base route
        /// <code>api/appointMate</code>
        /// </summary>
        private const string ControllerRoute = "api/appointMate";

        #region Companies

        /// <summary>
        /// The companies route
        /// <code>api/appointMate/companies</code>
        /// </summary>
        public const string CompaniesRoute = ControllerRoute + "/companies";

        /// <summary>
        /// The company route
        /// <code>api/appointMate/companies/1</code>
        /// </summary>
        public const string CompanyRoute = CompaniesRoute + "/{companyId}";

        /// <summary>
        /// The company route for the company with the specified <paramref name="companyId"/>
        /// <code>api/appointMate/companies/<paramref name="companyId"/></code>
        /// </summary>
        public static string GetCompanyRoute(string companyId) => CompaniesRoute + $"/{companyId}";

        #endregion
    }
}
