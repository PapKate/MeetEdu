namespace MeetEdu
{
    /// <summary>
    /// Provides abstractions for an object that can be noted
    /// </summary>
    public interface INoteable : IReadOnlyNoteable
    {
        #region Properties

        /// <summary>
        /// The note
        /// </summary>
        new string Note { get; set; }

        /// <summary>
        /// The note
        /// </summary>
        string IReadOnlyNoteable.Note => Note;

        #endregion
    }

    /// <summary>
    /// Provides abstractions for an object with a note
    /// </summary>
    public interface IReadOnlyNoteable
    {
        #region Properties

        /// <summary>
        /// The note
        /// </summary>
        string Note { get; }

        #endregion
    }
}
