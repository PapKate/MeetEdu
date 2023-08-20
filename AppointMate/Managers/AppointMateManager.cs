
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
        public async Task<DepartmentEntity> GetComanyAsync(string id, CancellationToken cancellationToken = default)
            => await ManagerHelpers.GetAsync(AppointMateDbMapper.Companies, Builders<DepartmentEntity>.Filter.Eq(x => x.Id, id.ToObjectId()), cancellationToken);

        #endregion

        #region Staff Members

        /// <summary>
        /// Gets the staff members
        /// </summary>
        /// <param name="args">The arguments</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns></returns>
        public async Task<IEnumerable<StaffMemberEntity>> GetStaffMembersAsync(CompanyRelatedAPIArgs? args, CancellationToken cancellationToken = default)
        {
            var filters = new List<FilterDefinition<StaffMemberEntity>>();

            if (args is not null)
                filters = CreateFilters<StaffMemberEntity>(args);

            var filter = Builders<StaffMemberEntity>.Filter.And(filters);

            return await ManagerHelpers.GetManyAsync(AppointMateDbMapper.StaffMembers, filter, args, cancellationToken);
        }

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
        public async Task<IEnumerable<MemberEntity>> GetCustomersAsync(string userId, CompanyRelatedAPIArgs? args, CancellationToken cancellationToken = default)
        {
            var filters = new List<FilterDefinition<MemberEntity>>();

            if (args is not null)
                filters = CreateFilters<MemberEntity>(args);
            
            filters.Add(Builders<MemberEntity>.Filter.Eq(x => x.UserId, userId.ToObjectId()));

            var filter = Builders<MemberEntity>.Filter.And(filters);

            return await ManagerHelpers.GetManyAsync(AppointMateDbMapper.Customers, filter, args, cancellationToken);
        }

        #endregion

        #region Services

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
        public async Task<AppointmentTemplateEntity> GetServiceAsync(string id, CancellationToken cancellationToken = default) 
            => await ManagerHelpers.GetAsync(AppointMateDbMapper.Services, Builders<AppointmentTemplateEntity>.Filter.Eq(x => x.Id, id.ToObjectId()), cancellationToken);

        #endregion

        #region Customer Services

        /// <summary>
        /// Gets the customer services 
        /// </summary>
        /// <param name="args">The arguments</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns></returns>
        public async Task<IEnumerable<AppointmentEntity>> GetCustomerServicesAsync(CustomerRelatedAPIArgs? args, CancellationToken cancellationToken = default)
        {
            var filters = new List<FilterDefinition<AppointmentEntity>>();

            // If there are arguments...
            if (args is not null)
                filters = CreateFilters<AppointmentEntity>(args);

            var filter = Builders<AppointmentEntity>.Filter.And(filters);

            return await ManagerHelpers.GetManyAsync(AppointMateDbMapper.CustomerServices, filter, args, cancellationToken);
        }

        /// <summary>
        /// Gets the customer service with the specified <paramref name="id"/>
        /// </summary>
        /// <param name="id">The id</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns></returns>
        public async Task<AppointmentEntity> GetCustomerServiceAsync(string id, CancellationToken cancellationToken = default) 
            => await ManagerHelpers.GetAsync(AppointMateDbMapper.CustomerServices, Builders<AppointmentEntity>.Filter.Eq(x => x.Id, id.ToObjectId()), cancellationToken);

        #endregion

        #region Sessions

        /// <summary>
        /// Gets the sessions 
        /// </summary>
        /// <param name="args">The arguments</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns></returns>
        public async Task<IEnumerable<CustomerServiceSessionEntity>> GetSessionsAsync(SessionAPIArgs? args, CancellationToken cancellationToken = default)
        {
            var filters = new List<FilterDefinition<CustomerServiceSessionEntity>>();

            // If there are arguments...
            if (args is not null)
            {
                filters = CreateFilters<CustomerServiceSessionEntity>(args);

                // If there is a specified session status...
                if (args.SessionStatus is not null)
                    filters.Add(Builders<CustomerServiceSessionEntity>.Filter.Eq(x => x.SessionStatus, args.SessionStatus));
            }

            var filter = Builders<CustomerServiceSessionEntity>.Filter.And(filters);

            return await ManagerHelpers.GetManyAsync(AppointMateDbMapper.CustomerServiceSessions, filter, x => x.DateAndTime, false, args, cancellationToken);
        }

        /// <summary>
        /// Gets the session with the specified <paramref name="id"/>
        /// </summary>
        /// <param name="id">The id</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns></returns>
        public async Task<CustomerServiceSessionEntity> GetSessionAsync(string id, CancellationToken cancellationToken = default)
            => await ManagerHelpers.GetAsync(AppointMateDbMapper.CustomerServiceSessions, Builders<CustomerServiceSessionEntity>.Filter.Eq(x => x.Id, id.ToObjectId()), cancellationToken);

        #endregion

        #region Notes

        /// <summary>
        /// Gets the notes
        /// </summary>
        /// <param name="args">The arguments</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns></returns>
        public async Task<IEnumerable<CustomerNoteEntity>> GetNotesAsync(CustomerRelatedAPIArgs? args, CancellationToken cancellationToken = default)
        {
            var filters = new List<FilterDefinition<CustomerNoteEntity>>();

            // If there are arguments...
            if (args is not null)
                filters = CreateFilters<CustomerNoteEntity>(args);
            
            var filter = Builders<CustomerNoteEntity>.Filter.And(filters);

            return await ManagerHelpers.GetManyAsync(AppointMateDbMapper.CustomerNotes, filter, args, cancellationToken);
        }

        #endregion

        #region Offset Point Logs

        /// <summary>
        /// Gets the point offset logs for the user with the specified <paramref name="userId"/>
        /// </summary>
        /// <param name="userId">The user id</param>
        /// <param name="args">The arguments</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns></returns>
        public async Task<IEnumerable<UserPointOffsetLogEntity>> GetPointOffsetLogsAsync(string userId, APIArgs? args, CancellationToken cancellationToken = default)
        {
            var filters = new List<FilterDefinition<UserPointOffsetLogEntity>>
            {
                Builders<UserPointOffsetLogEntity>.Filter.Eq(x => x.UserId, userId.ToObjectId())
            };

            var filter = Builders<UserPointOffsetLogEntity>.Filter.And(filters);

            return await ManagerHelpers.GetManyAsync(AppointMateDbMapper.CustomerPointOffsetLogs, filter, args, cancellationToken);
        }

        /// <summary>
        /// Gets the point offset logs for the user with the specified <paramref name="userId"/>
        /// </summary>
        /// <param name="userId">The user id</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns></returns>
        public async Task<long> GetPointOffsetLogsCountAsync(string userId, CancellationToken cancellationToken = default) 
            => await AppointMateDbMapper.CustomerPointOffsetLogs.CountDocumentsAsync(Builders<UserPointOffsetLogEntity>.Filter.Eq(x => x.UserId, userId.ToObjectId()), cancellationToken: cancellationToken);
        
        /// <summary>
        /// Gets the total point earned for the user with the specified <paramref name="userId"/>
        /// </summary>
        /// <param name="userId">The user id</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns></returns>
        public async Task<int> GetTotalPointsEarnedAsync(string userId, CancellationToken cancellationToken = default)
        {
            var pointOffsets = await AppointMateDbMapper.CustomerPointOffsetLogs.SelectAsync(x => x.IsPositive == true && x.UserId == userId.ToObjectId(), cancellationToken);

            return pointOffsets.Sum(x => x.Offset);
        }

        /// <summary>
        /// Gets the total point spent for the user with the specified <paramref name="userId"/>
        /// </summary>
        /// <param name="userId">The user id</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns></returns>
        public async Task<int> GetTotalPointsSpentAsync(string userId, CancellationToken cancellationToken = default)
        {
            var pointOffsets = await AppointMateDbMapper.CustomerPointOffsetLogs.SelectAsync(x => x.IsPositive == false && x.UserId == userId.ToObjectId(), cancellationToken);

            return pointOffsets.Sum(x => x.Offset);
        }

        #endregion

        #region Reviews

        /// <summary>
        /// Gets the reviews for the company with the specified <paramref name="companyId"/>
        /// </summary>
        /// <param name="companyId">The company id</param>
        /// <param name="args">The arguments</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns></returns>
        public async Task<IEnumerable<CustomerServiceReviewEntity>> GetReviewsAsync(string companyId, APIArgs? args, CancellationToken cancellationToken = default) 
            => await ManagerHelpers.GetManyAsync(AppointMateDbMapper.CustomerServiceReviews, Builders<CustomerServiceReviewEntity>.Filter.Eq(x => x.CompanyId, companyId.ToObjectId()), args, cancellationToken);

        /// <summary>
        /// Gets the reviews
        /// </summary>
        /// <param name="args">The arguments</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns></returns>
        public async Task<IEnumerable<CustomerServiceReviewEntity>> GetReviewsAsync(CustomerRelatedAPIArgs? args, CancellationToken cancellationToken = default)
        {
            var filters = new List<FilterDefinition<CustomerServiceReviewEntity>>();

            // If there are arguments...
            if (args is not null)
                filters = CreateFilters<CustomerServiceReviewEntity>(args);

            var filter = Builders<CustomerServiceReviewEntity>.Filter.And(filters);

            return await ManagerHelpers.GetManyAsync(AppointMateDbMapper.CustomerServiceReviews, filter, args, cancellationToken);
        }

        /// <summary>
        /// Gets the review with the specified <paramref name="id"/>
        /// </summary>
        /// <param name="id">The id</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns></returns>
        public async Task<CustomerServiceReviewEntity> GetCustomerReviewAsync(string id, CancellationToken cancellationToken = default)
            => await ManagerHelpers.GetAsync(AppointMateDbMapper.CustomerServiceReviews, Builders<CustomerServiceReviewEntity>.Filter.Eq(x => x.Id, id.ToObjectId()), cancellationToken);

        #endregion

        #region Favorite Companies

        /// <summary>
        /// Gets the customers that represent the user with the specified <paramref name="userId"/> in a company
        /// </summary>
        /// <param name="userId">The user id</param>
        /// <param name="args">The arguments</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns></returns>
        public async Task<IEnumerable<MemberSavedDepartmentEntity>> GetFavoriteCompaniesAsync(string userId, CompanyRelatedAPIArgs? args, CancellationToken cancellationToken = default)
            => await ManagerHelpers.GetManyAsync(AppointMateDbMapper.UserFavoriteCompanies, Builders<MemberSavedDepartmentEntity>.Filter.Eq(x => x.UserId, userId.ToObjectId()), args, cancellationToken);

        /// <summary>
        /// Gets the total number of favorite companies for the user with the specified <paramref name="userId"/>
        /// </summary>
        /// <param name="userId">The user id</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns></returns>
        public async Task<long> GetFavoriteCompaniesCountAsync(string userId, CancellationToken cancellationToken = default)
            => await AppointMateDbMapper.UserFavoriteCompanies.CountDocumentsAsync(Builders<MemberSavedDepartmentEntity>.Filter.Eq(x => x.UserId, userId.ToObjectId()), cancellationToken: cancellationToken);

        #endregion

        #region Payments

        /// <summary>
        /// Gets the total number of payments
        /// </summary>
        /// <param name="args">The arguments</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns></returns>
        public async Task<long> GetPaymentsCountAsync(PaymentAPIArgs? args, CancellationToken cancellationToken = default)
        {
            var filters = new List<FilterDefinition<CustomerServicePaymentEntity>>();

            // If there are arguments...
            if (args is not null)
                filters = CreateFilters<CustomerServicePaymentEntity>(args);

            var filter = Builders<CustomerServicePaymentEntity>.Filter.And(filters);

            return await AppointMateDbMapper.CustomerServicePayments.CountDocumentsAsync(filter, cancellationToken: cancellationToken);
        }

        /// <summary>
        /// Gets the payments
        /// </summary>
        /// <param name="args">The arguments</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns></returns>
        public async Task<IEnumerable<CustomerServicePaymentEntity>> GetReviewsAsync(PaymentAPIArgs? args, CancellationToken cancellationToken = default)
        {
            var filters = new List<FilterDefinition<CustomerServicePaymentEntity>>();

            // If there are arguments...
            if (args is not null)
            {
                filters = CreateFilters<CustomerServicePaymentEntity>(args);

                // If there is a limit to the payment methods to include...
                if (!args.IncludePaymentMethods.IsNullOrEmpty())
                {
                    var ids = args.IncludePaymentMethods.Select(x => x.ToObjectId()).ToList();
                    filters.Add(Builders<CustomerServicePaymentEntity>.Filter.In(x => x.PaymentMethod!.Source, ids));
                }

                // If there is a limit to the payment methods to exclude...
                if (!args.ExcludePaymentMethods.IsNullOrEmpty())
                {
                    var ids = args.ExcludePaymentMethods.Select(x => x.ToObjectId()).ToList();
                    filters.Add(Builders<CustomerServicePaymentEntity>.Filter.Nin(x => x.PaymentMethod!.Source, ids));
                }
            }

            var filter = Builders<CustomerServicePaymentEntity>.Filter.And(filters);

            return await ManagerHelpers.GetManyAsync(AppointMateDbMapper.CustomerServicePayments, filter, args, cancellationToken);
        }

        #endregion

        #endregion

        #region Private Methods

        /// <summary>
        /// Creates the filters for the specified <paramref name="args"/>
        /// </summary>
        /// <typeparam name="T">The document type</typeparam>
        /// <param name="args">The arguments</param>
        /// <returns></returns>
        private List<FilterDefinition<T>> CreateFilters<T>(CompanyRelatedAPIArgs args)
            where T : BaseEntity, IDepartmentIdentifiable<ObjectId>
        {
            var filters = new List<FilterDefinition<T>>();

            // If there is a limit to the companies to include...
            if (!args.IncludeCompanies.IsNullOrEmpty())
            {
                var ids = args.IncludeCompanies.Select(x => x.ToObjectId()).ToList();
                filters.Add(Builders<T>.Filter.In(x => x.DepartmentId, ids));
            }

            // If there is a limit to the companies to exclude...
            if (!args.ExcludeCompanies.IsNullOrEmpty())
            {
                var ids = args.ExcludeCompanies.Select(x => x.ToObjectId()).ToList();
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
        private List<FilterDefinition<T>> CreateFilters<T>(CustomerRelatedAPIArgs args)
            where T : BaseEntity, IDepartmentIdentifiable<ObjectId>, ICustomerIdentifiable<ObjectId>
        {
            var filters = new List<FilterDefinition<T>>();

            filters.AddRange(CreateFilters<T>((CompanyRelatedAPIArgs)args));

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

        /// <summary>
        /// Creates the filters for the specified <paramref name="args"/>
        /// </summary>
        /// <param name="args">The arguments</param>
        /// <returns></returns>
        private List<FilterDefinition<CustomerServicePaymentEntity>> CreateFilters(PaymentAPIArgs args)
        {
            var filters = new List<FilterDefinition<CustomerServicePaymentEntity>>();

            filters.AddRange(CreateFilters<CustomerServicePaymentEntity>(args));

            // If there is a limit to the payment methods to include...
            if (!args.IncludePaymentMethods.IsNullOrEmpty())
            {
                var ids = args.IncludePaymentMethods.Select(x => x.ToObjectId()).ToList();
                filters.Add(Builders<CustomerServicePaymentEntity>.Filter.In(x => x.PaymentMethod!.Source, ids));
            }

            // If there is a limit to the payment methods to exclude...
            if (!args.ExcludePaymentMethods.IsNullOrEmpty())
            {
                var ids = args.ExcludePaymentMethods.Select(x => x.ToObjectId()).ToList();
                filters.Add(Builders<CustomerServicePaymentEntity>.Filter.Nin(x => x.PaymentMethod!.Source, ids));
            }

            return filters;
        }

        #endregion
    }
}
