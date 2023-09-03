namespace MeetEdu
{
    /// <summary>
    /// Represents a member
    /// </summary>
    public class MemberResponseModel : DateResponseModel, IUserIdentifiable<string>
    {
        #region Private Members

        /// <summary>
        /// The member of the <see cref="UserId"/> property
        /// </summary>
        private string? mUserId;

        #endregion

        #region Public Properties

        /// <summary>
        /// The user id
        /// </summary>
        public string UserId
        {
            get => mUserId ?? string.Empty;
            set => mUserId = value;
        }

        /// <summary>
        /// The total number of appointments
        /// </summary>
        public uint TotalAppointments { get; set; }

        /// <summary>
        /// The total number of saved companies
        /// </summary>
        public uint TotalSavedCompanies { get; set; }

        /// <summary>
        /// The total number of saved professors
        /// </summary>
        public uint TotalSavedProfessors { get; set; }

        /// <summary>
        /// The user
        /// </summary>
        public EmbeddedUserResponseModel? User { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public MemberResponseModel() : base()
        {

        }

        #endregion
    }

    /// <summary>
    /// A minimal version of the <see cref="MemberResponseModel"/> that contains the fields that are 
    /// more frequently used when embedding documents in the MongoDB
    /// </summary>
    public class EmbeddedMemberResponseModel : EmbeddedBaseResponseModel, IUserIdentifiable<string>
    {
        #region Private Members

        /// <summary>
        /// The member of the <see cref="UserId"/> property
        /// </summary>
        private string? mUserId;

        #endregion

        #region Public Properties

        /// <summary>
        /// The user id
        /// </summary>
        public string UserId
        {
            get => mUserId ?? string.Empty;
            set => mUserId = value;
        }

        /// <summary>
        /// The user
        /// </summary>
        public EmbeddedUserResponseModel? User { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public EmbeddedMemberResponseModel() : base()
        {

        }

        #endregion
    }
}
