using MongoDB.Bson;
using MongoDB.Driver;

namespace MeetEdu
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
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns></returns>
        public async Task<WebServerFailable<UserEntity>> AddUserAsync(UserRequestModel model, CancellationToken cancellationToken = default)
        {
            if(model.Username.IsNullOrEmpty()
            || model.FirstName.IsNullOrEmpty()
            || model.LastName.IsNullOrEmpty()
            || model.Email.IsNullOrEmpty()
            || model.PasswordHash.IsNullOrEmpty()
            || model.PhoneNumber is null)
                return MeetEduWebServerConstants.InvalidRegistrationCredentialsErrorMessage;

            return await MeetEduDbMapper.Users.AddAsync(UserEntity.FromRequestModel(model), cancellationToken);
        }

        /// <summary>
        /// Updates the user with the specified <paramref name="id"/>
        /// </summary>
        /// <param name="id">The id</param>
        /// <param name="model">The model</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns></returns>
        public async Task<WebServerFailable<UserEntity>> UpdateUserAsync(ObjectId id, UserRequestModel model, CancellationToken cancellationToken = default)
        {
            var entity = await MeetEduDbMapper.Users.UpdateAsync(id, model);

            if (entity is null)
                return WebServerFailable.NotFound(id, nameof(MeetEduDbMapper.Users));

            var secretary = await MeetEduDbMapper.Secretaries.FirstOrDefaultAsync(x => x.UserId == entity.Id, cancellationToken);
            
            // If the user is a secretary...
            if (secretary != null)
            {
                secretary.User = entity.ToEmbeddedEntity();
                await MeetEduDbMapper.Secretaries.UpdateAsync(secretary);
             
                return entity;
            }

            var professor = await MeetEduDbMapper.Professors.FirstOrDefaultAsync(x => x.UserId == entity.Id, cancellationToken);

            // If the user is a professor...
            if (professor != null)
            {
                professor.User = entity.ToEmbeddedEntity();
                await MeetEduDbMapper.Professors.UpdateAsync(professor);
                
                return entity;
            }

            var member = await MeetEduDbMapper.Members.FirstOrDefaultAsync(x => x.UserId == entity.Id, cancellationToken);

            // If the user is a member...
            if (member != null)
            {
                member.User = entity.ToEmbeddedEntity();
                await MeetEduDbMapper.Members.UpdateAsync(member);
            }

            return entity;
        }

        /// <summary>
        /// Deletes the user with the specified <paramref name="id"/>
        /// </summary>
        /// <param name="id">The id</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns></returns>
        public async Task<WebServerFailable<UserEntity>> DeleteUserAsync(ObjectId id, CancellationToken cancellationToken = default)
        {
            // Gets the member
            var entity = await MeetEduDbMapper.Users.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

            // If the member does not exist...
            if (entity is null)
                return WebServerFailable.NotFound(id, nameof(MeetEduDbMapper.Users));

            await MeetEduDbMapper.Users.DeleteOneAsync(x => x.Id == id, cancellationToken);

            return entity;
        }

        /// <summary>
        /// Sets the image of the user with the specified <paramref name="id"/>
        /// </summary>
        /// <param name="id">The id</param>
        /// <param name="file">The file</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns></returns>
        public async Task<WebServerFailable<UserEntity>> SetUserImageAsync(ObjectId id, IFormFile file, CancellationToken cancellationToken = default)
        {
            return await RepositoryHelpers.SetImageAsync<UserRequestModel, UserEntity>(
                                                id,
                                                $"{MeetEduConstants.Users.ToLower()}/{file.Headers.First(x => x.Key == "userType").Value.ToString().ToLower()}/",
                                                file,
                                                (model) => UpdateUserAsync(id, model, cancellationToken),
                                                cancellationToken);
        }

        #endregion
    }
}
