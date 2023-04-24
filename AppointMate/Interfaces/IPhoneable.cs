using AppointMate.DataModels.Classes;

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
}
