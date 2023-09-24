namespace MeetBase.Web
{
    /// <summary>
    /// The MeetCore related API routes
    /// </summary>
    public static class MeetCoreAPIRoutes
    {
        /// <summary>
        /// The app route
        /// <code>/appointMate/api</code>
        /// </summary>
        private const string APIRoute = "/appointMate/api";

        /// <summary>
        /// The app route
        /// <code>/appointMate/api/v1</code>
        /// </summary>
        private const string VersionRoute = APIRoute + "/v1";

        /// <summary>
        /// The base route
        /// <code>/appointMate/api/v1/meetCore</code>
        /// </summary>
        private const string BaseRoute = VersionRoute + "/meetCore";

        #region Login

        /// <summary>
        /// The login route
        /// <code>/appointMate/api/v1/meetCore/login</code>
        /// </summary>
        public const string LogInRoute = BaseRoute + "/login";

        #endregion

        #region Register

        /// <summary>
        /// The register route
        /// <code>/appointMate/api/v1/meetCore/register</code>
        /// </summary>
        public const string RegisterRoute = BaseRoute + "/register";

        #endregion

        #region Reset

        /// <summary>
        /// The register route
        /// <code>/appointMate/api/v1/meetCore/reset</code>
        /// </summary>
        public const string ResetRoute = BaseRoute + "/reset";

        #endregion

        #region Users

        /// <summary>
        /// The users route
        /// <code>/appointMate/api/v1/meetCore/users</code>
        /// </summary>
        public const string UsersRoute = BaseRoute + "/users";

        /// <summary>
        /// The user route
        /// <code>/appointMate/api/v1/meetCore/users/3</code>
        /// </summary>
        public const string UserRoute = UsersRoute + "/{userId}";

        /// <summary>
        /// Gets the route for the user with the specified <paramref name="userId"/>
        /// <code>/appointMate/api/v1/meetCore/users/4</code>
        /// </summary>
        /// <param name="userId">The id</param>
        /// <returns></returns>
        public static string GetUserRoute(string userId) => UsersRoute + $"/{userId}";

        #endregion

        #region Universities

        /// <summary>
        /// The universities route
        /// <code>/appointMate/api/v1/meetCore/universities</code>
        /// </summary>
        public const string UniversitiesRoute = BaseRoute + "/universities";

        /// <summary>
        /// The university route
        /// <code>/appointMate/api/v1/meetCore/universities/5</code>
        /// </summary>
        public const string UniversityRoute = UniversitiesRoute + "/{universityId}";

        /// <summary>
        /// Gets the route for the university with the specified <paramref name="universityId"/>
        /// <code>/appointMate/api/v1/meetCore/universities/5</code>
        /// </summary>
        /// <param name="universityId">The id</param>
        /// <returns></returns>
        public static string GetUniversityRoute(string universityId) => UniversitiesRoute + $"/{universityId}";

        #endregion

        #region University Labels

        /// <summary>
        /// The university labels route
        /// <code>/appointMate/api/v1/meetCore/universityLabels</code>
        /// </summary>
        public const string UniversityLabelsRoute = BaseRoute + "/universityLabels";

        /// <summary>
        /// The university label route
        /// <code>/appointMate/api/v1/meetCore/universityLabels/5</code>
        /// </summary>
        public const string UniversityLabelRoute = UniversityLabelsRoute + "/{universityLabelId}";

        /// <summary>
        /// Gets the route for the university label with the specified <paramref name="universityLabelId"/>
        /// <code>/appointMate/api/v1/meetCore/universityLabels/5</code>
        /// </summary>
        /// <param name="universityLabelId">The id</param>
        /// <returns></returns>
        public static string GetUniversityLabelRoute(string universityLabelId) => UniversityLabelsRoute + $"/{universityLabelId}";

        #endregion

        #region Departments

        /// <summary>
        /// The departments route
        /// <code>/appointMate/api/v1/meetCore/departments</code>
        /// </summary>
        public const string DepartmentsRoute = BaseRoute + "/departments";

        /// <summary>
        /// The department route
        /// <code>/appointMate/api/v1/meetCore/departments/3</code>
        /// </summary>
        public const string DepartmentRoute = DepartmentsRoute + "/{departmentId}";

        /// <summary>
        /// Gets the department route for the one with the specified <paramref name="departmentId"/>
        /// <code>/appointMate/api/v1/meetCore/departments/3</code>
        /// </summary>
        /// <param name="departmentId">The id</param>
        /// <returns></returns>
        public static string GetDepartmentRoute(string departmentId) => DepartmentsRoute + $"/{departmentId}";

        #endregion

        #region Department Layouts

        /// <summary>
        /// The department layouts route
        /// <code>/appointMate/api/v1/meetCore/departmentLayouts</code>
        /// </summary>
        public const string DepartmentLayoutsRoute = BaseRoute + "/departmentLayouts";

        /// <summary>
        /// The department layout route
        /// <code>/appointMate/api/v1/meetCore/departmentLayouts/3</code>
        /// </summary>
        public const string DepartmentLayoutRoute = DepartmentLayoutsRoute + "/{departmentLayoutId}";

        /// <summary>
        /// Gets the department layout route for the one with the specified <paramref name="departmentLayoutId"/>
        /// <code>/appointMate/api/v1/meetCore/departmentLayouts/3</code>
        /// </summary>
        /// <param name="departmentLayoutId">The id</param>
        /// <returns></returns>
        public static string GetDepartmentLayoutRoute(string departmentLayoutId) => DepartmentLayoutsRoute + $"/{departmentLayoutId}";

        #endregion

        #region Department Labels

        /// <summary>
        /// The department labels route
        /// <code>/appointMate/api/v1/meetCore/departmentLabels</code>
        /// </summary>
        public const string DepartmentLabelsRoute = BaseRoute + "/departmentLabels";

        /// <summary>
        /// The department label route
        /// <code>/appointMate/api/v1/meetCore/departmentLabels/5</code>
        /// </summary>
        public const string DepartmentLabelRoute = DepartmentLabelsRoute + "/{departmentLabelId}";

        /// <summary>
        /// Gets the route for the department label with the specified <paramref name="departmentLabelId"/>
        /// <code>/appointMate/api/v1/meetCore/departmentLabels/5</code>
        /// </summary>
        /// <param name="departmentLabelId">The id</param>
        /// <returns></returns>
        public static string GetDepartmentLabelRoute(string departmentLabelId) => DepartmentLabelsRoute + $"/{departmentLabelId}";

        #endregion

        #region Department Contact Messages

        /// <summary>
        /// The department contact messages route
        /// <code>/appointMate/api/v1/meetCore/departmentContactMessages</code>
        /// </summary>
        public const string DepartmentContactMessagesRoute = BaseRoute + "/departmentContactMessages";

        /// <summary>
        /// The department contact message route
        /// <code>/appointMate/api/v1/meetCore/departmentContactMessages/3</code>
        /// </summary>
        public const string DepartmentContactMessageRoute = DepartmentContactMessagesRoute + "/{departmentContactMessageId}";

        /// <summary>
        /// Gets the route for the department contact message with the specified <paramref name="departmentContactMessageId"/>
        /// <code>/appointMate/api/v1/meetCore/departmentContactMessages/2</code>
        /// </summary>
        /// <param name="departmentContactMessageId">The id</param>
        /// <returns></returns>
        public static string GetDepartmentContactMessageRoute(string departmentContactMessageId) => DepartmentContactMessagesRoute + $"/{departmentContactMessageId}";

        #endregion

        #region Secretaries

        /// <summary>
        /// The secretaries route
        /// <code>/appointMate/api/v1/meetCore/secretaries</code>
        /// </summary>
        public const string SecretariesRoute = BaseRoute + "/secretaries";

        /// <summary>
        /// The secretary route
        /// <code>/appointMate/api/v1/meetCore/secretaries/3</code>
        /// </summary>
        public const string SecretaryRoute = SecretariesRoute + "/{secretaryId}";

        /// <summary>
        /// Gets the route for the secretary with the specified <paramref name="secretaryId"/>
        /// <code>/appointMate/api/v1/meetCore/secretaries/4</code>
        /// </summary>
        /// <param name="secretaryId">The id</param>
        /// <returns></returns>
        public static string GetSecretaryRoute(string secretaryId) => SecretariesRoute + $"/{secretaryId}";

        #endregion

        #region Professors
        /// <summary>
        /// The professors route
        /// <code>/appointMate/api/v1/meetCore/professors</code>
        /// </summary>
        public const string ProfessorsRoute = BaseRoute + "/professors";

        /// <summary>
        /// The professor route
        /// <code>/appointMate/api/v1/meetCore/professors/3</code>
        /// </summary>
        public const string ProfessorRoute = ProfessorsRoute + "/{professorId}";

        /// <summary>
        /// Gets the professor route for the one with the specified <paramref name="professorId"/>
        /// <code>/appointMate/api/v1/meetCore/professors/3</code>
        /// </summary>
        /// <param name="professorId">The id</param>
        /// <returns></returns>
        public static string GetProfessorRoute(string professorId) => ProfessorsRoute + $"/{professorId}";

        #endregion

        #region Appointment Rules

        /// <summary>
        /// The appointment rules route
        /// <code>/appointMate/api/v1/meetCore/appointmentRules</code>
        /// </summary>
        public const string AppointmentRulesRoute = BaseRoute + "/appointmentRules";

        /// <summary>
        /// The appointment rule route
        /// <code>/appointMate/api/v1/meetCore/appointmentRules/3</code>
        /// </summary>
        public const string AppointmentRuleRoute = AppointmentRulesRoute + "/{appointmentRuleId}";

        /// <summary>
        /// Gets the route for the appointment with the specified <paramref name="appointmentRuleId"/>
        /// <code>/appointMate/api/v1/meetCore/appointmentRules/4</code>
        /// </summary>
        /// <param name="appointmentRuleId">The id</param>
        /// <returns></returns>
        public static string GetAppointmentRuleRoute(string appointmentRuleId) => AppointmentRulesRoute + $"/{appointmentRuleId}";

        #endregion

        #region Appointments

        /// <summary>
        /// The appointments route
        /// <code>/appointMate/api/v1/meetCore/appointments</code>
        /// </summary>
        public const string AppointmentsRoute = BaseRoute + "/appointments";

        /// <summary>
        /// The appointment route
        /// <code>/appointMate/api/v1/meetCore/appointments/3</code>
        /// </summary>
        public const string AppointmentRoute = AppointmentsRoute + "/{appointmentId}";

        /// <summary>
        /// Gets the route for the appointment with the specified <paramref name="appointmentId"/>
        /// <code>/appointMate/api/v1/meetCore/appointments/4</code>
        /// </summary>
        /// <param name="appointmentId">The id</param>
        /// <returns></returns>
        public static string GetAppointmentRoute(string appointmentId) => AppointmentsRoute + $"/{appointmentId}";

        #endregion
    }
}
