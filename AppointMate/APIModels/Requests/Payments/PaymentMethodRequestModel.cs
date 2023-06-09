namespace AppointMate
{
    /// <summary>
    /// The payment method response model
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
        /// The small image URL
        /// </summary>
        public Uri? SmallImageUrl { get; set; }

        /// <summary>
        /// The image URL
        /// </summary>
        public Uri? NormalImageUrl { get; set; }

        /// <summary>
        /// The large image URL
        /// </summary>
        public Uri? LargeImageUrl { get; set; }

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

    /// <summary>
    /// The payment method response model
    /// </summary>
    public class EmbeddedPaymentMethodRequestModel : EmbeddedStandardRequestModel, IImageable
    {
        #region Public Properties
        
        /// <summary>
        /// The path data of the icon
        /// </summary>
        public string? IconPathData { get; set; }

        /// <summary>
        public Uri? SmallImageUrl { get; set; }

        /// <summary>
        /// The image URL
        /// </summary>
        public Uri? NormalImageUrl { get; set; }

        /// <summary>
        /// The large image URL
        /// </summary>
        public Uri? LargeImageUrl { get; set; }

        /// <summary>
        /// The flat rate commission
        /// </summary>
        public decimal? FlatRateCommission { get; set; }

        /// <summary>
        /// The percent commission.
        /// NOTE: That's a value from 0 to 100!
        /// </summary>
        public decimal? PercentCommission { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public EmbeddedPaymentMethodRequestModel() : base()
        {

        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns></returns>
        public override string ToString() => $"{Name} Flat rate commission: {FlatRateCommission}, Percent commission: {PercentCommission}";

        #endregion
    }
}
