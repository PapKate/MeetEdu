namespace MeetEdu
{
    /// <summary>
    /// Request model used for a secretary 
    /// </summary>
    public class SecretaryRequestModel : StaffMemberRequestModel
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
        public SecretaryRequestModel() : base()
        {

        }

        #endregion
    }
}
