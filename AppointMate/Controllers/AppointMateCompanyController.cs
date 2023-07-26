using Microsoft.AspNetCore.Mvc;

using MongoDB.Driver;

using System.Linq;

namespace AppointMate
{
    /// <summary>
    /// Controller used for handing the requests related to an AppointMate related application
    /// </summary>
    public class AppointMateController : Controller
    {
        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public AppointMateController() : base()
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
        [HttpPost]
        [Route(AppointMateAPIRoutes.RegisterRoute)]
        public async Task<ActionResult<UserResponseModel>> RegisterUserAsync([FromBody] UserRequestModel model)
            => (await DI.UsersRepository.AddUserAsync(model)).ToActionResult(x => x.ToResponseModel());

        /// <summary>
        /// Validates the user credentials sent by the user and returns the user
        /// </summary>
        /// <param name="model">The model</param>
        /// <returns></returns>
        [HttpPost]
        [Route(AppointMateAPIRoutes.LogInRoute)]
        public async Task<ActionResult<UserResponseModel>> LoginAsync([FromBody] LogInRequestModel model)
            => (await DI.AccountsRepository.LoginAsync(model)).ToActionResult(x => x.ToResponseModel());

        #endregion

        #region Services

        /// <summary>
        /// Gets the services
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route(AppointMateAPIRoutes.ServicesRoute)]
        public async Task<ActionResult<IEnumerable<ServiceResponseModel>>> GetServicesAsync()
        {
            var entities = await AppointMateDbMapper.Services.AsQueryable().ToListAsync();

            entities.OrderBy(x => x.Price).GroupBy(x => x.Name);

            return Ok(entities);
        }

        #endregion

        #endregion
    }
}
