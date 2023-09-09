using MeetEdu;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;

using Newtonsoft.Json;

using System.Collections.Generic;
using System.Text;
using System.Web;

namespace MeetEdu
{
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

        public Task<WebRequestResult<DepartmentResponseModel>> DeleteSavedDeparmentAsync(string id) 
            => WebRequestsClient.Instance.DeleteAsync<DepartmentResponseModel>(GetAbsoluteUrl(MeetEduAPIRoutes.GetDepartmentRoute(id)), null);

        public Task<WebRequestResult<IEnumerable<AppointmentRuleResponseModel>>> GetAppointmentRulesAsync([FromQuery] AppointmentRuleAPIArgs? args = null) 
            => WebRequestsClient.Instance.GetAsync<IEnumerable<AppointmentRuleResponseModel>>(RouteHelpers.AttachParameters(GetAbsoluteUrl(MeetEduAPIRoutes.AppointmentRulesRoute), args), null);

        private static string GetAbsoluteUrl(string url)
        {
            return $"http://localhost:5050/" + url;
        }
    }


    public class ErrorMessageResponseModel
    {
        #region Public Properties

        public string Title { get; set; }

        public string Description { get; set; }

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
