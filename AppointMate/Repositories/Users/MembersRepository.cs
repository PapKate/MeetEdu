using MongoDB.Bson;
using MongoDB.Driver;

namespace MeetEdu
{
    /// <summary>
    /// Provides methods for managing members
    /// </summary>
    public class MembersRepository
    {
        #region Public Properties

        /// <summary>
        /// The single instance of the <see cref="MembersRepository"/>
        /// </summary>
        public static MembersRepository Instance { get; } = new MembersRepository();

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public MembersRepository() : base()
        {

        }

        #endregion

        #region Public Methods

        #region Members

        /// <summary>
        /// Adds a member
        /// </summary>
        /// <param name="model">The model</param>
        /// <returns></returns>
        public async Task<WebServerFailable<MemberEntity>> AddMemberAsync(MemberRequestModel model)
        {
            return await MeetEduDbMapper.Members.AddAsync(await MemberEntity.FromRequestModelAsync(model));
        }

        /// <summary>
        /// Updates the member with the specified <paramref name="id"/>
        /// </summary>
        /// <param name="id">The id</param>
        /// <param name="model">The model</param>
        /// <returns></returns>
        public async Task<WebServerFailable<MemberEntity>> UpdateMemberAsync(ObjectId id, UserRequestModel model)
        {
            var entity = await MeetEduDbMapper.Members.FirstOrDefaultAsync(x => x.Id == id);

            // If the member does not exist...
            if (entity is null)
                return WebServerFailable.NotFound(id, nameof(MeetEduDbMapper.Members));

            var user = await MeetEduDbMapper.Users.UpdateAsync(id, model);
            
            // If the user exists...
            if(user is not null)
                entity.User = user.ToEmbeddedEntity();
            
            return entity;
        }

        /// <summary>
        /// Deletes the member with the specified <paramref name="id"/>
        /// </summary>
        /// <param name="id">The id</param>
        /// <returns></returns>
        public async Task<WebServerFailable<MemberEntity>> DeleteMemberAsync(ObjectId id)
        {
            // Gets the member
            var entity = await MeetEduDbMapper.Members.FirstOrDefaultAsync(x => x.Id == id);

            // If the member does not exist...
            if (entity is null)
                return WebServerFailable.NotFound(id, nameof(MeetEduDbMapper.Members));

            await MeetEduDbMapper.Members.DeleteAsync(id);

            return entity;
        }

        #endregion

        #region Favorite Departments

        /// <summary>
        /// Adds the professor with the specified <paramref name="departmentId"/> as a saved professor to the member with the specified <paramref name="memberId"/>
        /// </summary>
        /// <param name="memberId">The member id</param>
        /// <param name="departmentId">The professor id</param>
        /// <returns></returns>
        public async Task<WebServerFailable<MemberSavedDepartmentEntity>> AddMemberSavedDpartmentAsync(ObjectId memberId, ObjectId departmentId)
        {
            // Get the member with the specified id
            var member = await MeetEduDbMapper.Members.FirstOrDefaultAsync(x => x.Id == memberId);

            // If the member does not exist...
            if (member is null)
                return WebServerFailable.NotFound(memberId, nameof(MeetEduDbMapper.Members));

            // Gets the professor with the specified id
            var department = await MeetEduDbMapper.Departments.FirstOrDefaultAsync(x => x.Id == departmentId);

            // If the professor does not exist...
            if (department is null)
                return WebServerFailable.NotFound(departmentId, nameof(MeetEduDbMapper.Departments));

            // Create the favorite professor
            var entity = new MemberSavedDepartmentEntity()
            {
                DepartmentId = departmentId,
                MemberId = memberId,
            };

            // Adds the favorite professor
            await MeetEduDbMapper.MemberSavedDepartments.AddAsync(entity);

            // Returns the entity
            return entity;
        }

        /// <summary>
        /// Deletes the member saved department with the specified <paramref name="id"/>
        /// </summary>
        /// <param name="id">The id</param>
        /// <returns></returns>
        public async Task<WebServerFailable<MemberSavedDepartmentEntity>> DeleteMemberSavedDepartmentAsync(ObjectId id)
        {
            var entity = await MeetEduDbMapper.MemberSavedDepartments.FirstOrDefaultAsync(x => x.Id == id);

            if (entity is null)
                return WebServerFailable.NotFound(id, nameof(MeetEduDbMapper.MemberSavedDepartments));

            await MeetEduDbMapper.MemberSavedDepartments.DeleteAsync(id);

            return entity;
        }

        #endregion

        #region Favorite Professors

        /// <summary>
        /// Adds the professor with the specified <paramref name="professorId"/> as a saved professor to the member with the specified <paramref name="memberId"/>
        /// </summary>
        /// <param name="memberId">The member id</param>
        /// <param name="professorId">The professor id</param>
        /// <returns></returns>
        public async Task<WebServerFailable<MemberSavedProfessorEntity>> AddMemberSavedProfessorAsync(ObjectId memberId, ObjectId professorId)
        {
            // Get the member with the specified id
            var member = await MeetEduDbMapper.Members.FirstOrDefaultAsync(x => x.Id == memberId);

            // If the member does not exist...
            if (member is null)
                return WebServerFailable.NotFound(memberId, nameof(MeetEduDbMapper.Members));

            // Gets the professor with the specified id
            var professor = await MeetEduDbMapper.Professors.FirstOrDefaultAsync(x => x.Id == professorId);

            // If the professor does not exist...
            if (professor is null)
                return WebServerFailable.NotFound(professorId, nameof(MeetEduDbMapper.Professors));

            // Create the favorite professor
            var entity = new MemberSavedProfessorEntity()
            {
                ProfessorId = professorId,
                MemberId = memberId,
            };

            // Adds the favorite professor
            await MeetEduDbMapper.MemberSavedProfessors.AddAsync(entity);

            // Returns the entity
            return entity;
        }

        /// <summary>
        /// Deletes the member saved professor with the specified <paramref name="id"/>
        /// </summary>
        /// <param name="id">The id</param>
        /// <returns></returns>
        public async Task<WebServerFailable<MemberSavedProfessorEntity>> DeleteMemberFavoriteProfessorAsync(ObjectId id)
        {
            var entity = await MeetEduDbMapper.MemberSavedProfessors.FirstOrDefaultAsync(x => x.Id == id);

            if (entity is null)
                return WebServerFailable.NotFound(id, nameof(MeetEduDbMapper.MemberSavedProfessors));

            await MeetEduDbMapper.MemberSavedProfessors.DeleteAsync(id);

            return entity;
        }

        #endregion

        #endregion
    }
}
