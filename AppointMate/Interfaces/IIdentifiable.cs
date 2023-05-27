
namespace AppointMate
{
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
