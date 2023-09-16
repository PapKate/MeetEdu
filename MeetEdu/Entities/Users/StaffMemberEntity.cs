using AutoMapper;

using MongoDB.Bson;
using MongoDB.Driver;

namespace MeetEdu
{
    /// <summary>
    /// Represents a staff member document in the MongoDB
    /// </summary>
    public abstract class StaffMemberEntity : DateEntity, IUserIdentifiable<ObjectId>, IDepartmentIdentifiable<ObjectId>
    {
        #region Private Members

        /// <summary>
        /// The member of the <see cref="Quote"/> property
        /// </summary>
        private string? mQuote;

        #endregion

        #region Public Properties

        /// <summary>
        /// The user id
        /// </summary>
        public ObjectId UserId { get; set; }

        /// <summary>
        /// The department id
        /// </summary>
        public ObjectId DepartmentId { get; set; }

        /// <summary>
        /// The quote
        /// </summary>
        public string Quote
        {
            get => mQuote ?? string.Empty;
            set => mQuote = value;
        }

        /// <summary>
        /// The weekly schedule
        /// </summary>
        public WeeklySchedule? WeeklySchedule { get; set; }

        /// <summary>
        /// The user
        /// </summary>
        public EmbeddedUserEntity? User { get; set; }

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
    public abstract class EmbeddedStaffMemberEntity : BaseEmbeddedEntity, IUserIdentifiable<ObjectId>, IDepartmentIdentifiable<ObjectId>
    {
        #region Public Properties

        /// <summary>
        /// The user id
        /// </summary>
        public ObjectId UserId { get; set; }

        /// <summary>
        /// The company id
        /// </summary>
        public ObjectId DepartmentId { get; set; }

        /// <summary>
        /// The weekly schedule
        /// </summary>
        public WeeklySchedule? WeeklySchedule { get; set; }

        /// <summary>
        /// The user
        /// </summary>
        public EmbeddedUserEntity? User { get; set; }

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
