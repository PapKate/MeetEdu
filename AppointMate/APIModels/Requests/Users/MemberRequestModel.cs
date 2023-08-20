namespace AppointMate
{
    /// <summary>
    /// Request model used for a member
    /// </summary>
    public class MemberRequestModel : BaseRequestModel
    {
        #region Public Properties

        /// <summary>
        /// The id of the user
        /// </summary>
        public string? UserId { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public MemberRequestModel() : base()
        {

        }

        #endregion
    }
}
