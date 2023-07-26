using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace AppointMate
{
    /// <summary>
    /// Provides methods for managing users
    /// </summary>
    public class UsersRepository
    {
        #region Public Properties

        /// <summary>
        /// The single instance of the <see cref="UsersRepository"/>
        /// </summary>
        public static UsersRepository Instance { get; } = new UsersRepository();

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public UsersRepository() : base()
        {

        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Adds a user
        /// </summary>
        /// <param name="model">The model</param>
        /// <returns></returns>
        public async Task<WebServerFailable<UserEntity>> AddUserAsync(UserRequestModel model)
        {
            if (model.Username.IsNullOrEmpty()
            || model.FirstName.IsNullOrEmpty()
            || model.LastName.IsNullOrEmpty()
            || model.Email.IsNullOrEmpty()
            || model.PasswordHash.IsNullOrEmpty()
            || model.PhoneNumber is null
            || model.Billing is null
            || model.DateOfBirth is null)
                return AppointMateWebServerConstants.InvalidRegistrationCredentialsErrorMessage;

            return await AppointMateDbMapper.Users.AddAsync(UserEntity.FromRequestModel(model));
        }

        /// <summary>
        /// Updates the user with the specified <paramref name="id"/>
        /// </summary>
        /// <param name="id">The id</param>
        /// <param name="model">The model</param>
        /// <returns></returns>
        public async Task<WebServerFailable<UserEntity>> UpdateUserAsync(ObjectId id, UserRequestModel model)
        {
            var entity = await AppointMateDbMapper.Users.UpdateAsync(id, model);

            if (entity is null)
                return WebServerFailable.NotFound(id, nameof(AppointMateDbMapper.Users));

            return entity;
        }

        /// <summary>
        /// Deletes the company with the specified <paramref name="id"/>
        /// </summary>
        /// <param name="id">The company id</param>
        /// <returns></returns>
        public async Task<WebServerFailable<UserEntity>> DeleteCompanyAsync(ObjectId id)
        {
            var entity = await AppointMateDbMapper.Users.FirstOrDefaultAsync(x => x.Id == id);

            if (entity is null)
                return WebServerFailable.NotFound(id, nameof(AppointMateDbMapper.Users));

            await AppointMateDbMapper.Users.DeleteOneAsync(x => x.Id == id);

            return entity;
        }

        #region Favorite Companies

        /// <summary>
        /// Adds a favorite company to the user with the specified <paramref name="userId"/>
        /// </summary>
        /// <param name="userId">The user id</param>
        /// <param name="companyId">The comapny id</param>
        /// <returns></returns>
        public async Task<WebServerFailable<UserFavoriteCompanyEntity>> AddUserFavoriteCompanyAsync(ObjectId userId, ObjectId companyId)
        {
            // Get the user with the specified id
            var user = await AppointMateDbMapper.Users.FirstOrDefaultAsync(x => x.Id == userId);

            // If the user does not exist...
            if (user is null)
                return WebServerFailable.NotFound(userId, nameof(AppointMateDbMapper.Users));

            // Gets the company with the specified id
            var company = await AppointMateDbMapper.Companies.FirstOrDefaultAsync(x => x.Id == companyId);

            // If the company does not exist...
            if (company is null)
                return WebServerFailable.NotFound(companyId, nameof(AppointMateDbMapper.Companies));

            // Create the favorite company
            var entity = new UserFavoriteCompanyEntity()
            {
                CompanyId = companyId,
                UserId = userId,
                Comppany = company.ToEmbeddedEntity()
            };

            // Adds the favorite company
            await AppointMateDbMapper.UserFavoriteCompanies.AddAsync(entity);

            // Returns the entity
            return entity;
        }

        /// <summary>
        /// Adds a list of favorite companies to the user with the specified <paramref name="userId"/>
        /// </summary>
        /// <param name="userId">The user id</param>
        /// <param name="companyIds">The company ids</param>
        /// <returns></returns>
        public async Task<WebServerFailable<IEnumerable<UserFavoriteCompanyEntity>>> AddUserFavoriteCompaniesAsync(ObjectId userId, IEnumerable<ObjectId> companyIds)
        {
            // Get the user with the specified id
            var user = await AppointMateDbMapper.Users.FirstOrDefaultAsync(x => x.Id == userId);

            // If the user does not exist...
            if (user is null)
                return WebServerFailable.NotFound(userId, nameof(AppointMateDbMapper.Users));

            // Gets the companies with the specified ids
            var companies = await AppointMateDbMapper.Companies.SelectAsync(x => companyIds.Any(y => y == x.Id));

            // If the company does not exist...
            if (companies is null)
                return AppointMateWebServerConstants.NoCompaniesWereFoundWithTheSpecifiedIdsErrorMessage;

            return new WebServerFailable<IEnumerable<UserFavoriteCompanyEntity>>(await AppointMateDbMapper.UserFavoriteCompanies.AddRangeAsync(companies.Select(x =>
                new UserFavoriteCompanyEntity()
                {
                    CompanyId = x.Id,
                    UserId = userId,
                    Comppany = x.ToEmbeddedEntity()
                })
            .ToList()));
        }

        /// <summary>
        /// Deletes the user favorite company with the specified <paramref name="id"/>
        /// </summary>
        /// <param name="id">The id</param>
        /// <returns></returns>
        public async Task<WebServerFailable<UserFavoriteCompanyEntity>> DeleteUserFavoriteCompanyAsync(ObjectId id)
        {
            var entity = await AppointMateDbMapper.UserFavoriteCompanies.FirstOrDefaultAsync(x => x.Id == id);

            if (entity is null)
                return WebServerFailable.NotFound(id, nameof(AppointMateDbMapper.UserFavoriteCompanies));

            await AppointMateDbMapper.UserFavoriteCompanies.DeleteOneAsync(x => x.Id == id);

            return entity;
        }

        #endregion

        #endregion
    }
}
