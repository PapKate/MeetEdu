namespace MeetEdu
{
    /// <summary>
    /// Provides abstractions for an object that has can be marked as created at a specific date
    /// </summary>
    public interface IDateCreatable : IReadOnlyDateCreatable
    {
        #region Properties

        /// <summary>
        /// The creation date
        /// </summary>
        new DateTimeOffset DateCreated { get; set; }

        /// <summary>
        /// The creation date
        /// </summary>
        DateTimeOffset IReadOnlyDateCreatable.DateCreated => DateCreated;

        #endregion
    }

    /// <summary>
    /// Provides abstractions for an object that has a creation date
    /// </summary>
    public interface IReadOnlyDateCreatable
    {
        #region Properties

        /// <summary>
        /// The creation date
        /// </summary>
        DateTimeOffset DateCreated { get; }

        #endregion
    }
}
