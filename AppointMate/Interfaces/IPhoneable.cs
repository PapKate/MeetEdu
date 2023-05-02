
namespace AppointMate
{
    /// <summary>
    /// Provides abstractions for an object that has an assignable phone number
    /// </summary>
    public interface IPhoneable
    {
        #region Properties

        /// <summary>
        /// The phone number
        /// </summary>
        PhoneNumber? PhoneNumber { get; set; }

        #endregion
    }

    /// <summary>
    /// Provides abstractions for an object that has a description
    /// </summary>
    public interface IDescribable
    {
        #region Properties

        /// <summary>
        /// The description
        /// </summary>
        string Description { get; set; }

        #endregion
    }

    /// <summary>
    /// Provides abstractions for an object that has a description and a small description
    /// </summary>
    public interface ITotalDescribable : IDescribable
    {
        #region Properties

        /// <summary>
        /// The small description
        /// </summary>
        string SmallDescription { get; set; }

        #endregion
    }
}
