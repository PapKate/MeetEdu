
namespace AppointMate
{
    /// <summary>
    /// Represents a billing
    /// </summary>
    public class BillingResponseModel : ShippingResponseModel, IEmailable, IPhoneable
    {
        #region Private Members

        /// <summary>
        /// The member of the <see cref="Email"/> property
        /// </summary>
        private string? mEmail;

        #endregion

        #region Public Properties

        /// <summary>
        /// The email
        /// </summary>
        public string Email
        {
            get => mEmail ?? string.Empty;
            set => mEmail = value;
        }

        /// <summary>
        /// The phone number
        /// </summary>
        public PhoneNumber? PhoneNumber { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public BillingResponseModel() : base()
        {

        }

        #endregion
    }
}
