
namespace AppointMate
{
    /// <summary>
    /// Provides abstractions for an object that has a description
    /// </summary>
    public interface IDescriptable
    {
        #region Properties

        /// <summary>
        /// The description
        /// </summary>
        string Description { get; set; }

        #endregion
    }
}
