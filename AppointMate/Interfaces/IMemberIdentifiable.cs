namespace AppointMate
{
    /// <summary>
    /// Provides abstractions for an object that has a member id
    /// </summary>
    public interface IMemberIdentifiable
    {
        #region Properties

        /// <summary>
        /// The member id
        /// </summary>
        object? MemberId { get; set; }

        #endregion
    }

    /// <summary>
    /// Provides abstractions for an object that has a member id
    /// </summary>
    public interface IMemberIdentifiable<T> : IMemberIdentifiable
    {
        #region Properties

        /// <summary>
        /// The member id
        /// </summary>
        new T MemberId { get; set; }

        /// <summary>
        /// The member id
        /// </summary>
        object? IMemberIdentifiable.MemberId
        {
            get => MemberId;
            set { }
        }

        #endregion
    }
}
