namespace AppointMate
{
    /// <summary>
    /// Provides abstractions for an object that has a department id
    /// </summary>
    public interface IDepartmentIdentifiable 
    {
        #region Properties

        /// <summary>
        /// The department id
        /// </summary>
        object? DepartmentId { get; set; }

        #endregion
    }

    /// <summary>
    /// Provides abstractions for an object that has a department id
    /// </summary>
    public interface IDepartmentIdentifiable<T> : IDepartmentIdentifiable
    {
        #region Properties

        /// <summary>
        /// The department id
        /// </summary>
        new T DepartmentId { get; set; }

        /// <summary>
        /// The company id
        /// </summary>
        object? IDepartmentIdentifiable.DepartmentId
        {
            get => DepartmentId;
            set { }
        }

        #endregion
    }
}
