namespace AppointMate
{
    /// <summary>
    /// The customer service response model
    /// </summary>
    public class CustomerServiceResponseModel : BaseResponseModel
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
        public CustomerServiceResponseModel() : base()
        {

        }

        #endregion
    }

    /// <summary>
    /// The embedded customer service response model
    /// </summary>
    public class EmbeddedCustomerServiceResponseModel
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
