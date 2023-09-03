namespace MeetEdu
{
    /// <summary>
    /// Provides abstractions for an object that has an assignable parent
    /// </summary>
    public interface IParentable<T> : IReadOnlyParentable<T>
    {
        #region Properties

        /// <summary>
        /// The parent
        /// </summary>
        new T Parent { get; set; }

        /// <summary>
        /// The parent
        /// </summary>
        T IReadOnlyParentable<T>.Parent => Parent;

        #endregion
    }

    /// <summary>
    /// Provides abstractions for an object with a parent
    /// </summary>
    public interface IReadOnlyParentable<T>
    {
        #region Properties

        /// <summary>
        /// The parent
        /// </summary>
        T Parent { get; }

        #endregion
    }
}
