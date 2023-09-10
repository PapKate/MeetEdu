namespace MeetBase
{
    /// <summary>
    /// Provides abstractions for an object that has an address
    /// </summary>
    public interface IAddressable
    {
        #region Properties

        /// <summary>
        /// The email
        /// </summary>
        string Address { get; set; }

        /// <summary>
        /// The longitude
        /// </summary>
        double Longitude { get; set; }

        /// <summary>
        /// The latitude
        /// </summary>
        double Latitude { get; set; }

        #endregion
    }
}
