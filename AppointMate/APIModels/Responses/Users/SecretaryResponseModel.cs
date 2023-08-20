namespace AppointMate
{
    /// <summary>
    /// Represents a secretary 
    /// </summary>
    public class SecretaryResponseModel : StaffMemberResponseModel
    {
        #region Public Properties

        /// <summary>
        /// The role
        /// </summary>
        public SecretaryRole Role { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public SecretaryResponseModel() : base()
        {

        }

        #endregion
    }
}
