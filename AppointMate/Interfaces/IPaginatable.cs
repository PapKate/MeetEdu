namespace AppointMate
{
    /// <summary>
    /// Provides abstractions for an object that provides assignable pagination information
    /// </summary>
    public interface IPaginatable : IReadOnlyPaginatable
    {
        #region Properties

        /// <summary>
        /// The index of the page starting from 0.
        /// </summary>
        new int Page { get; set; }

        /// <summary>
        /// Maximum number of items to be returned in result set.
        /// </summary>
        new int PerPage { get; set; }

        /// <summary>
        /// The index of the page starting from 0.
        /// </summary>
        int IReadOnlyPaginatable.Page => Page;

        /// <summary>
        /// Maximum number of items to be returned in result set.
        /// </summary>
        int IReadOnlyPaginatable.PerPage => PerPage;

        #endregion
    }

    /// <summary>
    /// Provides abstractions for an object that contains pagination information
    /// </summary>
    public interface IReadOnlyPaginatable
    {
        #region Properties

        /// <summary>
        /// The index of the page starting from 0.
        /// </summary>
        int Page { get; }

        /// <summary>
        /// Maximum number of items to be returned in result set.
        /// </summary>
        int PerPage { get; }

        #endregion
    }
}
