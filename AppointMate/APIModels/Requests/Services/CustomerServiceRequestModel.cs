namespace AppointMate
{
    /// <summary>
    /// The customer service response model
    /// </summary>
    public class CustomerServiceRequestModel : BaseRequestModel
    {
        #region Public Properties

        /// <summary>
        /// The purchased amount
        /// </summary>
        public decimal? PurchasedAmount { get; set; }

        /// <summary>
        /// The amount that's paid by the customer
        /// </summary>
        public decimal? PaidAmount { get; set; }

        /// <summary>
        /// The amount that is owed by the customer
        /// </summary>
        public decimal? OwedAmount { get; set; }

        /// <summary>
        /// A flag indicating whether the subscription's amount is fully paid or not
        /// </summary>
        public bool? HasOwed { get; set; }

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
        public IEnumerable<CustomerServicePaymentRequestModel>? Payments { get; set; }

        /// <summary>
        /// The scheduled payments
        /// </summary>
        public IEnumerable<CustomerServiceScheduledPaymentRequestModel>? ScheduledPayments { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public CustomerServiceRequestModel() : base()
        {

        }

        #endregion
    }

    /// <summary>
    /// The embedded customer service response model
    /// </summary>
    public class EmbeddedCustomerServiceRequestModel : BaseEmbeddedRequestModel
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
        public EmbeddedCustomerServiceRequestModel() : base()
        {

        }

        #endregion
    }
}
