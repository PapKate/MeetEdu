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
        /// The member of the <see cref="Labels"/> property
        /// </summary>
        private IEnumerable<LabelEntity>? mLabels;

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
        /// The labels
        /// </summary>
        public IEnumerable<LabelEntity> Labels
        {
            get => mLabels ?? Enumerable.Empty<LabelEntity>();
            set => mLabels = value;
        }

        /// <summary>
        /// The work hours
        /// </summary>
        public WeeklyScheduleEntity? WorkHours { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public StaffMemberEntity() : base()
        {

        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Creates and returns a <see cref="StaffMemberEntity"/> from the specified <paramref name="model"/>
        /// </summary>
        /// <param name="model">The model</param>
        /// <returns></returns>
        public static StaffMemberEntity FromRequestModel(StaffMemberRequestModel model)
        {
            var entity = new StaffMemberEntity();

            DI.Mapper.Map(model, entity);
            return entity;
        }

        /// <summary>
        /// Creates and returns a <see cref="StaffMemberResponseModel"/> from the current <see cref="StaffMemberEntity"/>
        /// </summary>
        /// <returns></returns>
        public new StaffMemberResponseModel ToResponseModel()
            => EntityHelpers.ToResponseModel<StaffMemberResponseModel>(this);

        /// <summary>
        /// Creates and returns a <see cref="EmbeddedStaffMemberEntity"/> from the current <see cref="StaffMemberEntity"/>
        /// </summary>
        /// <returns></returns>
        public new EmbeddedStaffMemberEntity ToEmbeddedEntity()
            => EntityHelpers.ToEmbeddedEntity<EmbeddedStaffMemberEntity>(this);

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
