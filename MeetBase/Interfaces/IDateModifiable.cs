namespace MeetBase
{
    /// <summary>
    /// Provides abstraction for an object that can be marked as modified at a specific date
    /// </summary>
    public interface IDateModifiable : IReadOnlyDateModifiable
    {
        #region Properties

        /// <summary>
        /// The modification date
        /// </summary>
        new DateTimeOffset DateModified { get; set; }

        /// <summary>
        /// The modification date
        /// </summary>
        DateTimeOffset IReadOnlyDateModifiable.DateModified => DateModified;

        #endregion
    }

    /// <summary>
    /// Provides abstractions for an object that has a modification date
    /// </summary>
    public interface IReadOnlyDateModifiable
    {
        #region Properties

        /// <summary>
        /// The modification date
        /// </summary>
        DateTimeOffset DateModified { get; }

        #endregion
    }
}
