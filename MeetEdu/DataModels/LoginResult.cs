namespace MeetEdu
{
    /// <summary>
    /// Contains the <see cref="UserEntity"/> and depending on the user role...
    /// <list type="bullet">
    /// <item>If it is a secretary, the <see cref="SecretaryEntity"/></item>
    /// <item>If it is a professor, the <see cref="ProfessorEntity"/></item>
    /// <item>If it is a member, the <see cref="MemberEntity"/></item>
    /// </list>
    /// </summary>
    public class LoginResult
    {
        #region Public Properties

        /// <summary>
        /// The user
        /// </summary>
        public UserEntity User { get; }

        /// <summary>
        /// The secretary
        /// </summary>
        public SecretaryEntity? Secretary { get; init; }

        /// <summary>
        /// The professor
        /// </summary>
        public ProfessorEntity? Professor { get; init; }

        /// <summary>
        /// The member
        /// </summary>
        public MemberEntity? Member { get; init; }

        /// <summary>
        /// The JWT token
        /// </summary>
        public string Token { get; }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public LoginResult(UserEntity user, string token) : base()
        {
            User = user ?? throw new ArgumentNullException(nameof(user));
            Token = token ?? throw new ArgumentNullException(nameof(token));
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Creates and returns a <see cref="UserResponseModel"/> from the current <see cref="UserEntity"/>
        /// </summary>
        /// <returns></returns>
        public LoginResponseModel ToResponseModel()
        {
            var response = new LoginResponseModel(Token);

            // If there is a user...
            if (User is not null)
                // Adds the user to the response
                response.User = EntityHelpers.ToResponseModel<UserResponseModel>(User);

            // If there is a secretary...
            if (Secretary is not null)
                // Adds the secretary to the response
                response.Secretary = EntityHelpers.ToResponseModel<SecretaryResponseModel>(Secretary);

            // If there is a professor...
            if (Professor is not null)
                // Adds the professor to the response
                response.Professor = EntityHelpers.ToResponseModel<ProfessorResponseModel>(Professor);

            // If there is a member...
            if (Member is not null)
                // Adds the user to the response
                response.Member = EntityHelpers.ToResponseModel<MemberResponseModel>(Member);
            
            return response;
        }

        #endregion
    }
}
