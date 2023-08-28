namespace AppointMate
{
    /// <summary>
    /// The AppontMate related API routes
    /// </summary>
    public static class MeetEduAPIRoutes
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
        /// <code>/appointMate/api/v1/meetEdu</code>
        /// </summary>
        private const string BaseRoute = VersionRoute + "/meetEdu";

        #region Login

        /// <summary>
        /// The login route
        /// <code>/appointMate/api/v1/meetEdu/login</code>
        /// </summary>
        public const string LogInRoute = BaseRoute + "/logIn";

        #endregion

        #region Register

        /// <summary>
        /// The register route
        /// <code>/appointMate/api/v1/meetEdu/register</code>
        /// </summary>
        public const string RegisterRoute = BaseRoute + "/register";

        #endregion

        #region Users

        /// <summary>
        /// The users route
        /// <code>/appointMate/api/v1/meetEdu/users</code>
        /// </summary>
        public const string UsersRoute = BaseRoute + "/users";

        /// <summary>
        /// The user route
        /// <code>/appointMate/api/v1/meetEdu/users/3</code>
        /// </summary>
        public const string UserRoute = UsersRoute + "/{userId}";

        /// <summary>
        /// Gets the route for the user with the specified <paramref name="id"/>
        /// <code>/appointMate/api/v1/meetEdu/users/4</code>
        /// </summary>
        /// <param name="id">The id</param>
        /// <returns></returns>
        public static string GetUserRoute(string id) => UsersRoute + $"/{id}";

        #endregion

        #region Universities

        /// <summary>
        /// The universities route
        /// <code>/appointMate/api/v1/meetEdu/universities</code>
        /// </summary>
        public const string UniversitiesRoute = BaseRoute + "/universities";

        #endregion

        #region Departments

        /// <summary>
        /// The departments route
        /// <code>/appointMate/api/v1/meetEdu/departments</code>
        /// </summary>
        public const string DepartmentsRoute = BaseRoute + "/departments";

        /// <summary>
        /// The department route
        /// <code>/appointMate/api/v1/meetEdu/departments/3</code>
        /// </summary>
        public const string DepartmentRoute = DepartmentsRoute + "/{departmentId}";

        /// <summary>
        /// Gets the department route for the one with the specified <paramref name="id"/>
        /// <code>/appointMate/api/v1/meetEdu/departments/3</code>
        /// </summary>
        /// <param name="id">The id</param>
        /// <returns></returns>
        public static string GetDepartmentRoute(string id) => DepartmentsRoute + $"/{id}";

        #endregion

        #region Department Contact Messages

        /// <summary>
        /// The department contact messages route
        /// <code>/appointMate/api/v1/meetEdu/departments/3/departmentContactMessages</code>
        /// </summary>
        public const string DepartmentContactMessagesRoute = BaseRoute + "/departmentContactMessages";

        /// <summary>
        /// The department contact message route
        /// <code>/appointMate/api/v1/meetEdu/departmentContactMessages/3</code>
        /// </summary>
        public const string DepartmentContactMessageRoute = DepartmentsRoute + "/{departmentContactMessageId}";

        /// <summary>
        /// Gets the route for the department contact message with the specified <paramref name="id"/>
        /// <code>/appointMate/api/v1/meetEdu/departmentContactMessages/2</code>
        /// </summary>
        /// <param name="id">The id</param>
        /// <returns></returns>
        public static string GetDepartmentContactMessageRoute(string id) => DepartmentContactMessagesRoute + $"/{id}";

        #endregion

        #region Secretaries

        /// <summary>
        /// The secretaries route
        /// <code>/appointMate/api/v1/meetEdu/secretaries</code>
        /// </summary>
        public const string SecretariesRoute = BaseRoute + "/secretaries";

        /// <summary>
        /// The secretary route
        /// <code>/appointMate/api/v1/meetEdu/secretaries/3</code>
        /// </summary>
        public const string SecretaryRoute = SecretariesRoute + "/{secretaryId}";

        /// <summary>
        /// Gets the route for the secretary with the specified <paramref name="id"/>
        /// <code>/appointMate/api/v1/meetEdu/secretaries/4</code>
        /// </summary>
        /// <param name="id">The id</param>
        /// <returns></returns>
        public static string GetSecretaryRoute(string id) => SecretariesRoute + $"/{id}";

        #endregion

        #region Professors
        /// <summary>
        /// The professors route
        /// <code>/appointMate/api/v1/meetEdu/professors</code>
        /// </summary>
        public const string ProfessorsRoute = BaseRoute + "/professors";

        /// <summary>
        /// The professor route
        /// <code>/appointMate/api/v1/meetEdu/professors/3</code>
        /// </summary>
        public const string ProfessorRoute = ProfessorsRoute + "/{professorId}";

        /// <summary>
        /// Gets the professor route for the one with the specified <paramref name="id"/>
        /// <code>/appointMate/api/v1/meetEdu/professors/3</code>
        /// </summary>
        /// <param name="id">The id</param>
        /// <returns></returns>
        public static string GetProfessorRoute(string id) => ProfessorsRoute + $"/{id}";

        #endregion

        #region Appointment Rules

        /// <summary>
        /// The appointment rules route
        /// <code>/appointMate/api/v1/meetEdu/appointmentRules</code>
        /// </summary>
        public const string AppointmentRulesRoute = BaseRoute + "/appointmentRules";

        /// <summary>
        /// The appointment rule route
        /// <code>/appointMate/api/v1/meetEdu/appointmentRules/3</code>
        /// </summary>
        public const string AppointmentRuleRoute = AppointmentRulesRoute + "/{appointmentRuleId}";

        /// <summary>
        /// Gets the route for the appointment with the specified <paramref name="id"/>
        /// <code>/appointMate/api/v1/meetEdu/appointmentRules/4</code>
        /// </summary>
        /// <param name="id">The id</param>
        /// <returns></returns>
        public static string GetAppointmentRuleRoute(string id) => AppointmentRulesRoute + $"/{id}";

        #endregion

        #region Members

        /// <summary>
        /// The members route
        /// <code>/appointMate/api/v1/meetEdu/members</code>
        /// </summary>
        public const string MembersRoute = BaseRoute + "/members";

        /// <summary>
        /// The member route
        /// <code>/appointMate/api/v1/meetEdu/members/3</code>
        /// </summary>
        public const string MemberRoute = MembersRoute + "/{memberId}";

        /// <summary>
        /// Gets the member route for the one with the specified <paramref name="id"/>
        /// <code>/appointMate/api/v1/meetEdu/members/3</code>
        /// </summary>
        /// <param name="id">The id</param>
        /// <returns></returns>
        public static string GetMemberRoute(string id) => MembersRoute + $"/{id}";

        #endregion

        #region Saved Departments

        /// <summary>
        /// The saved departments route
        /// <code>/appointMate/api/v1/meetEdu/savedDepartments</code>
        /// </summary>
        public const string SavedDepartmentsRoute = BaseRoute + "/savedDepartments";

        #endregion

        #region Saved Professors

        /// <summary>
        /// The saved departments route
        /// <code>/appointMate/api/v1/meetEdu/savedProfessors</code>
        /// </summary>
        public const string SavedProfessorsRoute = BaseRoute + "/savedProfessors";

        #endregion

        #region Appointments

        /// <summary>
        /// The appointments route
        /// <code>/appointMate/api/v1/meetEdu/appointments</code>
        /// </summary>
        public const string AppointmentsRoute = BaseRoute + "/appointments";

        /// <summary>
        /// The appointment route
        /// <code>/appointMate/api/v1/meetEdu/appointments/3</code>
        /// </summary>
        public const string AppointmentRoute = AppointmentsRoute + "/{appointmentId}";

        /// <summary>
        /// Gets the route for the appointment with the specified <paramref name="id"/>
        /// <code>/appointMate/api/v1/meetEdu/appointments/4</code>
        /// </summary>
        /// <param name="id">The id</param>
        /// <returns></returns>
        public static string GetAppointmentRoute(string id) => AppointmentsRoute + $"/{id}";

        #endregion
    }

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
        public const string LogInRoute = BaseRoute + "/logIn";

        #endregion

        #region Register

        /// <summary>
        /// The register route
        /// <code>/appointMate/api/v1/meetCore/register</code>
        /// </summary>
        public const string RegisterRoute = BaseRoute + "/register";

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
        /// Gets the route for the user with the specified <paramref name="id"/>
        /// <code>/appointMate/api/v1/meetCore/users/4</code>
        /// </summary>
        /// <param name="id">The id</param>
        /// <returns></returns>
        public static string GetUserRoute(string id) => UsersRoute + $"/{id}";

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
        /// Gets the route for the university with the specified <paramref name="id"/>
        /// <code>/appointMate/api/v1/meetCore/universities/5</code>
        /// </summary>
        /// <param name="id">The id</param>
        /// <returns></returns>
        public static string GetUniversityRoute(string id) => UniversitiesRoute + $"/{id}";

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
        /// Gets the department route for the one with the specified <paramref name="id"/>
        /// <code>/appointMate/api/v1/meetCore/departments/3</code>
        /// </summary>
        /// <param name="id">The id</param>
        /// <returns></returns>
        public static string GetDepartmentRoute(string id) => DepartmentsRoute + $"/{id}";

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
        public const string DepartmentContactMessageRoute = DepartmentsRoute + "/{departmentContactMessageId}";

        /// <summary>
        /// Gets the route for the department contact message with the specified <paramref name="id"/>
        /// <code>/appointMate/api/v1/meetCore/departmentContactMessages/2</code>
        /// </summary>
        /// <param name="id">The id</param>
        /// <returns></returns>
        public static string GetDepartmentContactMessageRoute(string id) => DepartmentContactMessagesRoute + $"/{id}";

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
        /// Gets the route for the secretary with the specified <paramref name="id"/>
        /// <code>/appointMate/api/v1/meetCore/secretaries/4</code>
        /// </summary>
        /// <param name="id">The id</param>
        /// <returns></returns>
        public static string GetSecretaryRoute(string id) => SecretariesRoute + $"/{id}";

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
        /// Gets the professor route for the one with the specified <paramref name="id"/>
        /// <code>/appointMate/api/v1/meetCore/professors/3</code>
        /// </summary>
        /// <param name="id">The id</param>
        /// <returns></returns>
        public static string GetProfessorRoute(string id) => ProfessorsRoute + $"/{id}";

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
        /// Gets the route for the appointment with the specified <paramref name="id"/>
        /// <code>/appointMate/api/v1/meetCore/appointmentRules/4</code>
        /// </summary>
        /// <param name="id">The id</param>
        /// <returns></returns>
        public static string GetAppointmentRuleRoute(string id) => AppointmentRulesRoute + $"/{id}";

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
        /// Gets the route for the appointment with the specified <paramref name="id"/>
        /// <code>/appointMate/api/v1/meetCore/appointments/4</code>
        /// </summary>
        /// <param name="id">The id</param>
        /// <returns></returns>
        public static string GetAppointmentRoute(string id) => AppointmentsRoute + $"/{id}";

        #endregion
    }
}
