
namespace AppointMate
{
    /// <summary>
    /// Provides abstractions for an object that has an assignable phone number
    /// </summary>
    public interface IPhoneable
    {
        #region Properties

        /// <summary>
        /// The phone number
        /// </summary>
        PhoneNumber? PhoneNumber { get; set; }

        #endregion
    }

    /// <summary>
    /// Provides abstractions for an object that has a company id
    /// </summary>
    public interface ICompanyIdentifiable
    {
        /// <summary>
        /// The company id
        /// </summary>
        string CompanyId { get; set; }
    }

    /// <summary>
    /// Provides abstractions for an object that has an id
    /// </summary>
    public interface IIdentifiable
    {
        /// <summary>
        /// The id
        /// </summary>
        string Id { get; set; }
    }

    /// <summary>
    /// Provides abstractions for an object that has a source and an id
    /// </summary>
    public interface IEmbeddedIdentifiable : IIdentifiable
    {
        /// <summary>
        /// The <see cref="IIdentifiable.Id"/> of the object that was used for creating the embedded object
        /// </summary>
        string Source { get; set; }
    }
}
