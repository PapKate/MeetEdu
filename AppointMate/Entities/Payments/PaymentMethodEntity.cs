using MongoDB.Bson;

namespace AppointMate
{
    /// <summary>
    /// Represents a payment method document in the MongoDB
    /// </summary>
    public class PaymentMethodEntity : StandardEntity, ICompanyIdentifiable<ObjectId>, IDescriptable, IImageable, IIconPathDatable
    {
        #region Private Members

        /// <summary>
        /// The member of the <see cref="Description"/> property
        /// </summary>
        private string? mDescription;

        /// <summary>
        /// The member of the <see cref="IconPathData"/> property
        /// </summary>
        private string? mIconPathData;

        #endregion

        #region Public Properties

        /// <summary>
        /// The company id
        /// </summary>
        public ObjectId CompanyId { get; set; }

        /// <summary>
        /// The description
        /// </summary>
        public string Description
        {
            get => mDescription ?? string.Empty;
            set => mDescription = value;
        }

        /// <summary>
        /// The path data of the icon
        /// </summary>
        public string IconPathData
        {
            get => mIconPathData ?? string.Empty;
            set => mIconPathData = value;
        }

        /// <summary>
        /// The image URL
        /// </summary>
        public Uri? ImageUrl { get; set; }

        /// <summary>
        /// The flat rate commission
        /// </summary>
        public decimal FlatRateCommission { get; set; }

        /// <summary>
        /// The percent commission.
        /// NOTE: That's a value from 0 to 100!
        /// </summary>
        public decimal PercentCommission { get; set; }

        /// <summary>
        /// A flag indicating whether the payment method is active or not
        /// </summary>
        public bool IsActive { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public PaymentMethodEntity() : base()
        {

        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Creates and returns a <see cref="PaymentMethodEntity"/> from the specified <paramref name="model"/>
        /// </summary>
        /// <param name="model">The model</param>
        /// <param name="companyId">The company id</param>
        /// <returns></returns>
        public static PaymentMethodEntity FromRequestModel(PaymentMethodRequestModel model, ObjectId companyId)
            => EntityHelpers.FromRequestModel<PaymentMethodEntity>(model, companyId);

        /// <summary>
        /// Creates and returns a <see cref="PaymentMethodResponseModel"/> from the current <see cref="PaymentMethodEntity"/>
        /// </summary>
        /// <returns></returns>
        public PaymentMethodResponseModel ToResponseModel()
            => EntityHelpers.ToResponseModel<PaymentMethodResponseModel>(this);

        /// <summary>
        /// Creates and returns a <see cref="EmbeddedPaymentMethodEntity"/> from the current <see cref="PaymentMethodEntity"/>
        /// </summary>
        /// <param name="amount">The amount that is being paid</param>
        /// <returns></returns>
        public EmbeddedPaymentMethodEntity ToEmbeddedEntity(decimal amount)
            => EntityHelpers.ToEmbeddedEntity<EmbeddedPaymentMethodEntity>(this);

        #endregion
    }

    /// <summary>
    /// The payment method response model
    /// </summary>
    public class EmbeddedPaymentMethodEntity : EmbeddedStandardEntity, ICompanyIdentifiable<ObjectId>, IImageable, IVectorImageable
    {
        #region Public Properties

        /// <summary>
        /// The company id
        /// </summary>
        public ObjectId CompanyId { get; set; }

        /// <summary>
        /// The icon path data
        /// </summary>
        public VectorSource? VectorSource { get; set; }

        /// <summary>
        /// The image URL
        /// </summary>
        public Uri? ImageUrl { get; set; }

        /// <summary>
        /// The flat rate commission
        /// </summary>
        public decimal FlatRateCommission { get; set; }

        /// <summary>
        /// The percent commission.
        /// NOTE: That's a value from 0 to 100!
        /// </summary>
        public decimal PercentCommission { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public EmbeddedPaymentMethodEntity() : base()
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
