namespace AppointMate
{
    /// <summary>
    /// Request model used for a customer service scheduled payment
    /// </summary>
    public class CustomerServiceScheduledPaymentRequestModel : BaseRequestModel
    {
        #region Public Properties

        /// <summary>
        /// The amount
        /// </summary>
        public decimal? Amount { get; set; }

        /// <summary>
        /// The note
        /// </summary>
        public string? Note { get; set; }

        /// <summary>
        /// The date scheduled
        /// </summary>
        public DateTimeOffset? DateScheduled { get; set; }

        /// <summary>
        /// A flag indicating whether a the scheduled payment was paid or not
        /// </summary>
        public bool? IsPaid { get; set; }

        /// <summary>
        /// The payment date if any
        /// </summary>
        public DateTimeOffset? DatePaid { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public CustomerServiceScheduledPaymentRequestModel() : base()
        {

        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns></returns>
        public override string ToString() => Amount.HasValue ? Amount.Value.ToLocalizedCurrency() : string.Empty;

        #endregion
    }
}
