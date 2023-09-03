namespace MeetEdu
{
    /// <summary>
    /// Provides abstractions for an object that has a user id
    /// </summary>
    public interface IUserIdentifiable
    {
        #region Properties

        /// <summary>
        /// The user id
        /// </summary>
        object? UserId { get; set; }

        #endregion
    }

    /// <summary>
    /// Provides abstractions for an object that has a user id
    /// </summary>
    public interface IUserIdentifiable<T> : IUserIdentifiable
    {
        #region Properties

        /// <summary>
        /// The user id
        /// </summary>
        new T UserId { get; set; }

        /// <summary>
        /// The user id
        /// </summary>
        object? IUserIdentifiable.UserId
        {
            get => UserId;
            set { }
        }

        #endregion
    }
}
