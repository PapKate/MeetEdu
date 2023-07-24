namespace AppointMate
{
    /// <summary>
    /// Request model used for a customer service
    /// </summary>
    public class CustomerServiceRequestModel : BaseRequestModel
    {
        #region Public Properties

        /// <summary>
        /// The customer id
        /// </summary>
        public string? CustomerId { get; set; }

        /// <summary>
        /// The service id
        /// </summary>
        public string? ServiceId { get; set; }

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
        /// The date of the first or only session 
        /// NOTE: If the service has only one session the start and end date are the same!
        /// </summary>
        public DateTimeOffset DateStart { get; set; }

        /// <summary>
        /// The number of sessions
        /// </summary>
        public uint TotalSessions { get; set; }

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
}
