
using System.ComponentModel;

namespace AppointMate
{
    /// <summary>
    /// Provides abstractions for an object that has a company id
    /// </summary>
    public interface ICompanyIdentifiable 
    {
        #region Properties

        /// <summary>
        /// The company id
        /// </summary>
        object? CompanyId { get; set; }

        #endregion
    }

    /// <summary>
    /// Provides abstractions for an object that has a company id
    /// </summary>
    public interface ICompanyIdentifiable<T> : ICompanyIdentifiable
    {
        #region Properties

        /// <summary>
        /// The company id
        /// </summary>
        new T CompanyId { get; set; }

        /// <summary>
        /// The company id
        /// </summary>
        object? ICompanyIdentifiable.CompanyId
        {
            get => CompanyId;
            set { }
        }

        #endregion
    }

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
        /// The company id
        /// </summary>
        object? IUserIdentifiable.UserId
        {
            get => UserId;
            set { }
        }

        #endregion
    }
}
