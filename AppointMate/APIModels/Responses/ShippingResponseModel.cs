namespace AppointMate
{
    public class ShippingResponseModel : IAddressable
    {
        #region Private Properties

        /// <summary>
        /// The member of the <see cref="FirstName"/> property
        /// </summary>
        private string? mFirstName;

        /// <summary>
        /// The member of the <see cref="LastName"/> property
        /// </summary>
        private string? mLastName;

        /// <summary>
        /// The member of the <see cref="State"/> property
        /// </summary>
        private string? mState;

        /// <summary>
        /// The member of the <see cref="City"/> property
        /// </summary>
        private string? mCity;

        /// <summary>
        /// The member of the <see cref="Postcode"/> property
        /// </summary>
        private string? mPostcode;

        /// <summary>
        /// The member of the <see cref="Address"/> property
        /// </summary>
        private string? mAddress;

        /// <summary>
        /// The member of the <see cref="Address2"/> property
        /// </summary>
        private string? mAddress2;

        #endregion

        #region Public Properties

        /// <summary>
        /// The first name
        /// </summary>
        public string FirstName
        {
            get => mFirstName ?? string.Empty;
            set => mFirstName = value;
        }

        /// <summary>
        /// The last name
        /// </summary>
        public string LastName
        {
            get => mLastName ?? string.Empty;
            set => mLastName = value;
        }

        /// <summary>
        /// The country
        /// </summary>
        public CountryCode Country { get; set; }

        /// <summary>
        /// The address street
        /// </summary>
        public string State
        {
            get => mState ?? string.Empty;

            set => mState = value;
        }

        /// <summary>
        /// The city
        /// </summary>
        public string City
        {
            get => mCity ?? string.Empty;

            set => mCity = value;
        }

        /// <summary>
        /// The postal code
        /// </summary>
        public string Postcode
        {
            get => mPostcode ?? string.Empty;

            set => mPostcode = value;
        }

        /// <summary>
        /// The first address
        /// </summary>
        public string Address
        {
            get => mAddress ?? string.Empty;

            set => mAddress = value;
        }

        /// <summary>
        /// The second address
        /// </summary>
        public string Address2
        {
            get => mAddress2 ?? string.Empty;

            set => mAddress2 = value;
        }

        /// <summary>
        /// The longitude for the first address
        /// </summary>
        public double Longitude { get; set; }

        /// <summary>
        /// The latitude for the first address
        /// </summary>
        public double Latitude { get; set; }

        /// <summary>
        /// The longitude for the second address
        /// </summary>
        public double Longitude2 { get; set; }

        /// <summary>
        /// The latitude for the second address
        /// </summary>
        public double Latitude2 { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public ShippingResponseModel() : base()
        {

        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns></returns>
        public override string ToString() => $"{FirstName} {LastName} {Address}";

        #endregion
    }
}
