using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Core.Servers;
using MongoDB.Driver.Linq;

using System.ComponentModel.Design;

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
        /// <returns></returns>
        public async Task<WebServerFailable<CustomerServiceEntity>> AddCustomerServiceAsync(CustomerServiceRequestModel model)
        {
            // Create the customer service
            var result = await CustomerServiceEntity.FromRequestModelAsync(model);

            // If there was an error...
            if (!result.IsSuccessful || result.Result is null)
                // Return
                return result.ToUnsuccessfulWebServerFailable();

            return await AppointMateDbMapper.CustomerServices.AddAsync(result.Result);
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

            // TODO: Customer service payments and scheduled payments and sessions

            // Delete the customer service
            return await AppointMateDbMapper.CustomerServices.DeleteAsync(id);
        }

        #endregion
    }
}
