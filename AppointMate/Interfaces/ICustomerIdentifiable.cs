namespace AppointMate
{
    /// <summary>
    /// Provides abstractions for an object that has a customer id
    /// </summary>
    public interface ICustomerIdentifiable
    {
        #region Properties

        /// <summary>
        /// The customer id
        /// </summary>
        object? CustomerId { get; set; }

        #endregion
    }

    /// <summary>
    /// Provides abstractions for an object that has a customer id
    /// </summary>
    public interface ICustomerIdentifiable<T> : ICustomerIdentifiable
    {
        #region Properties

        /// <summary>
        /// The customer id
        /// </summary>
        new T UserId { get; set; }

        /// <summary>
        /// The customer id
        /// </summary>
        object? ICustomerIdentifiable.CustomerId
        {
            get => UserId;
            set { }
        }

        #endregion
    }
}
