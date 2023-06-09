using MongoDB.Bson;

namespace AppointMate
{
    /// <summary>
    /// Represents a staff member document in the MongoDB
    /// </summary>
    public class StaffMemberEntity : UserEntity, IUserIdentifiable<ObjectId>
    {
        #region Private Members

        /// <summary>
        /// The member of the <see cref="Quote"/> property
        /// </summary>
        private string? mQuote;

        /// <summary>
        /// The member of the <see cref="Roles"/> property
        /// </summary>
        private IEnumerable<string>? mRoles;

        #endregion

        #region Public Properties

        /// <summary>
        /// The id of the user
        /// </summary>
        public ObjectId UserId { get; set; }

        /// <summary>
        /// The quote
        /// </summary>
        public string Quote
        {
            get => mQuote ?? string.Empty;
            set => mQuote = value;
        }

        /// <summary>
        /// A flag indicating whether they are an owner
        /// </summary>
        public bool IsOwner { get; set; }

        /// <summary>
        /// The roles
        /// </summary>
        public IEnumerable<string> Roles
        {
            get => mRoles ?? Enumerable.Empty<string>();
            set => mRoles = value;
        }

        /// <summary>
        /// The work hours
        /// </summary>
        public WeeklyScheduleResponseModel? WorkHours { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public StaffMemberEntity() : base()
        {

        }

        #endregion
    }

    /// <summary>
    /// A minimal version of the <see cref="StaffMemberEntity"/> that contains the fields that are 
    /// more frequently used when embedding documents in the MongoDB
    /// </summary>
    public class EmbeddedStaffMemberEntity : EmbeddedUserEntity
    {
        #region Private Members

        /// <summary>
        /// The member of the <see cref="Quote"/> property
        /// </summary>
        private string? mQuote;

        /// <summary>
        /// The member of the <see cref="Roles"/> property
        /// </summary>
        private IEnumerable<string>? mRoles;

        #endregion

        #region Public Properties

        /// <summary>
        /// The quote
        /// </summary>
        public string Quote
        {
            get => mQuote ?? string.Empty;
            set => mQuote = value;
        }

        /// <summary>
        /// A flag indicating whether they are an owner
        /// </summary>
        public bool IsOwner { get; set; }

        /// <summary>
        /// The roles
        /// </summary>
        public IEnumerable<string> Roles
        {
            get => mRoles ?? Enumerable.Empty<string>();
            set => mRoles = value;
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public EmbeddedStaffMemberEntity() : base()
        {

        }

        #endregion
    }
}
