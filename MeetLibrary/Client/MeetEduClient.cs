using Microsoft.AspNetCore.Mvc;

namespace MeetBase.Web
{
    /// <summary>
    /// 
    /// </summary>
    public class MeetEduClient
    {
        /// <summary>
        /// 
        /// </summary>
        public HttpClient client = new HttpClient();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<WebRequestResult<UserResponseModel>> GetUserAsync(string id)
        {
            return WebRequestsClient.Instance.GetAsync<UserResponseModel>(GetAbsoluteUrl(MeetEduAPIRoutes.GetUserRoute(id)), null);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Task<WebRequestResult<UserResponseModel>> RegisterUserAsync(UserRequestModel model)
        {
            return WebRequestsClient.Instance.PostAsync<UserResponseModel>(GetAbsoluteUrl(MeetEduAPIRoutes.RegisterRoute), model, null);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<WebRequestResult<DepartmentResponseModel>> DeleteSavedDeparmentAsync(string id) 
            => WebRequestsClient.Instance.DeleteAsync<DepartmentResponseModel>(GetAbsoluteUrl(MeetEduAPIRoutes.GetDepartmentRoute(id)), null);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public Task<WebRequestResult<IEnumerable<AppointmentRuleResponseModel>>> GetAppointmentRulesAsync([FromQuery] AppointmentRuleAPIArgs? args = null) 
            => WebRequestsClient.Instance.GetAsync<IEnumerable<AppointmentRuleResponseModel>>(RouteHelpers.AttachParameters(GetAbsoluteUrl(MeetEduAPIRoutes.AppointmentRulesRoute), args), null);

        private static string GetAbsoluteUrl(string url)
        {
            return $"http://localhost:5050/" + url;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class ErrorMessageResponseModel
    {
        #region Public Properties

        /// <summary>
        /// 
        /// </summary>
        public string? Title { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int Code { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public ErrorMessageResponseModel() : base()
        {

        }

        #endregion
    }
}
