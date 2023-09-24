
namespace MeetBase.Web
{
    /// <summary>
    /// Contains the <see cref="UserResponseModel"/> and depending on the user role...
    /// <list type="bullet">
    /// <item>If it is a secretary, the <see cref="SecretaryResponseModel"/></item>
    /// <item>If it is a professor, the <see cref="ProfessorResponseModel"/></item>
    /// <item>If it is a member, the <see cref="MemberResponseModel"/></item>
    /// </list>
    /// </summary>
    public class LoginResponse
    {
        #region Public Properties

        /// <summary>
        /// The user
        /// </summary>
        public UserResponseModel? User { get; set; }

        /// <summary>
        /// The secretary
        /// </summary>
        public SecretaryResponseModel? Secretary { get; set; }

        /// <summary>
        /// The professor
        /// </summary>
        public ProfessorResponseModel? Professor { get; set; }

        /// <summary>
        /// The member
        /// </summary>
        public MemberResponseModel? Member { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public LoginResponse() : base()
        {

        }

        #endregion
    }
}
