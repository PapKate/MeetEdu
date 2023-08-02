using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;


namespace AppointMate
{
    /// <summary>
    /// Provides methods for managing companies
    /// </summary>
    public class CustomerServicesRepository
    {
        #region Public Properties

        /// <summary>
        /// The single instance of the <see cref="CustomerServicesRepository"/>
        /// </summary>
        public static CustomerServicesRepository Instance { get; } = new CustomerServicesRepository();

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public CustomerServicesRepository() : base()
        {

        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Adds a customer service
        /// </summary>
        /// <param name="model">The model</param>
        /// <param name="session">The session model</param>
        /// <returns></returns>
        public async Task<WebServerFailable<AddCustomerServiceResult>> AddCustomerServiceAsync(CustomerServiceRequestModel model, CustomerServiceSessionRequestModel session)
        {
            // If there is no session...
            if (session is null)
                // Return error message
                return AppointMateWebServerConstants.NoSessionWasCreatedErrorMessage;

            // Create the customer service
            var result = await CustomerServiceEntity.FromRequestModelAsync(model);

            // If there was an error...
            if (!result.IsSuccessful || result.Result is null)
                // Return
                return result.ToUnsuccessfulWebServerFailable();
            
            await AppointMateDbMapper.CustomerServices.AddAsync(result.Result);
            
            var sessionEntity = CustomerServiceSessionEntity.FromRequestModel(session);
            sessionEntity.Index = 1;

            // Adds the first session
            await AppointMateDbMapper.CustomerServiceSessions.AddAsync(sessionEntity);

            return new AddCustomerServiceResult(result.Result, sessionEntity);
        }

        /// <summary>
        /// Updates the customer service with the specified <paramref name="id"/>
        /// </summary>
        /// <param name="id">The id</param>
        /// <param name="model">The model</param>
        /// <returns></returns>
        public async Task<WebServerFailable<CustomerServiceEntity>> UpdateCustomerServiceAsync(ObjectId id, CustomerServiceRequestModel model)
        {
            var customerService = await AppointMateDbMapper.CustomerServices.FirstOrDefaultAsync(x => x.Id == id);

            // If the customer service does not exist...
            if(customerService is null)
                // Return
                return WebServerFailable.NotFound(id, nameof(AppointMateDbMapper.CustomerServices));

            var amount = model.PurchasedAmount ?? customerService.Amount;

            // If the amount of the customer service should be updated and the new amount that is requested to be set to the service is less than the paid amount...
            if (model.PurchasedAmount is not null && amount < customerService.PaidAmount)
                // Return
                return AppointMateWebServerConstants.TheCustomerServicePurchasedAmountCanNotBeSetToLessThanThePaidAmountErrorMessage;

            // If the amount of the customer service should be updated and the new amount that is requested to be set to the service is less than the amount of the scheduled payments...
            if (model.PurchasedAmount is not null && !customerService.ScheduledPayments.IsNullOrEmpty() && amount < customerService.ScheduledPayments.Sum(x => x.Amount))
                // Return
                return AppointMateWebServerConstants.TheCustomerServicePurchasedAmountCanNotBeSetToLessThanTheAmountOfTheScheduledPaymentsErrorMessage;

            // Keep the previous amount
            var previousAmount = customerService.PurchasedAmount;

            // Update the customer service
            DI.Mapper.Map(model, customerService);

            // Update the customer service
            return await AppointMateDbMapper.CustomerServices.UpdateAsync(customerService);
        }

        /// <summary>
        /// Deletes a customer service with the specified <paramref name="id"/>
        /// </summary>
        /// <param name="id">The id</param>
        /// <returns></returns>
        public async Task<WebServerFailable<CustomerServiceEntity>> DeleteCustomerServiceAsync(ObjectId id)
        {
            var customerService = await AppointMateDbMapper.CustomerServices.FirstOrDefaultAsync(x => x.Id == id);

            // If the customer service does not exist...
            if (customerService is null)
                // Return
                return WebServerFailable.NotFound(id, nameof(AppointMateDbMapper.CustomerServices));

            // If the service has already started...
            if(customerService.DateStart < DateTimeOffset.Now)
            {
                // Gets the future scheduled payments
                var scheduledPayments = customerService.ScheduledPayments.Where(x => x.DateScheduled > DateTimeOffset.Now).ToList();
                // Deletes them
                await AppointMateDbMapper.CustomerServiceScheduledPayments.DeleteManyAsync(x => scheduledPayments.Any(y => y == x));

                // Calculates the canceled amount
                customerService.CancelledAmount = scheduledPayments.Select(x => x.Amount).Sum();
                customerService.DateCanceled = DateTimeOffset.Now;
                customerService.IsCanceled = true;

                // Deletes the future sessions
                await AppointMateDbMapper.CustomerServiceSessions.DeleteManyAsync(x => x.UserId == customerService.CustomerId && x.ServiceId == customerService.ServiceId);
            }

            // Delete the customer service
            return await AppointMateDbMapper.CustomerServices.DeleteAsync(id);
        }

        #region Reviews

        /// <summary>
        /// Adds a customer service review
        /// </summary>
        /// <param name="model">The model</param>
        /// <returns></returns>
        public async Task<CustomerServiceReviewEntity> AddCustomerServiceReviewAsync(CustomerServiceReviewRequestModel model)
            => await AppointMateDbMapper.CustomerServiceReviews.AddAsync(CustomerServiceReviewEntity.FromRequestModel(model));

        /// <summary>
        /// Adds a list of customer service reviews 
        /// </summary>
        /// <param name="models">The models</param>
        /// <returns></returns>
        public async Task<WebServerFailable<IEnumerable<CustomerServiceReviewEntity>>> AddCustomerServiceReviewsAsync(IEnumerable<CustomerServiceReviewRequestModel> models)
            => new WebServerFailable<IEnumerable<CustomerServiceReviewEntity>>(await AppointMateDbMapper.CustomerServiceReviews.AddRangeAsync(models.Select(CustomerServiceReviewEntity.FromRequestModel).ToList()));

        /// <summary>
        /// Updates the customer service review with the specified <paramref name="id"/>
        /// </summary>
        /// <param name="id">The id</param>
        /// <param name="model">The model</param>
        /// <returns></returns>
        public async Task<WebServerFailable<CustomerServiceReviewEntity>> UpdateCustomerServiceReviewAsync(ObjectId id, CustomerServiceReviewRequestModel model)
        {
            var entity = await AppointMateDbMapper.CustomerServiceReviews.UpdateAsync(id, model);

            if (entity is null)
                return WebServerFailable.NotFound(id, nameof(AppointMateDbMapper.CustomerServiceReviews));

            return entity;
        }

        /// <summary>
        /// Deletes the customer service review with the specified <paramref name="id"/>
        /// </summary>
        /// <param name="id">The id</param>
        /// <returns></returns>
        public async Task<WebServerFailable<CustomerServiceReviewEntity>> DeleteCustomerServiceReviewAsync(ObjectId id)
        {
            var entity = await AppointMateDbMapper.CustomerServiceReviews.FirstOrDefaultAsync(x => x.Id == id);

            if (entity is null)
                return WebServerFailable.NotFound(id, nameof(AppointMateDbMapper.CustomerServiceReviews));

            await AppointMateDbMapper.CustomerServiceReviews.DeleteOneAsync(x => x.Id == id);

            return entity;
        }

        #endregion

        #region Sessions

        /// <summary>
        /// Adds a customer service session
        /// </summary>
        /// <param name="model">The model</param>
        /// <returns></returns>
        public async Task<CustomerServiceSessionEntity> AddCustomerServiceSessionAsync(CustomerServiceSessionRequestModel model)
            => await AppointMateDbMapper.CustomerServiceSessions.AddAsync(CustomerServiceSessionEntity.FromRequestModel(model));

        /// <summary>
        /// Adds a list of customer service sessions 
        /// </summary>
        /// <param name="models">The models</param>
        /// <returns></returns>
        public async Task<WebServerFailable<IEnumerable<CustomerServiceSessionEntity>>> AddCustomerServiceSessionsAsync(IEnumerable<CustomerServiceSessionRequestModel> models)
            => new WebServerFailable<IEnumerable<CustomerServiceSessionEntity>>(await AppointMateDbMapper.CustomerServiceSessions.AddRangeAsync(models.Select(CustomerServiceSessionEntity.FromRequestModel).ToList()));

        /// <summary>
        /// Updates the customer service session with the specified <paramref name="id"/>
        /// </summary>
        /// <param name="id">The id</param>
        /// <param name="model">The model</param>
        /// <returns></returns>
        public async Task<WebServerFailable<CustomerServiceSessionEntity>> UpdateCustomerServiceSessionAsync(ObjectId id, CustomerServiceSessionRequestModel model)
        {
            var entity = await AppointMateDbMapper.CustomerServiceSessions.UpdateAsync(id, model);

            if (entity is null)
                return WebServerFailable.NotFound(id, nameof(AppointMateDbMapper.CustomerServiceSessions));

            return entity;
        }

        /// <summary>
        /// Deletes the customer service session with the specified <paramref name="id"/>
        /// </summary>
        /// <param name="id">The id</param>
        /// <returns></returns>
        public async Task<WebServerFailable<CustomerServiceSessionEntity>> DeleteCustomerServiceSessionAsync(ObjectId id)
        {
            var entity = await AppointMateDbMapper.CustomerServiceSessions.FirstOrDefaultAsync(x => x.Id == id);

            if (entity is null)
                return WebServerFailable.NotFound(id, nameof(AppointMateDbMapper.CustomerServiceSessions));

            await AppointMateDbMapper.CustomerServiceSessions.DeleteOneAsync(x => x.Id == id);

            return entity;
        }
        #endregion

        #endregion
        
        #region Public Records
        
        /// <summary>
        /// The result of adding a service
        /// </summary>
        /// <param name="CustomerService"> The customer service </param>
        /// <param name="Session"> The session </param>
        public record AddCustomerServiceResult(CustomerServiceEntity CustomerService, CustomerServiceSessionEntity Session);

        #endregion
    }
}
