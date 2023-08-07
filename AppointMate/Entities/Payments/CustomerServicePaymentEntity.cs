using AutoMapper;

using MongoDB.Bson;
using MongoDB.Driver;

using System;

namespace AppointMate
{
    /// <summary>
    /// Represents a customer service payment document in the MongoDB
    /// </summary>
    public class CustomerServicePaymentEntity: DateEntity, INoteable, IPayable, ICompanyIdentifiable<ObjectId>, ICustomerIdentifiable<ObjectId>
    {
        #region Private Methods

        /// <summary>
        /// The member of the <see cref="Note"/> property
        /// </summary>
        private string? mNote;

        #endregion

        #region Public Properties

        /// <summary>
        /// The company id
        /// </summary>
        public ObjectId CompanyId { get; set; }

        /// <summary>
        /// The customer id
        /// </summary>
        public ObjectId CustomerId { get; set; }

        /// <summary>
        /// The amount
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// The note
        /// </summary>
        public string Note
        {
            get => mNote ?? string.Empty;
            set => mNote = value;
        }

        /// <summary>
        /// The payment method
        /// </summary>
        public EmbeddedPaymentMethodEntity? PaymentMethod { get; set; }

        /// <summary>
        /// The customer service
        /// </summary>
        public EmbeddedCustomerServiceEntity CustomerService { get; set; }

        /// <summary>
        /// The sum of the flat rate and the percent commission
        /// </summary>
        public decimal CommissionAmount { get; set; }

        /// <summary>
        /// The amount accumulated by the flat rate commission
        /// </summary>
        public decimal FlatRateCommissionAmount { get; set; }

        /// <summary>
        /// The amount accumulated by the commission
        /// </summary>
        public decimal PercentCommissionAmount { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public CustomerServicePaymentEntity() : base()
        {

        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Creates and returns a <see cref="CustomerServicePaymentEntity"/> from the specified <paramref name="model"/>
        /// </summary>
        /// <param name="model">The model</param>
        /// <returns></returns>
        public static async Task<CustomerServicePaymentEntity> FromRequestModelAsync(CustomerServicePaymentRequestModel model)
        {
            var entity = EntityHelpers.FromRequestModel<CustomerServicePaymentEntity>(model);

            await UpdateNonAutoMapperValuesAsync(model, entity);

            return entity;
        }

        /// <summary>
        /// Updates the values of the specified <paramref name="entity"/> with the values of the specified <paramref name="model"/>.
        /// NOTE: This method only affects the properties that can't be mapped by the <see cref="Mapper"/> and are not null!
        /// </summary>
        /// <param name="model">The model</param>
        /// <param name="entity">The entity</param>
        /// <returns></returns>
        public static async Task UpdateNonAutoMapperValuesAsync(CustomerServicePaymentRequestModel model, CustomerServicePaymentEntity entity) 
            => await EntityHelpers.UpdateNonAutoMapperValueAsync(
                        model,
                        entity,
                        x => x.PaymentMethodId ?? string.Empty,
                        x => x.PaymentMethod,
                        AppointMateDbMapper.PaymentMethods.AsQueryable(),
                        x => x.ToEmbeddedEntity(entity.Amount));

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns></returns>
        public override string ToString() => Amount.ToLocalizedCurrency();

        #endregion
    }
}
