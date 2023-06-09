namespace AppointMate
{
    /// <summary>
    /// Represents a customer service document in the MongoDB
    /// </summary>
    public class CustomerServiceEntity : DateResponseModel
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
        /// The purchased amount
        /// </summary>
        public decimal PurchasedAmount { get; set; }

        /// <summary>
        /// The amount that's paid by the customer
        /// </summary>
        public decimal PaidAmount { get; set; }

        /// <summary>
        /// The amount that is owed by the customer
        /// </summary>
        public decimal OwedAmount { get; set; }

        /// <summary>
        /// A flag indicating whether the subscription's amount is fully paid or not
        /// </summary>
        public bool HasOwed { get; set; }

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

        #endregion
    }

    /// <summary>
    /// A minimal version of the <see cref="CustomerServiceEntity"/> that contains the fields that are 
    /// more frequently used when embedding documents in the MongoDB
    /// </summary>
    public class EmbeddedCustomerServiceEntity
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
