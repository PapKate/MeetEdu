
using Amazon.SecurityToken.Model;

using AppointMate.Helpers;

using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

using System.Linq;

namespace AppointMate
{
    /// <summary>
    /// Provides methods for managing the app data
    /// </summary>
    public class AppointMateManager
    {
        #region Public Properties

        /// <summary>
        /// The single instance of the <see cref="AppointMateManager"/>
        /// </summary>
        public static AppointMateManager Instance { get; } = new AppointMateManager();

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public AppointMateManager() : base()
        {

        }

        #endregion

        #region Public Methods

        #region Users

        /// <summary>
        /// Gets the user with the specified <paramref name="id"/>
        /// </summary>
        /// <param name="id">The id</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns></returns>
        public async Task<UserEntity> GetUserAsync(string id, CancellationToken cancellationToken = default)
            => await ManagerHelpers.GetAsync(AppointMateDbMapper.Users, Builders<UserEntity>.Filter.Eq(x => x.Id, id.ToObjectId()), cancellationToken);

        #endregion

        #region Companies

        /// <summary>
        /// Gets the department with the specified <paramref name="id"/>
        /// </summary>
        /// <param name="id">The id</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns></returns>
        public async Task<DepartmentEntity> GetDepartmentAsync(string id, CancellationToken cancellationToken = default)
            => await ManagerHelpers.GetAsync(AppointMateDbMapper.Departments, Builders<DepartmentEntity>.Filter.Eq(x => x.Id, id.ToObjectId()), cancellationToken);

        #endregion

        #region Professors

        /// <summary>
        /// Gets the professor
        /// </summary>
        /// <param name="args">The arguments</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns></returns>
        public async Task<IEnumerable<ProfessorEntity>> GetStaffMembersAsync(DepartmentRelatedAPIArgs? args, CancellationToken cancellationToken = default)
        {
            var filters = new List<FilterDefinition<ProfessorEntity>>();

            if (args is not null)
                filters = CreateFilters<ProfessorEntity>(args);

            var filter = Builders<ProfessorEntity>.Filter.And(filters);

            return await ManagerHelpers.GetManyAsync(AppointMateDbMapper.Professors, filter, args, cancellationToken);
        }

        /// <summary>
        /// Gets the professor with the specified <paramref name="id"/>
        /// </summary>
        /// <param name="id">The id</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns></returns>
        public async Task<ProfessorEntity> GetStaffMemberAsync(string id, CancellationToken cancellationToken = default)
            => await ManagerHelpers.GetAsync(AppointMateDbMapper.Professors, Builders<ProfessorEntity>.Filter.Eq(x => x.Id, id.ToObjectId()), cancellationToken);

        #endregion

        #region Favorite Departments

        /// <summary>
        /// Gets the saved departments for the member with the specified <paramref name="memberId"/> 
        /// </summary>
        /// <param name="memberId">The member id</param>
        /// <param name="args">The arguments</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns></returns>
        public async Task<IEnumerable<MemberSavedDepartmentEntity>> GetMemberSavedDepartmentsAsync(string memberId, DepartmentRelatedAPIArgs? args, CancellationToken cancellationToken = default)
            => await ManagerHelpers.GetManyAsync(AppointMateDbMapper.MemberSavedDepartments, Builders<MemberSavedDepartmentEntity>.Filter.Eq(x => x.MemberId, memberId.ToObjectId()), args, cancellationToken);

        /// <summary>
        /// Gets the saved departments count for the member with the specified <paramref name="memberId"/> 
        /// </summary>
        /// <param name="memberId">The user id</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns></returns>
        public async Task<long> GetMemberSavedDepartmentsCountAsync(string memberId, CancellationToken cancellationToken = default)
            => await AppointMateDbMapper.MemberSavedDepartments.CountDocumentsAsync(Builders<MemberSavedDepartmentEntity>.Filter.Eq(x => x.MemberId, memberId.ToObjectId()), cancellationToken: cancellationToken);

        #endregion

        #endregion

        #region Private Methods

        /// <summary>
        /// Creates the filters for the specified <paramref name="args"/>
        /// </summary>
        /// <typeparam name="T">The document type</typeparam>
        /// <param name="args">The arguments</param>
        /// <returns></returns>
        private List<FilterDefinition<T>> CreateFilters<T>(DepartmentRelatedAPIArgs args)
            where T : BaseEntity, IDepartmentIdentifiable<ObjectId>
        {
            var filters = new List<FilterDefinition<T>>();

            // If there is a limit to the companies to include...
            if (!args.IncludeDepartments.IsNullOrEmpty())
            {
                var ids = args.IncludeDepartments.Select(x => x.ToObjectId()).ToList();
                filters.Add(Builders<T>.Filter.In(x => x.DepartmentId, ids));
            }

            // If there is a limit to the companies to exclude...
            if (!args.ExcludeDepartments.IsNullOrEmpty())
            {
                var ids = args.ExcludeDepartments.Select(x => x.ToObjectId()).ToList();
                filters.Add(Builders<T>.Filter.Nin(x => x.DepartmentId, ids));
            }

            return filters;
        }

        /// <summary>
        /// Creates the filters for the specified <paramref name="args"/>
        /// </summary>
        /// <typeparam name="T">The document type</typeparam>
        /// <param name="args">The arguments</param>
        /// <returns></returns>
        private List<FilterDefinition<T>> CreateFilters<T>(MemberRelatedAPIArgs args)
            where T : BaseEntity, IDepartmentIdentifiable<ObjectId>, ICustomerIdentifiable<ObjectId>
        {
            var filters = new List<FilterDefinition<T>>();

            filters.AddRange(CreateFilters<T>((DepartmentRelatedAPIArgs)args));

            // If there is a limit to the customers to include...
            if (!args.IncludeCustomers.IsNullOrEmpty())
            {
                var ids = args.IncludeCustomers.Select(x => x.ToObjectId()).ToList();
                filters.Add(Builders<T>.Filter.In(x => x.CustomerId, ids));
            }

            // If there is a limit to the customers to exclude...
            if (!args.ExcludeCustomers.IsNullOrEmpty())
            {
                var ids = args.ExcludeCustomers.Select(x => x.ToObjectId()).ToList();
                filters.Add(Builders<T>.Filter.Nin(x => x.CustomerId, ids));
            }

            return filters;
        }

        #endregion
    }
}
