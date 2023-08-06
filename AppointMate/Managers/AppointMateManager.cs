
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
        /// Gets the company with the specified <paramref name="id"/>
        /// </summary>
        /// <param name="id">The id</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns></returns>
        public async Task<CompanyEntity> GetComanyAsync(string id, CancellationToken cancellationToken = default)
            => await ManagerHelpers.GetAsync(AppointMateDbMapper.Companies, Builders<CompanyEntity>.Filter.Eq(x => x.Id, id.ToObjectId()), cancellationToken);

        #endregion

        #region Staff Members

        /// <summary>
        /// Gets the staff members that belong to the company with the specified <paramref name="companyId"/>
        /// </summary>
        /// <param name="companyId">The id</param>
        /// <param name="args">The arguments</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns></returns>
        public async Task<IEnumerable<StaffMemberEntity>> GetStaffMembersAsync(string companyId, StandardAPIArgs? args, CancellationToken cancellationToken = default)
            => await ManagerHelpers.GetManyAsync(AppointMateDbMapper.StaffMembers, Builders<StaffMemberEntity>.Filter.Eq(x => x.CompanyId, companyId.ToObjectId()), args, cancellationToken);

        /// <summary>
        /// Gets the staff member with the specified <paramref name="id"/>
        /// </summary>
        /// <param name="id">The id</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns></returns>
        public async Task<StaffMemberEntity> GetStaffMemberAsync(string id, CancellationToken cancellationToken = default)
            => await ManagerHelpers.GetAsync(AppointMateDbMapper.StaffMembers, Builders<StaffMemberEntity>.Filter.Eq(x => x.Id, id.ToObjectId()), cancellationToken);

        #endregion

        #region Weekly Schedules

        /// <summary>
        /// Gets the weekly schedule for the staff member with the specified <paramref name="staffMemberId"/>
        /// </summary>
        /// <param name="staffMemberId">The id</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns></returns>
        public async Task<WeeklyScheduleEntity> GetStaffMemberWeeklyScheduleAsync(string staffMemberId, CancellationToken cancellationToken = default)
            => await ManagerHelpers.GetAsync(AppointMateDbMapper.StaffMemberWeeklySchedules, Builders<WeeklyScheduleEntity>.Filter.Eq(x => x.StaffMemberId, staffMemberId.ToObjectId()), x => x.DateModified, false, cancellationToken);

        #endregion

        #region Customers 

        /// <summary>
        /// Gets the customers that represent the user with the specified <paramref name="userId"/> in a company
        /// </summary>
        /// <param name="userId">The user id</param>
        /// <param name="args">The arguments</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns></returns>
        public async Task<IEnumerable<CustomerEntity>> GetCustomers(string userId, StandardAPIArgs? args, CancellationToken cancellationToken = default)
            => await ManagerHelpers.GetManyAsync(AppointMateDbMapper.Customers, Builders<CustomerEntity>.Filter.Eq(x => x.UserId, userId.ToObjectId()), args, cancellationToken);

        #endregion

        #region Services

        // Get Group by name services pagination = 3
        // Get Group by name services pagination = 4

        /// <summary>
        /// Gets the services grouped by name
        /// </summary>
        /// <param name="args">The arguments</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns></returns>
        public async Task<IEnumerable<ServiceCompaniesResult>> GetServicesByName(GroupedServiceAPIArgs args, CancellationToken cancellationToken = default)
        {
            var query = AppointMateDbMapper.Services.Aggregate()
                .Group(
                    x => x.Name,
                    g => new ServiceCompaniesResult
                    (
                        g.Key,
                        g.OrderBy(m => m.Price)
                    )
                )
                .Project(x => new ServiceCompaniesResult(x.Name, x.Services.Skip((args.InnerPage * args.InnerPerPage) + args.InnerOffset).Take(args.InnerPerPage)));

            // Limit the results
            query = query.Skip((args.Page * args.PerPage) + args.Offset).Limit(args.PerPage);

            return await query.ToListAsync(cancellationToken);
        }

        /// <summary>
        /// Gets the service with the specified <paramref name="id"/>
        /// </summary>
        /// <param name="id">The id</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns></returns>
        public async Task<ServiceEntity> GetServiceAsync(string id, CancellationToken cancellationToken = default) 
            => await ManagerHelpers.GetAsync(AppointMateDbMapper.Services, Builders<ServiceEntity>.Filter.Eq(x => x.Id, id.ToObjectId()), cancellationToken);

        #endregion

        #region Customer Services

        /// <summary>
        /// Gets the customer services 
        /// </summary>
        /// <param name="args">The arguments</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns></returns>
        public async Task<IEnumerable<CustomerServiceEntity>> GetCustomerServicesAsync(CustomerServiceAPIArgs? args, CancellationToken cancellationToken = default)
        {
            var filters = new List<FilterDefinition<CustomerServiceEntity>>();

            // If there are arguments...
            if (args is not null)
            {
                // If there is a limit to the companies to include...
                if (!args.IncludeCompanies.IsNullOrEmpty())
                {
                    var ids = args.IncludeCompanies.Select(x => x.ToObjectId()).ToList();
                    filters.Add(Builders<CustomerServiceEntity>.Filter.In(x => x.Service!.CompanyId, ids));
                }

                // If there is a limit to the companies to exclude...
                if (!args.ExcludeCompanies.IsNullOrEmpty())
                {
                    var ids = args.ExcludeCompanies.Select(x => x.ToObjectId()).ToList();
                    filters.Add(Builders<CustomerServiceEntity>.Filter.Nin(x => x.Service!.CompanyId, ids));
                }

                // If there is a limit to the customers to include...
                if (!args.IncludeCustomers.IsNullOrEmpty())
                {
                    var ids = args.IncludeCustomers.Select(x => x.ToObjectId()).ToList();
                    filters.Add(Builders<CustomerServiceEntity>.Filter.In(x => x.CustomerId, ids));
                }

                // If there is a limit to the customers to exclude...
                if (!args.ExcludeCustomers.IsNullOrEmpty())
                {
                    var ids = args.ExcludeCustomers.Select(x => x.ToObjectId()).ToList();
                    filters.Add(Builders<CustomerServiceEntity>.Filter.Nin(x => x.CustomerId, ids));
                }
            }

            var filter = Builders<CustomerServiceEntity>.Filter.And(filters);

            return await ManagerHelpers.GetManyAsync(AppointMateDbMapper.CustomerServices, filter, args, cancellationToken);
        }

        /// <summary>
        /// Gets the customer service with the specified <paramref name="id"/>
        /// </summary>
        /// <param name="id">The id</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns></returns>
        public async Task<CustomerServiceEntity> GetCustomerServiceAsync(string id, CancellationToken cancellationToken = default)
        {
            return await ManagerHelpers.GetAsync(AppointMateDbMapper.CustomerServices, Builders<CustomerServiceEntity>.Filter.Eq(x => x.Id, id.ToObjectId()), cancellationToken);
        }

        #endregion

        #region Sessions

        // Get service sessions by user id -> filter date
        // Get service sessions count by user id -> filter for status
        // Get service sessions by user id and service id

        #endregion

        #region Notes

        // Get notes by user id

        #endregion

        #region Offset Point Logs

        // Get offset point logs by user id

        // Get offset point logs total by user id
        // Get offset point logs total earned by user id
        // Get offset point logs total spend by user id

        #endregion

        #region Reviews

        // Gets customer reviews by company id -> Select all the reviews for each service of the company with the specified id and calculate average rating of company
        // Get customer reviews by customer id
        // Get customer reviews Count by customer id

        // Get customer review by id

        #endregion

        #region Favorite Companies

        // Get favorite companies by user id
        // Get favorite companies count by user id

        #endregion

        #region Payments

        // Get payments sum by customer id and payment method
        // Get payments count by customer id and payment method

        #endregion

        #endregion
    }
}
