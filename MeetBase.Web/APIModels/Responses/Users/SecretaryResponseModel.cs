namespace MeetBase.Web
{
    /// <summary>
    /// Response model used for a secretary 
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

    /// <summary>
    /// A minimal version of the <see cref="SecretaryResponseModel"/>
    /// </summary>
    public class EmbeddedSecretaryResponseModel : EmbeddedStaffMemberResponseModel
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
        public EmbeddedSecretaryResponseModel() : base()
        {

        }

        #endregion
    }
}
