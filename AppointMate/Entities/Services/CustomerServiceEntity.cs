using AutoMapper;

using Microsoft.AspNetCore.Http;
using MongoDB.Bson;

using System.ComponentModel.Design;
using System.Net.Http;

using ZstdSharp.Unsafe;

namespace AppointMate
{
    /// <summary>
    /// Represents a customer service document in the MongoDB
    /// </summary>
    public class CustomerServiceEntity : DateEntity
    {
        #region Private Members

        /// <summary>
        /// The member of the <see cref="Payments"/> property
        /// </summary>
        private IEnumerable<CustomerServicePaymentEntity>? mPayments;

        /// <summary>
        /// The member of the <see cref="ScheduledPayments"/> property
        /// </summary>
        private IEnumerable<CustomerServiceScheduledPaymentEntity>? mScheduledPayments;

        #endregion

        #region Public Properties

        /// <summary>
        /// The amount
        /// </summary>
        public decimal Amount { get => PurchasedAmount - CancelledAmount; set { } }

        /// <summary>
        /// The purchased amount
        /// </summary>
        public decimal PurchasedAmount { get; set; }

        /// <summary>
        /// The amount that's paid by the customer
        /// </summary>
        public decimal PaidAmount { get => Payments?.Sum(x => x.Amount) ?? 0; set { } }

        /// <summary>
        /// The amount that is owed by the customer
        /// </summary>
        public decimal OwedAmount { get => IsCanceled ? 0 : PurchasedAmount - PaidAmount; set { } }

        /// <summary>
        /// A flag indicating whether the subscription's amount is fully paid or not
        /// </summary>
        public bool HasOwed { get => OwedAmount > 0; set { } }

        /// <summary>
        /// A flag indicating whether the subscription was canceled or not
        /// </summary>
        public bool IsCanceled { get; set; }

        /// <summary>
        /// A flag indicating whether the amount of the now canceled subscription has been fully returned to the customer
        /// or not
        /// </summary>
        public bool IsFullyRefunded
        {
            get => IsCanceled && PurchasedAmount != 0 && Amount <= 0;

            set { }
        }

        /// <summary>
        /// The canceled amount
        /// </summary>
        public decimal CancelledAmount { get; set; }

        /// <summary>
        /// The date the customer subscription was canceled
        /// </summary>
        public DateTimeOffset? DateCanceled { get; set; }

        /// <summary>
        /// The service
        /// </summary>
        public EmbeddedServiceEntity? Service { get; set; }

        /// <summary>
        /// The customer
        /// </summary>
        public EmbeddedCustomerEntity? Customer { get; set; }

        /// <summary>
        /// The payments
        /// </summary>
        public IEnumerable<CustomerServicePaymentEntity> Payments
        {
            get => mPayments ?? Enumerable.Empty<CustomerServicePaymentEntity>();
            set => mPayments = value;
        }

        /// <summary>
        /// The scheduled payments
        /// </summary>
        public IEnumerable<CustomerServiceScheduledPaymentEntity> ScheduledPayments
        {
            get => mScheduledPayments ?? Enumerable.Empty<CustomerServiceScheduledPaymentEntity>();
            set => mScheduledPayments = value;
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public CustomerServiceEntity() : base()
        {

        }

        #region Public Methods

        /// <summary>
        /// Creates and returns a <see cref="CustomerServiceEntity"/> from the specified <paramref name="model"/>
        /// </summary>
        /// <param name="model">The model</param>
        /// <returns></returns>
        public static async Task<WebServerFailable<CustomerServiceEntity>> FromRequestModelAsync(CustomerServiceRequestModel model)
        {
            // If no customer id was specified...
            if(model.CustomerId is null)
                return AppointMateWebServerConstants.NoCustomerIdSpecifiedInTheRequestErrorMessage;

            // If no service id was specified...
            if (model.ServiceId is null)
                return AppointMateWebServerConstants.NoServiceIdSpecifiedInTheRequestErrorMessage;

            // Gets the customer with the specified id
            var customer = await AppointMateDbMapper.Customers.FirstOrDefaultAsync(x => x.Id == model.CustomerId.ToObjectId());

            // If no customer is found...
            if (customer is null)
                // Return
                return WebServerFailable.NotFound(model.CustomerId, nameof(AppointMateDbMapper.Customers));

            // Gets the service with the specified id
            var service = await AppointMateDbMapper.Services.FirstOrDefaultAsync(x => x.Id == model.ServiceId.ToObjectId());

            var entity = new CustomerServiceEntity();

            DI.Mapper.Map(model, entity);

            entity.Customer = customer.ToEmbeddedEntity();
            entity.Service = service.ToEmbeddedEntity();

            // If there are payments...
            if (!model.Payments.IsNullOrEmpty())
            {
                var payments = new List<CustomerServicePaymentEntity>();

                // For every payment...
                foreach (var payment in model.Payments)
                    // Create and add the payment
                    payments.Add(await CustomerServicePaymentEntity.FromRequestModelAsync(payment));

                entity.Payments = payments;
            }

            // If there are scheduled payments...
            if (!model.ScheduledPayments.IsNullOrEmpty())
            {
                var scheduledPayments = new List<CustomerServiceScheduledPaymentEntity>();

                // For every scheduled payment...
                foreach (var schedulePayment in model.ScheduledPayments)
                    // Create and add the scheduled payment
                    scheduledPayments.Add(CustomerServiceScheduledPaymentEntity.FromRequestModel(schedulePayment));

                entity.ScheduledPayments = scheduledPayments;
            }

            entity.DateCreated = DateTime.UtcNow;
            entity.DateModified = DateTime.UtcNow;

            return entity;
        }

        /// <summary>
        /// Creates and returns a <see cref="CustomerServiceResponseModel"/> from the current <see cref="CustomerServiceEntity"/>
        /// </summary>
        /// <returns></returns>
        public CustomerServiceResponseModel ToResponseModel()
            => EntityHelpers.ToResponseModel<CustomerServiceResponseModel>(this);

        /// <summary>
        /// Creates and returns a <see cref="EmbeddedCustomerEntity"/> from the current <see cref="CustomerServiceEntity"/>
        /// </summary>
        /// <returns></returns>
        public EmbeddedCustomerServiceEntity ToEmbeddedEntity()
            => EntityHelpers.ToEmbeddedEntity<EmbeddedCustomerServiceEntity>(this);

        #endregion

        #endregion
    }

    /// <summary>
    /// A minimal version of the <see cref="CustomerServiceEntity"/> that contains the fields that are 
    /// more frequently used when embedding documents in the MongoDB
    /// </summary>
    public class EmbeddedCustomerServiceEntity : EmbeddedBaseEntity
    {
        #region Public Properties

        /// <summary>
        /// The service
        /// </summary>
        public EmbeddedServiceEntity? Service { get; set; }

        /// <summary>
        /// The customer
        /// </summary>
        public EmbeddedCustomerEntity? Customer { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public EmbeddedCustomerServiceEntity() : base()
        {

        }

        #endregion
    }
}
