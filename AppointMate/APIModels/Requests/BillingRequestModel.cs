
namespace AppointMate
{
    public class BillingRequestModel : ShippingRequestModel, IPhoneable
    {
        #region Public Properties

        /// <summary>
        /// The email
        /// </summary>
        public string? Email { get; set; }

        /// <summary>
        /// The phone number
        /// </summary>
        public PhoneNumber? PhoneNumber { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public BillingRequestModel() : base()
        {

        }

        #endregion
    }
}
