namespace MeetBase
{
    /// <summary>
    /// Provides abstractions for an object that has a university id
    /// </summary>
    public interface IUniversityIdentifiable
    {
        #region Properties

        /// <summary>
        /// The university id
        /// </summary>
        object? UniversityId { get; set; }

        #endregion
    }

    /// <summary>
    /// Provides abstractions for an object that has a university id
    /// </summary>
    public interface IUniversityIdentifiable<T> : IUniversityIdentifiable
    {
        #region Properties

        /// <summary>
        /// The university id
        /// </summary>
        new T UniversityId { get; set; }

        /// <summary>
        /// The company id
        /// </summary>
        object? IUniversityIdentifiable.UniversityId
        {
            get => UniversityId;
            set { }
        }

        #endregion
    }
}
