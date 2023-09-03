
namespace MeetEdu
{
    /// <summary>
    /// Provides abstractions for an object that can be identified
    /// </summary>
    public interface IIdentifiable
    {
        #region Properties

        /// <summary>
        /// The id
        /// </summary>
        object? Id { get; set; }

        #endregion
    }

    /// <summary>
    /// Provides abstractions for an object that can be identified
    /// </summary>
    /// <typeparam name="T">The type of the key</typeparam>
    public interface IIdentifiable<T> : IIdentifiable
    {
        #region Properties

        /// <summary>
        /// The id
        /// </summary>
        new T Id { get; set; }

        /// <summary>
        /// The id
        /// </summary>
        object? IIdentifiable.Id
        {
            get => Id;
            set { }
        }

        #endregion
    }

    /// <summary>
    /// Provides abstractions for an object that has a source and an id
    /// </summary>
    public interface ISourceIdentifiable 
    {
        /// <summary>
        /// The <see cref="IIdentifiable.Id"/> of the object that was used for creating the embedded object
        /// </summary>
        object? Source { get; set; }
    }

    /// <summary>
    /// Provides abstractions for an object that has a source and an id
    /// </summary>
    public interface ISourceIdentifiable<T> : ISourceIdentifiable
    {
        /// <summary>
        /// The <see cref="IIdentifiable.Id"/> of the object that was used for creating the embedded object
        /// </summary>
        new T Source { get; set; }

        /// <summary>
        /// The <see cref="IIdentifiable.Id"/> of the object that was used for creating the embedded object
        /// </summary>
        object? ISourceIdentifiable.Source
        {
            get => Source;
            set { }
        }
    }

    /// <summary>
    /// Provides abstractions for an object that has a source and an id
    /// </summary>
    public interface IEmbeddableIdentifiable : IIdentifiable, ISourceIdentifiable
    {
     
    }

    /// <summary>
    /// Provides abstractions for an object that has a source and an id
    /// </summary>
    public interface IEmbeddableIdentifiable<T> : IIdentifiable<T>, ISourceIdentifiable<T>
    {

    }
}
