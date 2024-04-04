using Newtonsoft.Json;

using System.Net;

namespace MeetBase
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
        /// The longitude 
        /// </summary>
        public double Longitude { get; set; }

        /// <summary>
        /// The latitude 
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

    /// <summary>
    /// The geo-location
    /// </summary>
    public class GeoLocation
    {
        #region Private Properties

        /// <summary>
        /// The member of the <see cref="Label"/> property
        /// </summary>
        private string? mLabel;

        #endregion

        #region Public Properties

        /// <summary>
        /// The label
        /// </summary>
        [JsonProperty("label")]
        public string Label
        {
            get => mLabel ?? string.Empty;

            set => mLabel = value;
        }

        /// <summary>
        /// The longitude 
        /// </summary>
        [JsonProperty("y")]
        public double Longitude { get; set; }

        /// <summary>
        /// The latitude
        /// </summary>
        [JsonProperty("x")]
        public double Latitude { get; set; }

        #endregion
    }
}
