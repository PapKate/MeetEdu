namespace AppointMate
{
    public class LocationRequestModel 
    {
        #region Public Properties

        /// <summary>
        /// The country
        /// </summary>
        public CountryCode Country { get; set; }

        /// <summary>
        /// The address street
        /// </summary>
        public string? State { get; set; }

        /// <summary>
        /// The city
        /// </summary>
        public string? City { get; set; }

        /// <summary>
        /// The postal code
        /// </summary>
        public string? Postcode { get; set; }

        /// <summary>
        /// The first address
        /// </summary>
        public string? Address { get; set; }

        /// <summary>
        /// The second address
        /// </summary>
        public string? Address2 { get; set; }

        /// <summary>
        /// The longitude for the first address
        /// </summary>
        public double? Longitude { get; set; }

        /// <summary>
        /// The latitude for the first address
        /// </summary>
        public double? Latitude { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public LocationRequestModel() : base()
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

    public class ShippingRequestModel : LocationRequestModel
    {
        #region Public Properties

        /// <summary>
        /// The first name
        /// </summary>
        public string? FirstName { get; set; }

        /// <summary>
        /// The last name
        /// </summary>
        public string? LastName { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public ShippingRequestModel() : base()
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
