namespace MeetBase
{
    /// <summary>
    /// Provides abstractions for an object that can set its offset
    /// </summary>
    public interface IOffsetable : IReadOnlyOffsetable
    {
        #region Properties

        /// <summary>
        /// The offset
        /// </summary>
        new int Offset { get; set; }

        /// <summary>
        /// The offset
        /// </summary>
        int IReadOnlyOffsetable.Offset => Offset;

        #endregion
    }

    /// <summary>
    /// Provides abstractions for an object with an offset
    /// </summary>
    public interface IReadOnlyOffsetable
    {
        #region Properties

        /// <summary>
        /// The offset
        /// </summary>
        int Offset { get; }

        #endregion
    }
}
