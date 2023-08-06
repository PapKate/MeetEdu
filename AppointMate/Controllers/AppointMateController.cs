using AppointMate.Helpers;

using Microsoft.AspNetCore.Mvc;

using MongoDB.Bson;
using MongoDB.Driver;

using System.Collections;
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
        /// <param name="args">The arguments</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns></returns>
        [HttpGet]
        [Route(AppointMateAPIRoutes.ServicesRoute)]
        public async Task<ActionResult<IEnumerable<ServiceCompaniesResult>>> GetServicesAsync([FromQuery]StandardAPIArgs args, CancellationToken cancellationToken = default)
        {
            var filters = new List<FilterDefinition<ServiceEntity>>();

            if(!args.IncludeCompanies.IsNullOrEmpty())
            {
                var ids = args.IncludeCompanies.Select(x => x.ToObjectId()).ToList();
                filters.Add(Builders<ServiceEntity>.Filter.In(x => x.Company!.Source, ids));
            }
            
            if(!args.ExcludeCompanies.IsNullOrEmpty())
            {
                var ids = args.ExcludeCompanies.Select(x => x.ToObjectId()).ToList();
                filters.Add(Builders<ServiceEntity>.Filter.Nin(x => x.Company!.Source, ids));
            }

            var filter = Builders<ServiceEntity>.Filter.And(filters);

            var services = await ControllerHelpers.GetManyAsync(AppointMateDbMapper.Services, filter, x => x.Name, true, args, x => x.ToResponseModel(), cancellationToken);
        }

        /// <summary>
        /// Gets the services with the specified <paramref name="name"/>
        /// </summary>
        /// <param name="name">The id</param>
        /// <param name="args">The arguments</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns></returns>
        [HttpGet]
        [Route(AppointMateAPIRoutes.ServiceRoute)]
        public async Task<ActionResult<ServiceCompaniesResult>> GetServiceAsync([FromRoute] string name, APIArgs args, CancellationToken cancellationToken = default)
        {
            var documents = await AppointMateDbMapper.Services.SelectAsync(x => x.Name == name, cancellationToken);
            var services = documents.OrderBy(x => x.Price)
                                         .Skip((args.Page * args.PerPage) + args.Offset).Take(args.PerPage)
                                         .ToList();

            return new ServiceCompaniesResult(name, services.Select(x => x.ToResponseModel()));
        }

        #endregion

        #region Companies

        /// <summary>
        /// Gets the company with the specified <paramref name="id"/>
        /// </summary>
        /// <param name="id">The id</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns></returns>
        [HttpGet]
        [Route(AppointMateAPIRoutes.CompanyRoute)]
        public async Task<ActionResult<CompanyResponseModel>?> GetCompanyAsync([FromRoute] string id, CancellationToken cancellationToken = default)
            => await ControllerHelpers.GetAsync(AppointMateDbMapper.Companies, x => x.Id == id.ToObjectId(), x => x.ToResponseModel(), cancellationToken);

        #endregion

        #region Users

        /// <summary>
        /// Gets the user with the specified <paramref name="id"/>
        /// </summary>
        /// <param name="id">The id</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns></returns>
        [HttpGet]
        [Route(AppointMateAPIRoutes.UserRoute)]
        public async Task<ActionResult<UserResponseModel>?> GetUserAsync([FromRoute] string id, CancellationToken cancellationToken = default) 
            => await ControllerHelpers.GetAsync(AppointMateDbMapper.Users, x => x.Id.ToString() == id, x => x.ToResponseModel(), cancellationToken);

        #region User Services

        /// <summary>
        /// Gets the services of the user with the specified <paramref name="userId"/>
        /// </summary>
        /// <param name="userId">The id</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns></returns>
        [HttpGet]
        [Route(AppointMateAPIRoutes.UserServicesRoute)]
        public async Task<ActionResult<IEnumerable<CustomerServiceResponseModel>>> GetUserServicesAsync([FromRoute] string userId, CancellationToken cancellationToken = default) 
            => (await DI.UsersRepository.GetUserServicesAsync(userId.ToObjectId())).ToActionResult(x => x.Select(y => y.ToResponseModel()));

        /// <summary>
        /// Gets the user service with the specified <paramref name="id"/>
        /// </summary>
        /// <param name="id">The id</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns></returns>
        [HttpGet]
        [Route(AppointMateAPIRoutes.UserServiceRoute)]
        public async Task<ActionResult<CustomerServiceResponseModel>?> GetUserServiceAsync([FromRoute] string id, CancellationToken cancellationToken = default)
            => await ControllerHelpers.GetAsync(AppointMateDbMapper.CustomerServices, x => x.Id.ToString() == id, x => x.ToResponseModel(), cancellationToken);

        #endregion

        #region Favorite Companies

        /// <summary>
        /// Adds as favorite the company with the specified <paramref name="companyId"/> to the user with the specified <paramref name="id"/>
        /// </summary>
        /// <param name="id">The id</param>
        /// <param name="companyId">The company id</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns></returns>
        [HttpPost]
        [Route(AppointMateAPIRoutes.UserFavoriteCompaniesRoute)]
        public async Task<ActionResult<bool>> AddFavoriteCompanyAsync([FromRoute] string id, [FromBody] string companyId, CancellationToken cancellationToken = default)
        {
            var entity = await DI.UsersRepository.AddUserFavoriteCompanyAsync(id.ToObjectId(), companyId.ToObjectId());

            return entity.ToActionResult(x => true);
        }

        /// <summary>
        /// Gets the favorite companies of the user with the specified <paramref name="id"/>
        /// </summary>
        /// <param name="id">The user id</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns></returns>
        [HttpGet]
        [Route(AppointMateAPIRoutes.UserFavoriteCompaniesRoute)]
        public async Task<ActionResult<IEnumerable<CompanyResponseModel>>?> GetUserFavoriteCompaniesAsync([FromRoute] string id, CancellationToken cancellationToken = default)
        {
            // Get the user favorite company with the specified user id
            var favorites = await AppointMateDbMapper.UserFavoriteCompanies.SelectAsync(x => x.UserId.ToString() == id);

            // If no favorite company is found...
            if (favorites is null)
                return NotFound();

            var companies = await AppointMateDbMapper.Companies.SelectAsync(x => favorites.Any(y => y.CompanyId == x.Id));
            
            return new OkObjectResult(companies.Select(x => x.ToResponseModel()));
        }

        /// <summary>
        /// Gets the favorite company with the specified <paramref name="id"/>
        /// </summary>
        /// <param name="id">The id</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns></returns>
        [HttpGet]
        [Route(AppointMateAPIRoutes.UserFavoriteCompanyRoute)]
        public async Task<ActionResult<CompanyResponseModel>?> GetUserFavoriteCompanyAsync([FromRoute] string id, CancellationToken cancellationToken = default)
        {
            // Get the user favorite company with the specified id
            var favorite = await AppointMateDbMapper.UserFavoriteCompanies.FirstOrDefaultAsync(x => x.Id.ToString() == id);

            // If the favorite company is not found...
            if (favorite is null)
                return NotFound();

            var company = await AppointMateDbMapper.Companies.FirstOrDefaultAsync(x => x.Id == favorite.CompanyId);

            return new OkObjectResult(company.ToResponseModel());
        }

        /// <summary>
        /// Deletes the favorite company with the specified <paramref name="id"/>
        /// </summary>
        /// <param name="id">The id</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns></returns>
        [HttpDelete]
        [Route(AppointMateAPIRoutes.UserFavoriteCompanyRoute)]
        public async Task<ActionResult<CompanyResponseModel>?> DeleteUserFavoriteCompanyAsync([FromRoute] string id, CancellationToken cancellationToken = default)
        {
            var response = await DI.UsersRepository.DeleteUserFavoriteCompanyAsync(id.ToObjectId());

            if (!response.IsSuccessful || response.Result is null)
                return StatusCode(response.StatusCode ?? 400, response);

            var company = await GetCompanyAsync(response.Result.CompanyId.ToString());

            return company;
        }

        #endregion

        #endregion

        #endregion
    }
}
