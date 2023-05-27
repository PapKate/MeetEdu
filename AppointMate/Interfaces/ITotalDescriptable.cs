
namespace AppointMate
{
    /// <summary>
    /// Provides abstractions for an object that has a description and a small description
    /// </summary>
    public interface ITotalDescriptable : IDescriptable
    {
        #region Properties

        /// <summary>
        /// The small description
        /// </summary>
        string SmallDescription { get; set; }

        #endregion
    }
}
