namespace AppointMate
{
    /// <summary>
    /// The AppontMate related API routes
    /// </summary>
    public static class AppointMateAPIRoutes
    {
        /// <summary>
        /// The base route
        /// <code>api/v1</code>
        /// </summary>
        private const string BaseRoute = "api/v1";

        /// <summary>
        /// The app route
        /// <code>api/v1/appointMate</code>
        /// </summary>
        private const string AppRoute = BaseRoute + "/appointMate";

        #region Login

        /// <summary>
        /// The login route
        /// <code>api/v1/appointMate/login</code>
        /// </summary>
        public const string LogInRoute = AppRoute + "/logIn";

        #endregion

        #region Register

        /// <summary>
        /// The register route
        /// <code>api/v1/appointMate/register</code>
        /// </summary>
        public const string RegisterRoute = AppRoute + "/register";

        #endregion

        #region Companies

        /// <summary>
        /// The companies route
        /// <code>api/v1/appointMate/companies</code>
        /// </summary>
        public const string CompaniesRoute = AppRoute + "/companies";

        /// <summary>
        /// The company route
        /// <code>api/v1/appointMate/companies/1</code>
        /// </summary>
        public const string CompanyRoute = CompaniesRoute + "/{companyId}";

        /// <summary>
        /// The route for the company with the specified <paramref name="companyId"/>
        /// <code>api/v1/appointMate/companies/<paramref name="companyId"/></code>
        /// </summary>
        public static string GetCompanyRoute(string companyId) => CompaniesRoute + $"/{companyId}";

        #region Services

        /// <summary>
        /// The company services route
        /// <code>api/v1/appointMate/companies/1/services</code>
        /// </summary>
        public const string CompanyServicesRoute = CompanyRoute + "/services";

        /// <summary>
        /// The company service route
        /// <code>api/v1/appointMate/companies/1/services/3</code>
        /// </summary>
        public const string CompanyServiceRoute = CompanyServicesRoute + "/{serviceId}";

        /// <summary>
        /// The route for the company service with the specified <paramref name="serviceId"/>
        /// <code>api/v1/appointMate/companies/1/services/<paramref name="serviceId"/></code>
        /// </summary>
        public static string GetCompanyServiceRoute(string serviceId) => CompanyServicesRoute + $"/{serviceId}";

        #region Sessions

        /// <summary>
        /// The company services route
        /// <code>api/v1/appointMate/companies/1/services/1/sessions</code>
        /// </summary>
        public const string CompanyServiceSessionsRoute = CompanyServiceRoute + "/services";

        /// <summary>
        /// The company service route
        /// <code>api/v1/appointMate/companies/1/services/3/sessions/1</code>
        /// </summary>
        public const string CompanyServiceSessionRoute = CompanyServiceRoute + "/{sessionId}";

        /// <summary>
        /// The route for the company service with the specified <paramref name="sessionId"/>
        /// <code>api/v1/appointMate/companies/1/services/1/sessions/<paramref name="sessionId"/></code>
        /// </summary>
        public static string GetCompanyServiceSessionRoute(string sessionId) => CompanyServicesRoute + $"/{sessionId}";

        #endregion

        #endregion

        #region Staff Members

        /// <summary>
        /// The company staff member route
        /// <code>api/v1/appointMate/companies/1/staffMembers</code>
        /// </summary>
        public const string CompanyStaffMembersRoute = CompanyRoute + "/staffMembers";

        /// <summary>
        /// The company staff member route
        /// <code>api/v1/appointMate/companies/1/staffMembers/3</code>
        /// </summary>
        public const string CompanyStaffMemberRoute = CompanyStaffMembersRoute + "/{staffMemberId}";

        /// <summary>
        /// The route for the company staff member with the specified <paramref name="staffMemberId"/>
        /// <code>api/v1/appointMate/companies/1/staffMembers/<paramref name="staffMemberId"/></code>
        /// </summary>
        public static string GetCompanyStaffMemberRoute(string staffMemberId) => CompanyStaffMembersRoute + $"/{staffMemberId}";

        #endregion

        #region Contact

        /// <summary>
        /// The company contact messages route
        /// <code>api/v1/appointMate/companies/1/contactMessages</code>
        /// </summary>
        public const string CompanyContactMessagesRoute = CompanyRoute + "/contactMessages";

        /// <summary>
        /// The company contact message route
        /// <code>api/v1/appointMate/companies/1/contactMessages/3</code>
        /// </summary>
        public const string CompanyyContactMessageRoute = CompanyContactMessagesRoute + "/{contactMessageId}";

        /// <summary>
        /// The route for the company contact message with the specified <paramref name="contactMessageId"/>
        /// <code>api/v1/appointMate/companies/1/contactMessages/<paramref name="contactMessageId"/></code>
        /// </summary>
        public static string GetCompanyyContactMessageRoute(string contactMessageId) => CompanyStaffMembersRoute + $"/{contactMessageId}";

        #endregion

        #region Customers

        /// <summary>
        /// The customers route
        /// <code>api/v1/appointMate/companies/1/customers</code>
        /// </summary>
        public const string CompanyCustomersRoute = CompanyRoute + "/customers";

        /// <summary>
        /// The customer route
        /// <code>api/v1/appointMate/companies/1/customers/1</code>
        /// </summary>
        public const string CompanyCustomerRoute = CompanyCustomersRoute + "/{customerId}";

        /// <summary>
        /// The route for the customer with the specified <paramref name="customerId"/>
        /// <code>api/v1/appointMate/companies/1/customers/<paramref name="customerId"/></code>
        /// </summary>
        public static string GetCompanyCustomerRoute(string customerId) => CompanyCustomersRoute + $"/{customerId}";

        #endregion

        #endregion

        #region Services

        /// <summary>
        /// The services route
        /// <code>api/v1/appointMate/services</code>
        /// </summary>
        public const string ServicesRoute = AppRoute + "/services";

        /// <summary>
        /// The service route
        /// <code>api/v1/appointMate/services/1</code>
        /// </summary>
        public const string ServiceRoute = ServicesRoute + "/{serviceId}";

        /// <summary>
        /// The route for the service with the specified <paramref name="customerId"/>
        /// <code>api/v1/appointMate/services/<paramref name="customerId"/></code>
        /// </summary>
        public static string GetServiceRoute(string customerId) => ServicesRoute + $"/{customerId}";

        #endregion

        #region Users

        /// <summary>
        /// The users route
        /// <code>api/v1/appointMate/users</code>
        /// </summary>
        public const string CustomersRoute = AppRoute + "/customers";

        /// <summary>
        /// The customer route
        /// <code>api/v1/appointMate/users/1</code>
        /// </summary>
        public const string CustomerRoute = CustomersRoute + "/{customerId}";

        /// <summary>
        /// The route for the customer with the specified <paramref name="customerId"/>
        /// <code>api/v1/appointMate/users/<paramref name="customerId"/></code>
        /// </summary>
        public static string GetCustomerRoute(string customerId) => CustomersRoute + $"/{customerId}";

        #region Notes

        /// <summary>
        /// The user notes route
        /// <code>api/v1/appointMate/users/1/notes</code>
        /// </summary>
        public const string UserNotesRoute = CustomerRoute + "/notes";

        /// <summary>
        /// The user note route
        /// <code>api/v1/appointMate/users/1/notes/3</code>
        /// </summary>
        public const string UserNoteRoute = UserNotesRoute + "/{noteId}";

        /// <summary>
        /// The route for the user note with the specified <paramref name="noteId"/>
        /// <code>api/v1/appointMate/users/1/notes/<paramref name="noteId"/></code>
        /// </summary>
        public static string GetUserNoteRoute(string noteId) => UserNotesRoute + $"/{noteId}";

        #endregion

        #region Favorite Companies

        /// <summary>
        /// The user favorite company route
        /// <code>api/v1/appointMate/users/1/favoriteCompanies</code>
        /// </summary>
        public const string UserFavoriteCompaniesRoute = CustomerRoute + "/favoriteCompanies";

        /// <summary>
        /// The user favorite company route
        /// <code>api/v1/appointMate/users/1/favoriteCompanies/3</code>
        /// </summary>
        public const string UserFavoriteCompanyRoute = UserFavoriteCompaniesRoute + "/{companyId}";

        /// <summary>
        /// The route for the user favorite company with the specified <paramref name="companyId"/>
        /// <code>api/v1/appointMate/users/1/favoriteCompanies/<paramref name="companyId"/></code>
        /// </summary>
        public static string GetUserFavoriteCompanyRoute(string companyId) => UserFavoriteCompaniesRoute + $"/{companyId}";

        #endregion

        #region Reviews

        /// <summary>
        /// The user reviwes route
        /// <code>api/v1/appointMate/users/1/reviews</code>
        /// </summary>
        public const string UserReviewsRoute = CustomerRoute + "/reviews";

        /// <summary>
        /// The user review route
        /// <code>api/v1/appointMate/users/1/reviews/3</code>
        /// </summary>
        public const string UserReviewRoute = UserReviewsRoute + "/{reviewsId}";

        /// <summary>
        /// The route for the user review with the specified <paramref name="reviewId"/>
        /// <code>api/v1/appointMate/users/1/reviews/<paramref name="reviewId"/></code>
        /// </summary>
        public static string GetUserReviewRoute(string reviewId) => UserReviewsRoute + $"/{reviewId}";

        #endregion

        #region Point Offset Logs

        /// <summary>
        /// The user point offset log route
        /// <code>api/v1/appointMate/users/1/reviews</code>
        /// </summary>
        public const string UserPointOffsetLogsRoute = CustomerRoute + "/pointOffsetLogs";

        /// <summary>
        /// The user point offset log route
        /// <code>api/v1/appointMate/users/1/pointOffsetLogs/3</code>
        /// </summary>
        public const string UserPointOffsetLogRoute = UserPointOffsetLogsRoute + "/{pointOffsetLogId}";

        /// <summary>
        /// The route for the user point offset log with the specified <paramref name="pointOffsetLogId"/>
        /// <code>api/v1/appointMate/users/1/reviews/<paramref name="pointOffsetLogId"/></code>
        /// </summary>
        public static string GetUserPointOffsetLogRoute(string pointOffsetLogId) => UserPointOffsetLogsRoute + $"/{pointOffsetLogId}";

        #endregion

        #endregion
    }

    /// <summary>
    /// The AppontMate related API routes
    /// </summary>
    public static class CoreMateAPIRoutes
    {
        /// <summary>
        /// The base route
        /// <code>api/v1</code>
        /// </summary>
        private const string BaseRoute = "api/v1";

        /// <summary>
        /// The management app route
        /// <code>api/v1/coreMate</code>
        /// </summary>
        private const string ManagementRoute = BaseRoute + "/coreMate";

        #region Staff Member

        /// <summary>
        /// The staff member route
        /// <code>api/v1/coreMate/staffMembers</code>
        /// </summary>
        public const string StaffMembersRoute = ManagementRoute + "/staffMembers";

        /// <summary>
        /// The staff member route
        /// <code>api/v1/coreMate/staffMembers/3</code>
        /// </summary>
        public const string StaffMemberRoute = StaffMembersRoute + "/{staffMemberId}";

        /// <summary>
        /// The route for the staff member with the specified <paramref name="staffMemberId"/>
        /// <code>api/v1/coreMate/companies/1/staffMembers/<paramref name="staffMemberId"/></code>
        /// </summary>
        public static string GetStaffMemberRoute(string staffMemberId) => StaffMembersRoute + $"/{staffMemberId}";

        #region Companies

        /// <summary>
        /// The staff member companies route
        /// <code>api/v1/coreMate/staffMembers/1/companies</code>
        /// </summary>
        public const string StaffMemberCompaniesRoute = StaffMemberRoute + "/companies";

        /// <summary>
        /// The staff member company route
        /// <code>api/v1/coreMate/staffMembers/3/companies/2</code>
        /// </summary>
        public const string StaffMemberCompanyRoute = StaffMemberCompaniesRoute + "/{companyId}";

        /// <summary>
        /// The route for the staff member company with the specified <paramref name="companyId"/>
        /// <code>api/v1/coreMate/staffMembers/1/companies/<paramref name="companyId"/></code>
        /// </summary>
        public static string GetStaffMemberCompanyRoute(string companyId) => StaffMemberCompaniesRoute + $"/{companyId}";

        #region Contact

        #endregion

        #region Services

        #endregion

        #endregion

        #endregion
    }
}
