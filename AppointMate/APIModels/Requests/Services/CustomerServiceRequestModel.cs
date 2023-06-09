namespace AppointMate
{
    /// <summary>
    /// Request model used for a customer service
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
