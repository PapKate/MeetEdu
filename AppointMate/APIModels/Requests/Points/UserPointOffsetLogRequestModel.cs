
namespace AppointMate
{
    /// <summary>
    /// Request model used for a customer point offset log
    /// </summary>
    public class UserPointOffsetLogRequestModel 
    {
        #region Public Properties

        /// <summary>
        /// The user id
        /// </summary>
        public string? UserId { get; set; }

        /// <summary>
        /// The offset
        /// </summary>
        public int? Offset { get; set; }

        /// <summary>
        /// The note
        /// </summary>
        public string? Note { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public UserPointOffsetLogRequestModel() : base()
        {

        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Returns a string that represents the current object
        /// </summary>
        /// <returns></returns>
        public override string ToString() => $"User: {UserId}, Note: {Note}, Offset: {Offset}";

        #endregion
    }
}
