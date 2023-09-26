using MeetBase;
using MeetBase.Web;

using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

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
        /// <returns></returns>
        public async Task<WebServerFailable<UserEntity>> AddUserAsync(UserRequestModel model)
        {
            if(model.Username.IsNullOrEmpty()
            || model.FirstName.IsNullOrEmpty()
            || model.LastName.IsNullOrEmpty()
            || model.Email.IsNullOrEmpty()
            || model.PasswordHash.IsNullOrEmpty()
            || model.PhoneNumber is null
            || model.DateOfBirth is null)
                return MeetEduWebServerConstants.InvalidRegistrationCredentialsErrorMessage;

            return await MeetEduDbMapper.Users.AddAsync(UserEntity.FromRequestModel(model));
        }

        /// <summary>
        /// Updates the user with the specified <paramref name="id"/>
        /// </summary>
        /// <param name="id">The id</param>
        /// <param name="model">The model</param>
        /// <returns></returns>
        public async Task<WebServerFailable<UserEntity>> UpdateUserAsync(ObjectId id, UserRequestModel model)
        {
            var entity = await MeetEduDbMapper.Users.UpdateAsync(id, model);

            if (entity is null)
                return WebServerFailable.NotFound(id, nameof(MeetEduDbMapper.Users));

            var secretary = await MeetEduDbMapper.Secretaries.FirstOrDefaultAsync(x => x.UserId == entity.Id);
            
            // If the user is a secretary...
            if (secretary != null)
            {
                secretary.User = entity.ToEmbeddedEntity();
                await MeetEduDbMapper.Secretaries.UpdateAsync(secretary);
             
                return entity;
            }

            var professor = await MeetEduDbMapper.Professors.FirstOrDefaultAsync(x => x.UserId == entity.Id);

            // If the user is a professor...
            if (professor != null)
            {
                professor.User = entity.ToEmbeddedEntity();
                await MeetEduDbMapper.Professors.UpdateAsync(professor);
                
                return entity;
            }

            var member = await MeetEduDbMapper.Members.FirstOrDefaultAsync(x => x.UserId == entity.Id);

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
        /// <returns></returns>
        public async Task<WebServerFailable<UserEntity>> DeleteUserAsync(ObjectId id)
        {
            // Gets the member
            var entity = await MeetEduDbMapper.Users.FirstOrDefaultAsync(x => x.Id == id);

            // If the member does not exist...
            if (entity is null)
                return WebServerFailable.NotFound(id, nameof(MeetEduDbMapper.Users));

            await MeetEduDbMapper.Users.DeleteOneAsync(x => x.Id == id);

            return entity;
        }

        #endregion
    }
}
