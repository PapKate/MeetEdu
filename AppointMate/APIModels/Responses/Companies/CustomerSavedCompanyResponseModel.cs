namespace AppointMate
{
    public class CustomerSavedCompanyResponseModel : BaseResponseModel
    {
        #region Public Properties

        /// <summary>
        /// The company
        /// </summary>
        public EmbeddedCompanyResponseModel? Company { get; set; }

        /// <summary>
        /// The customer
        /// </summary>
        public EmbeddedCustomerResponseModel? Customer { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public CustomerSavedCompanyResponseModel() : base()
        {

        }

        #endregion
    }
}
