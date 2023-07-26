namespace AppointMate
{
    /// <summary>
    /// Request model used for a payment method
    /// </summary>
    public class PaymentMethodRequestModel : StandardRequestModel, IImageable
    {
        #region Public Properties

        /// <summary>
        /// The description
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// The path data of the icon
        /// </summary>
        public string? IconPathData { get; set; }

        /// <summary>
        /// The image URL
        /// </summary>
        public Uri? ImageUrl { get; set; }

        /// <summary>
        /// The flat rate commission
        /// </summary>
        public decimal? FlatRateCommission { get; set; }

        /// <summary>
        /// The percent commission.
        /// NOTE: That's a value from 0 to 100!
        /// </summary>
        public decimal? PercentCommission { get; set; }

        /// <summary>
        /// A flag indicating whether the payment method is active or not
        /// </summary>
        public bool? IsActive { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public PaymentMethodRequestModel() : base()
        {

        }

        #endregion
    }
}
