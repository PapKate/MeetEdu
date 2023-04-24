namespace AppointMate
{
    /// <summary>
    /// Provides abstractions for an object that has an assignable email
    /// </summary>
    public interface IEmailable 
    {
        #region Properties

        /// <summary>
        /// The email
        /// </summary>
        string Email { get; set; }

        #endregion
    }
}
