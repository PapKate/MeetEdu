using MongoDB.Bson;

namespace MeetEdu
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

        /// <summary>
        /// Adds a professor
        /// </summary>
        /// <param name="model">The model</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns></returns>
        public async Task<WebServerFailable<ProfessorEntity>> AddProfessorAsync(ProfessorRequestModel model, CancellationToken cancellationToken = default)
        {
            return await MeetEduDbMapper.Professors.AddAsync(await ProfessorEntity.FromRequestModelAsync(model), cancellationToken);
        }

        /// <summary>
        /// Updates the professor with the specified <paramref name="id"/>
        /// </summary>
        /// <param name="id">The id</param>
        /// <param name="professor">The professor</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns></returns>
        public async Task<WebServerFailable<ProfessorEntity>> UpdateProfessorAsync(ObjectId id, ProfessorRequestModel professor, CancellationToken cancellationToken = default)
        {
            var professorEntity = await MeetEduDbMapper.Professors.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

            // If the professor does not exist...
            if (professorEntity is null)
                return WebServerFailable.NotFound(id, nameof(MeetEduDbMapper.Professors));

            professorEntity = await MeetEduDbMapper.Professors.UpdateAsync(id, professor, cancellationToken);

            return professorEntity!;
        }

        /// <summary>
        /// Deletes the professor with the specified <paramref name="id"/>
        /// </summary>
        /// <param name="id">The id</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns></returns>
        public async Task<WebServerFailable<ProfessorEntity>> DeleteProfessorAsync(ObjectId id, CancellationToken cancellationToken = default)
        {
            // Gets the professor
            var entity = await MeetEduDbMapper.Professors.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

            // If the professor does not exist...
            if (entity is null)
                return WebServerFailable.NotFound(id, nameof(MeetEduDbMapper.Professors));

            // Removes the professor from the database
            await MeetEduDbMapper.Professors.DeleteAsync(id, cancellationToken);
            // Removes the user from the database
            await MeetEduDbMapper.Users.DeleteAsync(entity.UserId, cancellationToken);

            return entity;
        }

        #region Layouts

        /// <summary>
        /// Sets a professor office layout image
        /// </summary>
        /// <param name="layoutId">The professor id</param>
        /// <param name="file">The file</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns></returns>
        public async Task<WebServerFailable<ProfessorOfficeLayoutEntity>> SetProfessorOfficeLayoutImageAsync(ObjectId layoutId, IFormFile file, CancellationToken cancellationToken = default)
        {
            return await RepositoryHelpers.SetImageAsync<ProfessorOfficeLayoutRequestModel, ProfessorOfficeLayoutEntity>(
                                                layoutId,
                                                $"{MeetEduConstants.Professors.ToLower()}/{MeetEduConstants.Layouts.ToLower()}/",
                                                file,
                                                (model) => UpdateProfessorOfficeLayoutAsync(layoutId, model, cancellationToken),
                                                cancellationToken);
        }

        /// <summary>
        /// Add a professor office layout
        /// </summary>
        /// <param name="professorId">The professor id</param>
        /// <param name="model">The model</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns></returns>
        public async Task<WebServerFailable<ProfessorOfficeLayoutEntity>> AddProfessorOfficeLayoutAsync(ObjectId professorId, ProfessorOfficeLayoutRequestModel model, CancellationToken cancellationToken = default)
            => await MeetEduDbMapper.ProfessorOfficeLayouts.AddAsync(ProfessorOfficeLayoutEntity.FromRequestModel(model, professorId), cancellationToken);

        /// <summary>
        /// Updates the layout with the specified <paramref name="layoutId"/>
        /// </summary>
        /// <param name="layoutId">The layout id</param>
        /// <param name="model">The model</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns></returns>
        public async Task<WebServerFailable<ProfessorOfficeLayoutEntity>> UpdateProfessorOfficeLayoutAsync(ObjectId layoutId, ProfessorOfficeLayoutRequestModel model, CancellationToken cancellationToken)
        {
            var entity = await MeetEduDbMapper.ProfessorOfficeLayouts.UpdateAsync(layoutId, model, cancellationToken);

            if (entity is null)
                return WebServerFailable.NotFound(layoutId, nameof(MeetEduDbMapper.ProfessorOfficeLayouts));

            return entity;
        }

        /// <summary>
        /// Deletes the professor office layout with the specified <paramref name="layoutId"/>
        /// </summary>
        /// <param name="layoutId">The layout id</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns></returns>
        public async Task<WebServerFailable<ProfessorOfficeLayoutEntity>> DeleteProfessorOfficeLayoutAsync(ObjectId layoutId, CancellationToken cancellationToken = default)
            => await MeetEduDbMapper.ProfessorOfficeLayouts.DeleteAsync(layoutId, cancellationToken);

        #endregion

        #endregion
    }
}
