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
        private const string BaseRoute = "api/appointMate";

        /// <summary>
        /// The management app route
        /// <code>api/appointMate/management</code>
        /// </summary>
        private const string ManagementRoute = BaseRoute + "/management";

        /// <summary>
        /// The app route
        /// <code>api/appointMate/app</code>
        /// </summary>
        private const string AppRoute = BaseRoute + "/app";

        #region App

        #region Companies
        
        /// <summary>
        /// The companies route
        /// <code>api/appointMate/app/companies</code>
        /// </summary>
        public const string CompaniesRoute = AppRoute + "/companies";

        /// <summary>
        /// The company route
        /// <code>api/appointMate/app/companies/1</code>
        /// </summary>
        public const string CompanyRoute = CompaniesRoute + "/{companyId}";

        /// <summary>
        /// The route for the company with the specified <paramref name="companyId"/>
        /// <code>api/appointMate/app/companies/<paramref name="companyId"/></code>
        /// </summary>
        public static string GetCompanyRoute(string companyId) => CompaniesRoute + $"/{companyId}";

        #endregion

        #region Services

        /// <summary>
        /// The services route
        /// <code>api/appointMate/app/services</code>
        /// </summary>
        public const string ServicesRoute = AppRoute + "/services";

        /// <summary>
        /// The service route
        /// <code>api/appointMate/app/services/1</code>
        /// </summary>
        public const string ServiceRoute = ServicesRoute + "/{serviceId}";

        /// <summary>
        /// The route for the service with the specified <paramref name="serviceId"/>
        /// <code>api/appointMate/app/services/<paramref name="serviceId"/></code>
        /// </summary>
        public static string GetServiceRoute(string serviceId) => ServicesRoute + $"/{serviceId}";

        #endregion

        #endregion

        #region Management
        #endregion
    }
}
