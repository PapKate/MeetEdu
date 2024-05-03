namespace MeetBase
{
    /// <summary>
    /// The information regarding a country
    /// </summary>
    public class CountryData
    {
        /// <summary>
        /// The country name
        /// </summary>
        public string Country { get; }

        /// <summary>
        /// The country code for phone numbers
        /// </summary>
        public string CountryCode { get; }

        /// <summary>
        /// The representative ISO codes of the country
        /// </summary>
        public IEnumerable<string>? IsoCodes { get; set;  }

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="country">The name</param>
        /// <param name="countryCode">The country code</param>
        /// <param name="isoCodes">The ISO codes</param>
        public CountryData(string country, string countryCode, List<string>? isoCodes)
        {
            Country = country;
            CountryCode = countryCode;
            IsoCodes = isoCodes;
        }
    }
}
