namespace AppointMate
{
    /// <summary>
    /// Represents a location
    /// </summary>
    public class Location : IAddressable
    {
        #region Private Properties

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

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public Location() : base()
        {

        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns></returns>
        public override string ToString() => $"{Address}";

        #endregion
    }
}
