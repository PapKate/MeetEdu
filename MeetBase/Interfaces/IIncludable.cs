namespace MeetBase
{
    /// <summary>
    /// Provides abstractions for an object that can include or exclude other objects based on identifiers
    /// </summary>
    public interface IIncludable<T> : IReadOnlyIncludable<T>
    {
        #region Properties

        /// <summary>
        /// Limit the result set by including only resource with specific ids
        /// </summary>
        new IEnumerable<T>? Include { get; set; }

        /// <summary>
        /// Limit the result set by excluding resource with specific ids
        /// </summary>
        new IEnumerable<T>? Exclude { get; set; }

        /// <summary>
        /// Limit the result set by including only resource with specific ids
        /// </summary>
        IEnumerable<T>? IReadOnlyIncludable<T>.Include => Include;

        /// <summary>
        /// Limit the result set by excluding resource with specific ids
        /// </summary>
        IEnumerable<T>? IReadOnlyIncludable<T>.Exclude => Exclude;

        #endregion
    }

    /// <summary>
    /// Provides abstractions for an object that can include or exclude other objects based on identifiers
    /// </summary>
    public interface IReadOnlyIncludable<T>
    {
        #region Properties

        /// <summary>
        /// Limit the result set by including only resource with specific ids
        /// </summary>
        IEnumerable<T>? Include { get; }

        /// <summary>
        /// Limit the result set by excluding resource with specific ids
        /// </summary>
        IEnumerable<T>? Exclude { get; }

        #endregion
    }
}
