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
        public UserEntity? User { get; }

        /// <summary>
        /// The secretary
        /// </summary>
        public SecretaryEntity? Secretary { get; }

        /// <summary>
        /// The professor
        /// </summary>
        public ProfessorEntity? Professor { get; }

        /// <summary>
        /// The member
        /// </summary>
        public MemberEntity? Member { get; }

        #endregion

        #region Constructors

        /// <summary>
        /// Constructor for a secretary
        /// </summary>
        public LoginResult(UserEntity user, SecretaryEntity secretary) : base()
        {
            User = user ?? throw new ArgumentNullException(nameof(user));
            Secretary = secretary ?? throw new ArgumentNullException(nameof(secretary));
        }

        /// <summary>
        /// Constructor for a professor
        /// </summary>
        public LoginResult(UserEntity user, ProfessorEntity professor) : base()
        {
            User = user ?? throw new ArgumentNullException(nameof(user));
            Professor = professor ?? throw new ArgumentNullException(nameof(professor));
        }

        /// <summary>
        /// Constructor for a member
        /// </summary>
        public LoginResult(UserEntity user, MemberEntity member) : base()
        {
            User = user ?? throw new ArgumentNullException(nameof(user));
            Member = member ?? throw new ArgumentNullException(nameof(member));
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Creates and returns a <see cref="UserResponseModel"/> from the current <see cref="UserEntity"/>
        /// </summary>
        /// <returns></returns>
        public LoginResponse ToResponseModel()
        {
            var response = new LoginResponse(); 

            // If there is a user...
            if(User is not null)
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

    /// <summary>
    /// Contains the <see cref="UserEntity"/> and depending on the user role...
    /// <list type="bullet">
    /// <item>If it is a secretary, the <see cref="SecretaryEntity"/></item>
    /// <item>If it is a professor, the <see cref="ProfessorEntity"/></item>
    /// <item>If it is a member, the <see cref="MemberEntity"/></item>
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
