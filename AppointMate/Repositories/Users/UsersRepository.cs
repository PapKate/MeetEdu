using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Core.Servers;
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
        public async Task<UserEntity> AddUserAsync(UserRequestModel model)
            => await AppointMateDbMapper.Users.AddAsync(UserEntity.FromRequestModel(model));

        /// <summary>
        /// Adds a list of users
        /// </summary>
        /// <param name="models">The models</param>
        /// <returns></returns>
        public async Task<WebServerFailable<IEnumerable<UserEntity>>> AddUsersAsync(IEnumerable<UserRequestModel> models)
            => new WebServerFailable<IEnumerable<UserEntity>>(await AppointMateDbMapper.Users.AddRangeAsync(models.Select(UserEntity.FromRequestModel).ToList()));

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

        #endregion
    }
}
