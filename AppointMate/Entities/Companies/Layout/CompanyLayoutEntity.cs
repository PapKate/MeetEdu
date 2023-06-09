using MongoDB.Bson;

namespace AppointMate
{
    /// <summary>
    /// Represents a company layout document in the MongoDB
    /// </summary>
    public class CompanyLayoutEntity : DateEntity, IDescriptable, ICompanyIdentifiable<ObjectId>
    {
        #region Private Members

        /// <summary>
        /// The member of the <see cref="Description"/> property
        /// </summary>
        private string? mDescription;

        /// <summary>
        /// The member of the <see cref="Rooms"/> property
        /// </summary>
        private IEnumerable<CompanyRoomEntity>? mRooms;

        #endregion

        #region Public Properties

        /// <summary>
        /// The company id
        /// </summary>
        public ObjectId CompanyId { get; set; }

        /// <summary>
        /// The description
        /// </summary>
        public string Description
        {
            get => mDescription ?? string.Empty;
            set => mDescription = value;
        }

        /// <summary>
        /// The rooms
        /// </summary>
        public IEnumerable<CompanyRoomEntity> Rooms
        {
            get => mRooms ?? Enumerable.Empty<CompanyRoomEntity>();
            set => mRooms = value;
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public CompanyLayoutEntity() : base()
        {

        }

        #endregion
    }
}
