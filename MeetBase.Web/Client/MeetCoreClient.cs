using Microsoft.AspNetCore.Http;

namespace MeetBase.Web
{
    /// <summary>
    /// Client that provides HTTP calls for sending and receiving information from the <c>MeetCore</c> server
    /// </summary>
    public class MeetCoreClient
    {
        #region Private Members

        /// <summary>
        /// The member of the <see cref="UniversityId"/>
        /// </summary>
        private string? mUniversityId;

        /// <summary>
        /// The member of the <see cref="DepartmentId"/>
        /// </summary>
        private string? mDepartmentId;

        #endregion

        #region Public Properties

        /// <summary>
        /// The university id
        /// </summary>
        public string UniversityId 
        { 
            get => mUniversityId ?? string.Empty;
            set => mUniversityId = value;
        }

        /// <summary>
        /// The department id
        /// </summary>
        public string DepartmentId
        {
            get => mDepartmentId ?? string.Empty;
            set => mDepartmentId = value;
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public MeetCoreClient() : base()
        {

        }

        #endregion

        #region Public Methods

        #region Register / Login

        /// <summary>
        /// Registers a user
        /// </summary>
        /// <param name="model">The model</param>
        /// <returns></returns>
        public Task<WebRequestResult<UserResponseModel>> RegisterUserAsync(UserRequestModel model)
            => WebRequestsClient.Instance.PostAsync<UserResponseModel>(GetAbsoluteUrl(MeetCoreAPIRoutes.RegisterRoute), model, null);

        /// <summary>
        /// Validates the user credentials sent by the user and returns the user
        /// </summary>
        /// <param name="model">The model</param>
        /// <returns></returns>
        public Task<WebRequestResult<LoginResponse>> LoginAsync(LogInRequestModel model)
            => WebRequestsClient.Instance.PostAsync<LoginResponse>(GetAbsoluteUrl(MeetCoreAPIRoutes.LogInRoute), model, null);

        /// <summary>
        /// Resets the user password
        /// </summary>
        /// <param name="model">The model</param>
        /// <returns></returns>
        public Task<WebRequestResult<UserResponseModel>> ResetUserPasswordAsync(ResetPasswordRequestModel model)
            => WebRequestsClient.Instance.PutAsync<UserResponseModel>(GetAbsoluteUrl(MeetCoreAPIRoutes.ResetRoute), model, null);

        #endregion

        #region Users

        /// <summary>
        /// Creates a user
        /// </summary>
        /// <param name="model">The model</param>
        /// <returns></returns>
        public Task<WebRequestResult<UserResponseModel>> AddUserAsync(UserRequestModel model)
            => WebRequestsClient.Instance.PostAsync<UserResponseModel>(GetAbsoluteUrl(MeetCoreAPIRoutes.UsersRoute), model, null);

        /// <summary>
        /// Updates the user with the specified <paramref name="id"/>
        /// </summary>
        /// <param name="id">The id</param>
        /// <param name="model">The model</param>
        /// <returns></returns>
        public Task<WebRequestResult<UserResponseModel>> UpdateUserAsync(string id, UserRequestModel model)
            => WebRequestsClient.Instance.PutAsync<UserResponseModel>(GetAbsoluteUrl(MeetCoreAPIRoutes.GetUserRoute(id)), model, null);

        /// <summary>
        /// Deletes the user with the specified <paramref name="id"/>
        /// </summary>
        /// <param name="id">The id</param>
        /// <returns></returns>
        public Task<WebRequestResult<UserResponseModel>> DeleteUserAsync(string id)
            => WebRequestsClient.Instance.DeleteAsync<UserResponseModel>(GetAbsoluteUrl(MeetCoreAPIRoutes.GetUserRoute(id)), null);

        #endregion

        #region Universities

        /// <summary>
        /// Gets the universities
        /// </summary>
        /// <param name="args">The arguments</param>
        /// <returns></returns>
        public Task<WebRequestResult<IEnumerable<UniversityResponseModel>>> GetUniversitiesAsync(APIArgs? args = null)
            => WebRequestsClient.Instance.GetAsync<IEnumerable<UniversityResponseModel>>(RouteHelpers.AttachParameters(GetAbsoluteUrl(MeetCoreAPIRoutes.UniversitiesRoute), args), null);

        /// <summary>
        /// Creates a university
        /// </summary>
        /// <param name="model">The model</param>
        /// <returns></returns>
        public Task<WebRequestResult<UniversityResponseModel>> AddUniversityAsync(UniversityRequestModel model)
            => WebRequestsClient.Instance.PostAsync<UniversityResponseModel>(GetAbsoluteUrl(MeetCoreAPIRoutes.UniversitiesRoute), model, null);

        /// <summary>
        /// Gets the university with the specified <paramref name="id"/>
        /// </summary>
        /// <param name="id">The id</param>
        /// <returns></returns>
        public Task<WebRequestResult<UniversityResponseModel>> GetUniversityAsync(string id)
            => WebRequestsClient.Instance.GetAsync<UniversityResponseModel>(GetAbsoluteUrl(MeetCoreAPIRoutes.GetUniversityRoute(id)), null);

        /// <summary>
        /// Updates the university with the specified <paramref name="id"/>
        /// </summary>
        /// <param name="id">The id</param>
        /// <param name="model">The model</param>
        /// <returns></returns>
        public Task<WebRequestResult<UniversityResponseModel>> UpdateUniversityAsync(string id, UniversityRequestModel model)
            => WebRequestsClient.Instance.PutAsync<UniversityResponseModel>(GetAbsoluteUrl(MeetCoreAPIRoutes.GetUniversityRoute(id)), model, null);

        /// <summary>
        /// Deletes the university with the specified <paramref name="id"/>
        /// </summary>
        /// <param name="id">The id</param>
        /// <returns></returns>
        public Task<WebRequestResult<UniversityResponseModel>> DeleteUniversityAsync(string id)
            => WebRequestsClient.Instance.DeleteAsync<UniversityResponseModel>(GetAbsoluteUrl(MeetCoreAPIRoutes.GetUniversityRoute(id)), null);

        #region Labels

        /// <summary>
        /// Gets the university labels
        /// </summary>
        /// <param name="args">The arguments</param>
        /// <returns></returns>
        public Task<WebRequestResult<IEnumerable<LabelResponseModel>>> GetUniversityLabelsAsync(APIArgs args)
            => WebRequestsClient.Instance.GetAsync<IEnumerable<LabelResponseModel>>(RouteHelpers.AttachParameters(GetAbsoluteUrl(MeetCoreAPIRoutes.UniversityLabelsRoute), args), null);


        /// <summary>
        /// Creates a university label
        /// </summary>
        /// <param name="model">The model</param>
        /// <returns></returns>
        public Task<WebRequestResult<LabelResponseModel>> AddUniversityLabelAsync(LabelRequestModel model)
            => WebRequestsClient.Instance.PostAsync<LabelResponseModel>(GetAbsoluteUrl(MeetCoreAPIRoutes.UniversityLabelsRoute), model, null);

        /// <summary>
        /// Gets the university label with the specified <paramref name="id"/>
        /// </summary>
        /// <param name="id">The id</param>
        /// <returns></returns>
        public Task<WebRequestResult<LabelResponseModel>> GetUniversityLabelAsync(string id)
            => WebRequestsClient.Instance.GetAsync<LabelResponseModel>(GetAbsoluteUrl(MeetCoreAPIRoutes.GetUniversityLabelRoute(id)), null);

        /// <summary>
        /// Updates the university label with the specified <paramref name="id"/>
        /// </summary>
        /// <param name="id">The id</param>
        /// <param name="model">The model</param>
        /// <returns></returns>
        public Task<WebRequestResult<LabelResponseModel>> UpdateUniversityLabelAsync(string id, LabelRequestModel model)
            => WebRequestsClient.Instance.PutAsync<LabelResponseModel>(GetAbsoluteUrl(MeetCoreAPIRoutes.GetUniversityLabelRoute(id)), model, null);

        /// <summary>
        /// Deletes the university label with the specified <paramref name="id"/>
        /// </summary>
        /// <param name="id">The id</param>
        /// <returns></returns>
        public Task<WebRequestResult<LabelResponseModel>> DeleteUniversityLabelAsync(string id)
            => WebRequestsClient.Instance.DeleteAsync<LabelResponseModel>(GetAbsoluteUrl(MeetCoreAPIRoutes.GetUniversityLabelRoute(id)), null);

        #endregion

        #endregion

        #region Departments

        /// <summary>
        /// Gets the departments
        /// </summary>
        /// <param name="args">The arguments</param>
        /// <returns></returns>
        public Task<WebRequestResult<IEnumerable<DepartmentResponseModel>>> GetDepartmentsAsync(DepartmentAPIArgs args)
            => WebRequestsClient.Instance.GetAsync<IEnumerable<DepartmentResponseModel>>(RouteHelpers.AttachParameters(GetAbsoluteUrl(MeetCoreAPIRoutes.DepartmentsRoute), args), null);


        /// <summary>
        /// Creates a department
        /// </summary>
        /// <param name="model">The model</param>
        /// <returns></returns>
        public Task<WebRequestResult<DepartmentResponseModel>> AddDepartmentAsync(DepartmentRequestModel model)
            => WebRequestsClient.Instance.PostAsync<DepartmentResponseModel>(GetAbsoluteUrl(MeetCoreAPIRoutes.DepartmentsRoute), model, null);


        /// <summary>
        /// Gets the department with the specified <paramref name="id"/>
        /// </summary>
        /// <param name="id">The id</param>
        /// <returns></returns>
        public Task<WebRequestResult<DepartmentResponseModel>> GetDepartmentAsync(string id)
            => WebRequestsClient.Instance.GetAsync<DepartmentResponseModel>(GetAbsoluteUrl(MeetCoreAPIRoutes.GetDepartmentRoute(id)), null);

        /// <summary>
        /// Updates the department with the specified <paramref name="id"/>
        /// </summary>
        /// <param name="id">The id</param>
        /// <param name="model">The model</param>
        /// <returns></returns>
        public Task<WebRequestResult<DepartmentResponseModel>> UpdateDepartmentAsync(string id, DepartmentRequestModel model)
            => WebRequestsClient.Instance.PutAsync<DepartmentResponseModel>(GetAbsoluteUrl(MeetCoreAPIRoutes.GetDepartmentRoute(id)), model, null);

        /// <summary>
        /// Deletes the department with the specified <paramref name="id"/>
        /// </summary>
        /// <param name="id">The id</param>
        /// <returns></returns>
        public Task<WebRequestResult<DepartmentResponseModel>> DeleteDepartmentAsync(string id)
            => WebRequestsClient.Instance.DeleteAsync<DepartmentResponseModel>(GetAbsoluteUrl(MeetCoreAPIRoutes.GetDepartmentRoute(id)), null);

        #region Layout 

        /// <summary>
        /// Gets the department layouts
        /// </summary>
        /// <param name="args">The arguments</param>
        /// <returns></returns>
        public Task<WebRequestResult<IEnumerable<DepartmentLayoutResponseModel>>> GetDepartmentLayoutsAsync(DepartmentRelatedAPIArgs args)
            => WebRequestsClient.Instance.GetAsync<IEnumerable<DepartmentLayoutResponseModel>>(RouteHelpers.AttachParameters(GetAbsoluteUrl(MeetCoreAPIRoutes.DepartmentLayoutsRoute), args), null);

        /// <summary>
        /// Creates a department layout
        /// </summary>
        /// <param name="model">The model</param>
        /// <returns></returns>
        public Task<WebRequestResult<DepartmentLayoutResponseModel>> AddDepartmentLayoutAsync(DepartmentLayoutRequestModel model)
            => WebRequestsClient.Instance.PostAsync<DepartmentLayoutResponseModel>(GetAbsoluteUrl(MeetCoreAPIRoutes.DepartmentLayoutsRoute), model, null);

        /// <summary>
        /// Gets the department layout with the specified <paramref name="id"/>
        /// </summary>
        /// <param name="id">The id</param>
        /// <returns></returns>
        public Task<WebRequestResult<DepartmentLayoutResponseModel>> GetDepartmentLayoutAsync(string id)
            => WebRequestsClient.Instance.GetAsync<DepartmentLayoutResponseModel>(GetAbsoluteUrl(MeetCoreAPIRoutes.GetDepartmentLayoutRoute(id)), null);

        /// <summary>
        /// Updates the department with the specified <paramref name="id"/>
        /// </summary>
        /// <param name="id">The id</param>
        /// <param name="model">The model</param>
        /// <returns></returns>
        public Task<WebRequestResult<DepartmentLayoutResponseModel>> UpdateDepartmentLayoutAsync(string id, DepartmentLayoutRequestModel model)
            => WebRequestsClient.Instance.PutAsync<DepartmentLayoutResponseModel>(GetAbsoluteUrl(MeetCoreAPIRoutes.GetDepartmentLayoutRoute(id)), model, null);

        /// <summary>
        /// Deletes the department with the specified <paramref name="id"/>
        /// </summary>
        /// <param name="id">The id</param>
        /// <returns></returns>
        public Task<WebRequestResult<DepartmentLayoutResponseModel>> DeleteDepartmentLayoutAsync(string id)
            => WebRequestsClient.Instance.DeleteAsync<DepartmentLayoutResponseModel>(GetAbsoluteUrl(MeetCoreAPIRoutes.GetDepartmentLayoutRoute(id)), null);

        /// <summary>
        /// Creates a department layout
        /// </summary>
        /// <param name="id">The id</param>
        /// <param name="file">The file</param>
        /// <returns></returns>
        public Task<WebRequestResult<DepartmentLayoutResponseModel>> SetDepartmentLayoutImageAsync(string id, IFormFile file)
            => WebRequestsClient.Instance.PutFilesAsync<DepartmentLayoutResponseModel>(
                GetAbsoluteUrl(MeetCoreAPIRoutes.GetDepartmentLayoutImagesRoute(id)), 
                new List<FileUploadGroupDataModel>() { new FileUploadGroupDataModel("file", file) }, 
                null);

        #endregion

        #region Labels

        /// <summary>
        /// Gets the department labels
        /// </summary>
        /// <param name="args">The arguments</param>
        /// <returns></returns>
        public Task<WebRequestResult<IEnumerable<LabelResponseModel>>> GetDepartmentLabelsAsync(DepartmentRelatedAPIArgs args)
            => WebRequestsClient.Instance.GetAsync<IEnumerable<LabelResponseModel>>(RouteHelpers.AttachParameters(GetAbsoluteUrl(MeetCoreAPIRoutes.DepartmentLabelsRoute), args), null);

        /// <summary>
        /// Creates a department label
        /// </summary>
        /// <param name="model">The model</param>
        /// <returns></returns>
        public Task<WebRequestResult<LabelResponseModel>> AddDepartmentLabelAsync(LabelRequestModel model)
            => WebRequestsClient.Instance.PostAsync<LabelResponseModel>(GetAbsoluteUrl(MeetCoreAPIRoutes.DepartmentLabelsRoute), model, null);

        /// <summary>
        /// Gets the department label with the specified <paramref name="id"/>
        /// </summary>
        /// <param name="id">The id</param>
        /// <returns></returns>
        public Task<WebRequestResult<LabelResponseModel>> GetDepartmentLabelAsync(string id)
            => WebRequestsClient.Instance.GetAsync<LabelResponseModel>(GetAbsoluteUrl(MeetCoreAPIRoutes.GetDepartmentLabelRoute(id)), null);

        /// <summary>
        /// Updates the department label with the specified <paramref name="id"/>
        /// </summary>
        /// <param name="id">The id</param>
        /// <param name="model">The model</param>
        /// <returns></returns>
        public Task<WebRequestResult<LabelResponseModel>> UpdateDepartmentLabelAsync(string id, LabelRequestModel model)
            => WebRequestsClient.Instance.PutAsync<LabelResponseModel>(GetAbsoluteUrl(MeetCoreAPIRoutes.GetDepartmentLabelRoute(id)), model, null);

        /// <summary>
        /// Deletes the department label with the specified <paramref name="id"/>
        /// </summary>
        /// <param name="id">The id</param>
        /// <returns></returns>
        public Task<WebRequestResult<LabelResponseModel>> DeleteDepartmentLabelAsync(string id)
            => WebRequestsClient.Instance.DeleteAsync<LabelResponseModel>(GetAbsoluteUrl(MeetCoreAPIRoutes.GetDepartmentLabelRoute(id)), null);

        #endregion

        #region Contact

        /// <summary>
        /// Gets the department contact messages
        /// </summary>
        /// <param name="args">The arguments</param>
        /// <returns></returns>
        public Task<WebRequestResult<IEnumerable<DepartmentContactMessageResponseModel>>> GetDepartmentContactMessagesAsync(DepartmentRelatedAPIArgs args)
            => WebRequestsClient.Instance.GetAsync<IEnumerable<DepartmentContactMessageResponseModel>>(RouteHelpers.AttachParameters(GetAbsoluteUrl(MeetCoreAPIRoutes.DepartmentContactMessagesRoute), args), null);

        /// <summary>
        /// Creates a department contact message
        /// </summary>
        /// <param name="model">The model</param>
        /// <returns></returns>
        public Task<WebRequestResult<DepartmentContactMessageResponseModel>> AddDepartmentContactMessageAsync(DepartmentContactMessageRequestModel model)
            => WebRequestsClient.Instance.PostAsync<DepartmentContactMessageResponseModel>(GetAbsoluteUrl(MeetCoreAPIRoutes.DepartmentContactMessagesRoute), model, null);

        /// <summary>
        /// Gets the department contact message with the specified <paramref name="id"/>
        /// </summary>
        /// <param name="id">The id</param>
        /// <returns></returns>
        public Task<WebRequestResult<DepartmentContactMessageResponseModel>> GetDepartmentContactMessageAsync(string id)
            => WebRequestsClient.Instance.GetAsync<DepartmentContactMessageResponseModel>(GetAbsoluteUrl(MeetCoreAPIRoutes.GetDepartmentContactMessageRoute(id)), null);

        /// <summary>
        /// Updates the department contact message with the specified <paramref name="id"/>
        /// </summary>
        /// <param name="id">The id</param>
        /// <param name="model">The model</param>
        /// <returns></returns>
        public Task<WebRequestResult<DepartmentContactMessageResponseModel>> UpdateDepartmentContactMessageAsync(string id, DepartmentContactMessageRequestModel model)
            => WebRequestsClient.Instance.PutAsync<DepartmentContactMessageResponseModel>(GetAbsoluteUrl(MeetCoreAPIRoutes.GetDepartmentContactMessageRoute(id)), model, null);

        /// <summary>
        /// Deletes the department contact message with the specified <paramref name="id"/>
        /// </summary>
        /// <param name="id">The id</param>
        /// <returns></returns>
        public Task<WebRequestResult<DepartmentContactMessageResponseModel>> DeleteDepartmentContactMessageAsync(string id)
            => WebRequestsClient.Instance.DeleteAsync<DepartmentContactMessageResponseModel>(GetAbsoluteUrl(MeetCoreAPIRoutes.GetDepartmentContactMessageRoute(id)), null);

        #endregion

        #endregion

        #region Secretaries

        /// <summary>
        /// Gets the secretaries
        /// </summary>
        /// <param name="args">The arguments</param>
        /// <returns></returns>
        public Task<WebRequestResult<IEnumerable<SecretaryResponseModel>>> GetSecretariesAsync(DepartmentRelatedAPIArgs args)
            => WebRequestsClient.Instance.GetAsync<IEnumerable<SecretaryResponseModel>>(RouteHelpers.AttachParameters(GetAbsoluteUrl(MeetCoreAPIRoutes.SecretariesRoute), args), null);

        /// <summary>
        /// Creates a secretary
        /// </summary>
        /// <param name="model">The model</param>
        /// <returns></returns>
        public Task<WebRequestResult<SecretaryResponseModel>> AddSecretaryAsync(SecretaryRequestModel model)
            => WebRequestsClient.Instance.PostAsync<SecretaryResponseModel>(GetAbsoluteUrl(MeetCoreAPIRoutes.SecretariesRoute), model, null);

        /// <summary>
        /// Gets the secretary
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<WebRequestResult<SecretaryResponseModel>> GetSecretaryAsync(string id)
            => WebRequestsClient.Instance.GetAsync<SecretaryResponseModel>(GetAbsoluteUrl(MeetCoreAPIRoutes.GetSecretaryRoute(id)), null);

        /// <summary>
        /// Updates the secretary with the specified <paramref name="id"/>
        /// </summary>
        /// <param name="id">The id</param>
        /// <param name="model">The model</param>
        /// <returns></returns>
        public Task<WebRequestResult<SecretaryResponseModel>> UpdateSecretaryAsync(string id, SecretaryRequestModel model)
            => WebRequestsClient.Instance.PutAsync<SecretaryResponseModel>(GetAbsoluteUrl(MeetCoreAPIRoutes.GetSecretaryRoute(id)), model, null);

        /// <summary>
        /// Deletes the secretary with the specified <paramref name="id"/>
        /// </summary>
        /// <param name="id">The id</param>
        /// <returns></returns>
        public Task<WebRequestResult<SecretaryResponseModel>> DeleteSecretaryAsync(string id)
            => WebRequestsClient.Instance.DeleteAsync<SecretaryResponseModel>(GetAbsoluteUrl(MeetCoreAPIRoutes.GetSecretaryRoute(id)), null);

        #endregion

        #region Professors

        /// <summary>
        /// Gets the professors
        /// </summary>
        /// <param name="args">The arguments</param>
        /// <returns></returns>
        public Task<WebRequestResult<IEnumerable<ProfessorResponseModel>>> GetProfessorsAsync(DepartmentRelatedAPIArgs args)
            => WebRequestsClient.Instance.GetAsync<IEnumerable<ProfessorResponseModel>>(RouteHelpers.AttachParameters(GetAbsoluteUrl(MeetCoreAPIRoutes.ProfessorsRoute), args), null);

        /// <summary>
        /// Creates a professor
        /// </summary>
        /// <param name="model">The model</param>
        /// <returns></returns>
        public Task<WebRequestResult<ProfessorResponseModel>> AddProfessorAsync(ProfessorRequestModel model)
            => WebRequestsClient.Instance.PostAsync<ProfessorResponseModel>(GetAbsoluteUrl(MeetCoreAPIRoutes.ProfessorsRoute), model, null);

        /// <summary>
        /// Gets the professor
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<WebRequestResult<ProfessorResponseModel>> GetProfessorAsync(string id)
            => WebRequestsClient.Instance.GetAsync<ProfessorResponseModel>(GetAbsoluteUrl(MeetCoreAPIRoutes.GetProfessorRoute(id)), null);

        /// <summary>
        /// Updates the professor with the specified <paramref name="id"/>
        /// </summary>
        /// <param name="id">The id</param>
        /// <param name="model">The model</param>
        /// <param name="user">The user</param>
        /// <returns></returns>
        public Task<WebRequestResult<ProfessorResponseModel>> UpdateProfessorAsync(string id, ProfessorRequestModel model)
            => WebRequestsClient.Instance.PutAsync<ProfessorResponseModel>(GetAbsoluteUrl(MeetCoreAPIRoutes.GetProfessorRoute(id)), model, null);

        /// <summary>
        /// Deletes the professor with the specified <paramref name="id"/>
        /// </summary>
        /// <param name="id">The id</param>
        /// <returns></returns>
        public Task<WebRequestResult<ProfessorResponseModel>> DeleteProfessorAsync(string id)
            => WebRequestsClient.Instance.DeleteAsync<ProfessorResponseModel>(GetAbsoluteUrl(MeetCoreAPIRoutes.GetProfessorRoute(id)), null);

        #endregion

        #region Appointment Rules

        /// <summary>
        /// Gets the appointment rules
        /// </summary>
        /// <param name="args">The arguments</param>
        /// <returns></returns>
        public Task<WebRequestResult<IEnumerable<AppointmentRuleResponseModel>>> GetAppointmentRulesAsync(AppointmentRuleAPIArgs args)
            => WebRequestsClient.Instance.GetAsync<IEnumerable<AppointmentRuleResponseModel>>(RouteHelpers.AttachParameters(GetAbsoluteUrl(MeetCoreAPIRoutes.AppointmentRulesRoute), args), null);

        /// <summary>
        /// Creates an appointment rule
        /// </summary>
        /// <param name="model">The model</param>
        /// <returns></returns>
        public Task<WebRequestResult<AppointmentRuleResponseModel>> AddAppointmentRuleAsync(AppointmentRuleRequestModel model)
            => WebRequestsClient.Instance.PostAsync<AppointmentRuleResponseModel>(GetAbsoluteUrl(MeetCoreAPIRoutes.AppointmentRulesRoute), model, null);

        /// <summary>
        /// Gets the appointment rule with the specified <paramref name="id"/>
        /// </summary>
        /// <param name="id">The id</param>
        /// <returns></returns>
        public Task<WebRequestResult<AppointmentRuleResponseModel>> GetAppointmentRuleAsync(string id)
            => WebRequestsClient.Instance.GetAsync<AppointmentRuleResponseModel>(GetAbsoluteUrl(MeetCoreAPIRoutes.GetAppointmentRuleRoute(id)), null);

        /// <summary>
        /// Updates the appointment rule with the specified <paramref name="id"/>
        /// </summary>
        /// <param name="id">The id</param>
        /// <param name="model">The model</param>
        /// <returns></returns>
        public Task<WebRequestResult<AppointmentRuleResponseModel>> UpdateAppointmentRuleAsync(string id, AppointmentRuleRequestModel model)
            => WebRequestsClient.Instance.PutAsync<AppointmentRuleResponseModel>(GetAbsoluteUrl(MeetCoreAPIRoutes.GetAppointmentRuleRoute(id)), model, null);

        /// <summary>
        /// Deletes the appointment rule with the specified <paramref name="id"/>
        /// </summary>
        /// <param name="id">The id</param>
        /// <returns></returns>
        public Task<WebRequestResult<AppointmentRuleResponseModel>> DeleteAppointmentRuleAsync(string id)
            => WebRequestsClient.Instance.DeleteAsync<AppointmentRuleResponseModel>(GetAbsoluteUrl(MeetCoreAPIRoutes.GetAppointmentRuleRoute(id)), null);

        #endregion

        #region Appointments

        /// <summary>
        /// Gets the appointments
        /// </summary>
        /// <param name="args">The arguments</param>
        /// <returns></returns>
        public Task<WebRequestResult<IEnumerable<AppointmentResponseModel>>> GetAppointmentsAsync(APIArgs args)
            => WebRequestsClient.Instance.GetAsync<IEnumerable<AppointmentResponseModel>>(RouteHelpers.AttachParameters(GetAbsoluteUrl(MeetCoreAPIRoutes.AppointmentsRoute), args), null);

        /// <summary>
        /// Creates a appointment
        /// </summary>
        /// <param name="model">The model</param>
        /// <returns></returns>
        public Task<WebRequestResult<AppointmentResponseModel>> AddAppointmentAsync(AppointmentRequestModel model)
            => WebRequestsClient.Instance.PostAsync<AppointmentResponseModel>(GetAbsoluteUrl(MeetCoreAPIRoutes.AppointmentsRoute), model, null);

        /// <summary>
        /// Gets the appointment with the specified <paramref name="id"/>
        /// </summary>
        /// <param name="id">The id</param>
        /// <returns></returns>
        public Task<WebRequestResult<AppointmentResponseModel>> GetAppointmentAsync(string id)
            => WebRequestsClient.Instance.GetAsync<AppointmentResponseModel>(GetAbsoluteUrl(MeetCoreAPIRoutes.GetAppointmentRoute(id)), null);

        /// <summary>
        /// Updates the appointment with the specified <paramref name="id"/>
        /// </summary>
        /// <param name="id">The id</param>
        /// <param name="model">The model</param>
        /// <returns></returns>
        public Task<WebRequestResult<AppointmentResponseModel>> UpdateAppointmentAsync(string id, AppointmentRequestModel model)
            => WebRequestsClient.Instance.PutAsync<AppointmentResponseModel>(GetAbsoluteUrl(MeetCoreAPIRoutes.GetAppointmentRoute(id)), model, null);

        /// <summary>
        /// Deletes the appointment with the specified <paramref name="id"/>
        /// </summary>
        /// <param name="id">The id</param>
        /// <returns></returns>
        public Task<WebRequestResult<AppointmentResponseModel>> DeleteAppointmentAsync(string id)
            => WebRequestsClient.Instance.DeleteAsync<AppointmentResponseModel>(GetAbsoluteUrl(MeetCoreAPIRoutes.GetAppointmentRoute(id)), null);

        #endregion

        #endregion

        #region Private Methods

        /// <summary>
        /// Gets the absolute URL of the site
        /// </summary>
        /// <param name="url">The url</param>
        /// <returns></returns>
        private static string GetAbsoluteUrl(string url) 
            => $"https://localhost:44307" + url;

        #endregion
    }
}
