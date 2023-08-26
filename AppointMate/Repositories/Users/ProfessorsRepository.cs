using MongoDB.Bson;

namespace AppointMate
{
    /// <summary>
    /// Provides methods for managing professors
    /// </summary>
    public class ProfessorsRepository
    {
        #region Public Properties

        /// <summary>
        /// The single instance of the <see cref="ProfessorsRepository"/>
        /// </summary>
        public static ProfessorsRepository Instance { get; } = new ProfessorsRepository();

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public ProfessorsRepository() : base()
        {

        }

        #endregion

        #region Public Methods

        #region Professors

        /// <summary>
        /// Adds a professor
        /// </summary>
        /// <param name="model">The model</param>
        /// <returns></returns>
        public async Task<WebServerFailable<ProfessorEntity>> AddProfessorAsync(ProfessorRequestModel model)
        {
            return await AppointMateDbMapper.Professors.AddAsync(await ProfessorEntity.FromRequestModelAsync(model));
        }

        /// <summary>
        /// Updates the professor with the specified <paramref name="id"/>
        /// </summary>
        /// <param name="id">The id</param>
        /// <param name="model">The model</param>
        /// <returns></returns>
        public async Task<WebServerFailable<ProfessorEntity>> UpdateProfessorAsync(ObjectId id, UserRequestModel model)
        {
            var entity = await AppointMateDbMapper.Professors.FirstOrDefaultAsync(x => x.Id == id);

            // If the professor does not exist...
            if (entity is null)
                return WebServerFailable.NotFound(id, nameof(AppointMateDbMapper.Professors));

            var user = await AppointMateDbMapper.Users.UpdateAsync(id, model);

            // If the user exists...
            if (user is not null)
                entity.User = user.ToEmbeddedEntity();

            return entity;
        }

        /// <summary>
        /// Deletes the professor with the specified <paramref name="id"/>
        /// </summary>
        /// <param name="id">The id</param>
        /// <returns></returns>
        public async Task<WebServerFailable<ProfessorEntity>> DeleteProfessorAsync(ObjectId id)
        {
            // Gets the professor
            var entity = await AppointMateDbMapper.Professors.FirstOrDefaultAsync(x => x.Id == id);

            // If the professor does not exist...
            if (entity is null)
                return WebServerFailable.NotFound(id, nameof(AppointMateDbMapper.Professors));

            await AppointMateDbMapper.Professors.DeleteAsync(id);

            return entity;
        }

        #endregion

        #endregion
    }
}
