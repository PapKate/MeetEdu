using MongoDB.Bson;
using MongoDB.Driver;

namespace AppointMate
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
            return await AppointMateDbMapper.Members.AddAsync(await MemberEntity.FromRequestModelAsync(model));
        }

        /// <summary>
        /// Updates the member with the specified <paramref name="id"/>
        /// </summary>
        /// <param name="id">The id</param>
        /// <param name="model">The model</param>
        /// <returns></returns>
        public async Task<WebServerFailable<MemberEntity>> UpdateMemberAsync(ObjectId id, UserRequestModel model)
        {
            var entity = await AppointMateDbMapper.Members.FirstOrDefaultAsync(x => x.Id == id);

            // If the member does not exist...
            if (entity is null)
                return WebServerFailable.NotFound(id, nameof(AppointMateDbMapper.Members));

            var user = await AppointMateDbMapper.Users.UpdateAsync(id, model);
            
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
            var entity = await AppointMateDbMapper.Members.FirstOrDefaultAsync(x => x.Id == id);

            // If the member does not exist...
            if (entity is null)
                return WebServerFailable.NotFound(id, nameof(AppointMateDbMapper.Members));

            await AppointMateDbMapper.Members.DeleteAsync(id);

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
            var member = await AppointMateDbMapper.Members.FirstOrDefaultAsync(x => x.Id == memberId);

            // If the member does not exist...
            if (member is null)
                return WebServerFailable.NotFound(memberId, nameof(AppointMateDbMapper.Members));

            // Gets the professor with the specified id
            var department = await AppointMateDbMapper.Departments.FirstOrDefaultAsync(x => x.Id == departmentId);

            // If the professor does not exist...
            if (department is null)
                return WebServerFailable.NotFound(departmentId, nameof(AppointMateDbMapper.Departments));

            // Create the favorite professor
            var entity = new MemberSavedDepartmentEntity()
            {
                DepartmentId = departmentId,
                MemberId = memberId,
            };

            // Adds the favorite professor
            await AppointMateDbMapper.MemberSavedDepartments.AddAsync(entity);

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
            var entity = await AppointMateDbMapper.MemberSavedDepartments.FirstOrDefaultAsync(x => x.Id == id);

            if (entity is null)
                return WebServerFailable.NotFound(id, nameof(AppointMateDbMapper.MemberSavedDepartments));

            await AppointMateDbMapper.MemberSavedDepartments.DeleteAsync(id);

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
            var member = await AppointMateDbMapper.Members.FirstOrDefaultAsync(x => x.Id == memberId);

            // If the member does not exist...
            if (member is null)
                return WebServerFailable.NotFound(memberId, nameof(AppointMateDbMapper.Members));

            // Gets the professor with the specified id
            var professor = await AppointMateDbMapper.Professors.FirstOrDefaultAsync(x => x.Id == professorId);

            // If the professor does not exist...
            if (professor is null)
                return WebServerFailable.NotFound(professorId, nameof(AppointMateDbMapper.Professors));

            // Create the favorite professor
            var entity = new MemberSavedProfessorEntity()
            {
                ProfessorId = professorId,
                MemberId = memberId,
            };

            // Adds the favorite professor
            await AppointMateDbMapper.MemberSavedProfessors.AddAsync(entity);

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
            var entity = await AppointMateDbMapper.MemberSavedProfessors.FirstOrDefaultAsync(x => x.Id == id);

            if (entity is null)
                return WebServerFailable.NotFound(id, nameof(AppointMateDbMapper.MemberSavedProfessors));

            await AppointMateDbMapper.MemberSavedProfessors.DeleteAsync(id);

            return entity;
        }

        #endregion

        #endregion
    }
}
