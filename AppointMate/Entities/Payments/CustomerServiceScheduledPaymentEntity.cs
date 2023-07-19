using AutoMapper;
using MongoDB.Bson;
using System.ComponentModel;

namespace AppointMate
{
    /// <summary>
    /// Represents a customer service scheduled payment document in the MongoDB
    /// </summary>
    public class CustomerServiceScheduledPaymentEntity: DateEntity, INoteable, IPayable
    {
        #region Private Properties

        /// <summary>
        /// The member of the <see cref="Note"/> property
        /// </summary>
        private string? mNote;

        #endregion

        #region Public Properties

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
        /// The date scheduled
        /// </summary>
        public DateTimeOffset DateScheduled { get; set; }

        /// <summary>
        /// A flag indicating whether a the scheduled payment was paid or not
        /// </summary>
        public bool IsPaid { get; set; }

        /// <summary>
        /// The payment date if any
        /// </summary>
        public DateTimeOffset? DatePaid { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public CustomerServiceScheduledPaymentEntity() : base()
        {

        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Creates and returns a <see cref="CustomerServiceScheduledPaymentEntity"/> from the specified <paramref name="model"/>
        /// </summary>
        /// <param name="model">The model</param>
        /// <returns></returns>
        public static CustomerServiceScheduledPaymentEntity FromRequestModel(CustomerServiceScheduledPaymentRequestModel model)
        {
            var entity = EntityHelpers.FromRequestModel<CustomerServiceScheduledPaymentEntity>(model);

            UpdateNonAutoMapperValues(model, entity);

            return entity;
        }

        /// <summary>
        /// Updates the values of the specified <paramref name="entity"/> with the values of the specified <paramref name="model"/>.
        /// NOTE: This method only affects the properties that can't be mapped by the <see cref="Mapper"/> and are not null!
        /// </summary>
        /// <param name="model">The model</param>
        /// <param name="entity">The entity</param>
        /// <returns></returns>
        public static void UpdateNonAutoMapperValues(CustomerServiceScheduledPaymentRequestModel model, CustomerServiceScheduledPaymentEntity entity)
        {
            // If we should update the is paid flag...
            if (model.IsPaid is not null)
            {
                if (model.IsPaid == false)
                {
                    entity.DatePaid = null;
                }
                else
                {
                    if (entity.DatePaid is null)
                        entity.DatePaid = DateTimeOffset.Now;
                }
            }
        }

        /// <summary>
        /// Creates and returns a <see cref="CustomerServiceScheduledPaymentResponseModel"/> from the current <see cref="CustomerServiceScheduledPaymentEntity"/>
        /// </summary>
        /// <returns></returns>
        public CustomerServiceScheduledPaymentResponseModel ToResponseModel()
            => EntityHelpers.ToResponseModel<CustomerServiceScheduledPaymentResponseModel>(this);

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns></returns>
        public override string ToString() => Amount.ToLocalizedCurrency();

        #endregion
    }
}
