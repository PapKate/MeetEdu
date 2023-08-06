using System.Runtime.CompilerServices;

namespace AppointMate
{
    /// <summary>
    /// The AppontMate related API routes
    /// </summary>
    public static class AppointMateAPIRoutes
    {
        /// <summary>
        /// The app route
        /// <code>/appointMate/api/v1</code>
        /// </summary>
        private const string AppRoute = "/appointMate";

        /// <summary>
        /// The base route
        /// <code>/appointMate/api/v1</code>
        /// </summary>
        private const string BaseAPIRoute = AppRoute + "api/v1";

        #region Login

        /// <summary>
        /// The login route
        /// <code>/appointMate/api/v1/login</code>
        /// </summary>
        public const string LogInRoute = AppRoute + "/logIn";

        #endregion

        #region Register

        /// <summary>
        /// The register route
        /// <code>/appointMate/api/v1/register</code>
        /// </summary>
        public const string RegisterRoute = AppRoute + "/register";

        #endregion

        #region Companies

        /// <summary>
        /// The companies route
        /// <code>/appointMate/api/v1/companies</code>
        /// </summary>
        public const string CompaniesRoute = AppRoute + "/companies";

        /// <summary>
        /// The company route
        /// <code>/appointMate/api/v1/companies/1</code>
        /// </summary>
        public const string CompanyRoute = CompaniesRoute + "/{companyId}";

        /// <summary>
        /// The route for the company with the specified <paramref name="companyId"/>
        /// <code>/appointMate/api/v1/companies/<paramref name="companyId"/></code>
        /// </summary>
        public static string GetCompanyRoute(string companyId) => CompaniesRoute + $"/{companyId}";

        #region Services

        /// <summary>
        /// The company services route
        /// <code>/appointMate/api/v1/companies/1/services</code>
        /// </summary>
        public const string CompanyServicesRoute = CompanyRoute + "/services";

        /// <summary>
        /// The company service route
        /// <code>/appointMate/api/v1/companies/1/services/3</code>
        /// </summary>
        public const string CompanyServiceRoute = CompanyServicesRoute + "/{serviceId}";

        /// <summary>
        /// The route for the company service with the specified <paramref name="serviceId"/>
        /// <code>/appointMate/api/v1/companies/1/services/<paramref name="serviceId"/></code>
        /// </summary>
        public static string GetCompanyServiceRoute(string serviceId) => CompanyServicesRoute + $"/{serviceId}";

        #region Sessions

        /// <summary>
        /// The company services route
        /// <code>/appointMate/api/v1/companies/1/services/1/sessions</code>
        /// </summary>
        public const string CompanyServiceSessionsRoute = CompanyServiceRoute + "/services";

        /// <summary>
        /// The company service route
        /// <code>/appointMate/api/v1/companies/1/services/3/sessions/1</code>
        /// </summary>
        public const string CompanyServiceSessionRoute = CompanyServiceRoute + "/{sessionId}";

        /// <summary>
        /// The route for the company service with the specified <paramref name="sessionId"/>
        /// <code>/appointMate/api/v1/companies/1/services/1/sessions/<paramref name="sessionId"/></code>
        /// </summary>
        public static string GetCompanyServiceSessionRoute(string sessionId) => CompanyServicesRoute + $"/{sessionId}";

        #endregion

        #endregion

        #region Staff Members

        /// <summary>
        /// The company staff member route
        /// <code>/appointMate/api/v1/companies/1/staffMembers</code>
        /// </summary>
        public const string CompanyStaffMembersRoute = CompanyRoute + "/staffMembers";

        /// <summary>
        /// The company staff member route
        /// <code>/appointMate/api/v1/companies/1/staffMembers/3</code>
        /// </summary>
        public const string CompanyStaffMemberRoute = CompanyStaffMembersRoute + "/{staffMemberId}";

        /// <summary>
        /// The route for the company staff member with the specified <paramref name="staffMemberId"/>
        /// <code>/appointMate/api/v1/companies/1/staffMembers/<paramref name="staffMemberId"/></code>
        /// </summary>
        public static string GetCompanyStaffMemberRoute(string staffMemberId) => CompanyStaffMembersRoute + $"/{staffMemberId}";

        #endregion

        #region Contact

        /// <summary>
        /// The company contact messages route
        /// <code>/appointMate/api/v1/companies/1/contactMessages</code>
        /// </summary>
        public const string CompanyContactMessagesRoute = CompanyRoute + "/contactMessages";

        /// <summary>
        /// The company contact message route
        /// <code>/appointMate/api/v1/companies/1/contactMessages/3</code>
        /// </summary>
        public const string CompanyyContactMessageRoute = CompanyContactMessagesRoute + "/{contactMessageId}";

        /// <summary>
        /// The route for the company contact message with the specified <paramref name="contactMessageId"/>
        /// <code>/appointMate/api/v1/companies/1/contactMessages/<paramref name="contactMessageId"/></code>
        /// </summary>
        public static string GetCompanyyContactMessageRoute(string contactMessageId) => CompanyStaffMembersRoute + $"/{contactMessageId}";

        #endregion

        #endregion

        #region Services

        /// <summary>
        /// The services route
        /// <code>/appointMate/api/v1/services</code>
        /// </summary>
        public const string ServicesRoute = AppRoute + "/services";

        /// <summary>
        /// The service route
        /// <code>/appointMate/api/v1/services/name</code>
        /// </summary>
        public const string ServiceRoute = ServicesRoute + "/{serviceName}";

        /// <summary>
        /// The route for the service with the specified <paramref name="serviceName"/>
        /// <code>/appointMate/api/v1/services/<paramref name="serviceName"/></code>
        /// </summary>
        public static string GetServiceRoute(string serviceName) => ServicesRoute + $"/{serviceName}";

        #endregion

        #region Users

        /// <summary>
        /// The users route
        /// <code>/appointMate/api/v1/users</code>
        /// </summary>
        public const string UsersRoute = AppRoute + "/users";

        /// <summary>
        /// The customer route
        /// <code>/appointMate/api/v1/users/1</code>
        /// </summary>
        public const string UserRoute = UsersRoute + "/{userId}";

        /// <summary>
        /// The route for the customer with the specified <paramref name="userId"/>
        /// <code>/appointMate/api/v1/users/<paramref name="userId"/></code>
        /// </summary>
        public static string GetUserRoute(string userId) => UsersRoute + $"/{userId}";

        #region Notes

        /// <summary>
        /// The user notes route
        /// <code>/appointMate/api/v1/users/1/notes</code>
        /// </summary>
        public const string UserNotesRoute = UserRoute + "/notes";

        /// <summary>
        /// The user note route
        /// <code>/appointMate/api/v1/users/1/notes/3</code>
        /// </summary>
        public const string UserNoteRoute = UserNotesRoute + "/{noteId}";

        /// <summary>
        /// The route for the user note with the specified <paramref name="noteId"/>
        /// <code>/appointMate/api/v1/users/1/notes/<paramref name="noteId"/></code>
        /// </summary>
        public static string GetUserNoteRoute(string noteId) => UserNotesRoute + $"/{noteId}";

        #endregion

        #region Favorite Companies

        /// <summary>
        /// The user favorite company route
        /// <code>/appointMate/api/v1/users/1/favoriteCompanies</code>
        /// </summary>
        public const string UserFavoriteCompaniesRoute = UserRoute + "/favoriteCompanies";

        /// <summary>
        /// The user favorite company route
        /// <code>/appointMate/api/v1/users/1/favoriteCompanies/3</code>
        /// </summary>
        public const string UserFavoriteCompanyRoute = UserFavoriteCompaniesRoute + "/{companyId}";

        /// <summary>
        /// The route for the user favorite company with the specified <paramref name="companyId"/>
        /// <code>/appointMate/api/v1/users/1/favoriteCompanies/<paramref name="companyId"/></code>
        /// </summary>
        public static string GetUserFavoriteCompanyRoute(string companyId) => UserFavoriteCompaniesRoute + $"/{companyId}";

        #endregion

        #region Reviews

        /// <summary>
        /// The user reviews route
        /// <code>/appointMate/api/v1/users/1/reviews</code>
        /// </summary>
        public const string UserReviewsRoute = UserRoute + "/reviews";

        /// <summary>
        /// The user review route
        /// <code>/appointMate/api/v1/users/1/reviews/3</code>
        /// </summary>
        public const string UserReviewRoute = UserReviewsRoute + "/{reviewsId}";

        /// <summary>
        /// The route for the user review with the specified <paramref name="reviewId"/>
        /// <code>/appointMate/api/v1/users/1/reviews/<paramref name="reviewId"/></code>
        /// </summary>
        public static string GetUserReviewRoute(string reviewId) => UserReviewsRoute + $"/{reviewId}";

        #endregion

        #region Point Offset Logs

        /// <summary>
        /// The user point offset log route
        /// <code>/appointMate/api/v1/users/1/reviews</code>
        /// </summary>
        public const string UserPointOffsetLogsRoute = UserRoute + "/pointOffsetLogs";

        /// <summary>
        /// The user point offset log route
        /// <code>/appointMate/api/v1/users/1/pointOffsetLogs/3</code>
        /// </summary>
        public const string UserPointOffsetLogRoute = UserPointOffsetLogsRoute + "/{pointOffsetLogId}";

        /// <summary>
        /// The route for the user point offset log with the specified <paramref name="pointOffsetLogId"/>
        /// <code>/appointMate/api/v1/users/1/reviews/<paramref name="pointOffsetLogId"/></code>
        /// </summary>
        public static string GetUserPointOffsetLogRoute(string pointOffsetLogId) => UserPointOffsetLogsRoute + $"/{pointOffsetLogId}";

        #endregion

        #region User Services

        /// <summary>
        /// The services route
        /// <code>/appointMate/api/v1/users/1/services</code>
        /// </summary>
        public const string UserServicesRoute = UserRoute + "/services";

        /// <summary>
        /// The service route
        /// <code>/appointMate/api/v1/users/1/services/2</code>
        /// </summary>
        public const string UserServiceRoute = UserServicesRoute + "/{serviceId}";

        /// <summary>
        /// The route for the service with the specified <paramref name="serviceId"/>
        /// <code>/appointMate/api/v1/users/<paramref name="customerId"/>/services/<paramref name="serviceId"/></code>
        /// </summary>
        public static string GetUserServiceRoute(string customerId, string serviceId) => GetUserRoute(customerId) + $"/services/{serviceId}";

        #endregion

        #region User Sessions

        /// <summary>
        /// The user sessions route
        /// <code>/appointMate/api/v1/users/1/sessions</code>
        /// </summary>
        public const string UserSessionsRoute = UserRoute + "/sessions";

        /// <summary>
        /// The user session route
        /// <code>/appointMate/api/v1/users/1/sessions/2</code>
        /// </summary>
        public const string UserSessionRoute = UserSessionsRoute + "/{sessionId}";

        /// <summary>
        /// The route for the user session with the specified <paramref name="sessionId"/>
        /// <code>/appointMate/api/v1/users/<paramref name="userId"/>/services/<paramref name="sessionId"/></code>
        /// </summary>
        public static string GetUserSessionRoute(string userId, string sessionId) => GetUserRoute(userId) + $"/services/{sessionId}";

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
