namespace AppointMate
{
    /// <summary>
    /// Represents a customer service 
    /// </summary>
    public class CustomerServiceResponseModel : DateResponseModel
    {
        #region Private Members

        /// <summary>
        /// The member of the <see cref="Payments"/> property
        /// </summary>
        private IEnumerable<CustomerServicePaymentResponseModel>? mPayments;

        /// <summary>
        /// The member of the <see cref="ScheduledPayments"/> property
        /// </summary>
        private IEnumerable<CustomerServiceScheduledPaymentResponseModel>? mScheduledPayments;

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
        public EmbeddedServiceResponseModel? Service { get; set; }

        /// <summary>
        /// The customer
        /// </summary>
        public EmbeddedCustomerResponseModel? Customer { get; set; }

        /// <summary>
        /// The payments
        /// </summary>
        public IEnumerable<CustomerServicePaymentResponseModel> Payments
        { 
            get => mPayments ?? Enumerable.Empty<CustomerServicePaymentResponseModel>();
            set => mPayments = value;
        }

        /// <summary>
        /// The scheduled payments
        /// </summary>
        public IEnumerable<CustomerServiceScheduledPaymentResponseModel> ScheduledPayments
        {
            get => mScheduledPayments ?? Enumerable.Empty<CustomerServiceScheduledPaymentResponseModel>();
            set => mScheduledPayments = value;
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public CustomerServiceResponseModel() : base()
        {

        }

        #endregion
    }

    /// <summary>
    /// A minimal version of the <see cref="CustomerServiceResponseModel"/> used when embedding
    /// </summary>
    public class EmbeddedCustomerServiceResponseModel : EmbeddedBaseResponseModel
    {
        #region Public Properties

        /// <summary>
        /// The service
        /// </summary>
        public EmbeddedServiceResponseModel? Service { get; set; }

        /// <summary>
        /// The customer
        /// </summary>
        public EmbeddedCustomerResponseModel? Customer { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public EmbeddedCustomerServiceResponseModel() : base()
        {

        }

        #endregion
    }
}
