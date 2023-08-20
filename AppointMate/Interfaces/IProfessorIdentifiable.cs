namespace AppointMate
{
    /// <summary>
    /// Provides abstractions for an object that has a professor id
    /// </summary>
    public interface IProfessorIdentifiable
    {
        #region Properties

        /// <summary>
        /// The professor id
        /// </summary>
        object? ProfessorId { get; set; }

        #endregion
    }

    /// <summary>
    /// Provides abstractions for an object that has a professor id
    /// </summary>
    public interface IProfessorIdentifiable<T> : IProfessorIdentifiable
    {
        #region Properties

        /// <summary>
        /// The professor id
        /// </summary>
        new T ProfessorId { get; set; }

        /// <summary>
        /// The professor id
        /// </summary>
        object? IProfessorIdentifiable.ProfessorId
        {
            get => ProfessorId;
            set { }
        }

        #endregion
    }
}
