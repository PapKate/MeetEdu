using MongoDB.Bson;

namespace AppointMate
{
    /// <summary>
    /// Represents a customer point offset log document in the MongoDB
    /// </summary>
    public class UserPointOffsetLogEntity : BaseEntity, IDateCreatable, INoteable, IOffsetable, IUserIdentifiable<ObjectId>
    {
        #region Private Members

        /// <summary>
        /// The member of the <see cref="Note"/> property
        /// </summary>
        private string? mNote;

        #endregion

        #region Public Properties

        /// <summary>
        /// The user id
        /// </summary>
        public ObjectId UserId { get; set; }

        /// <summary>
        /// The creation date
        /// </summary>
        public DateTimeOffset DateCreated { get; set; }

        /// <summary>
        /// The old points of the customer
        /// </summary>
        public uint OldPoints { get; set; }

        /// <summary>
        /// The offset
        /// </summary>
        public int Offset { get; set; }

        /// <summary>
        /// A flag indicating whether the offset was positive or not
        /// </summary>
        public bool IsPositive { get; set; }

        /// <summary>
        /// The new points of the customer
        /// </summary>
        public uint NewPoints { get; set; }

        /// <summary>
        /// The note
        /// </summary>
        public string Note
        {
            get => mNote ?? string.Empty;
            set => mNote = value;
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public UserPointOffsetLogEntity() : base()
        {

        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Creates and returns a <see cref="UserPointOffsetLogEntity"/> from the specified <paramref name="model"/>
        /// </summary>
        /// <param name="userId">The user id</param>
        /// <param name="model">The model</param>
        /// <returns></returns>
        public static UserPointOffsetLogEntity FromRequestModel(string userId, CustomerPointOffsetLogRequestModel model)
        {
            var entity = new UserPointOffsetLogEntity();

            DI.Mapper.Map(model, entity);
            entity.IsPositive = model.Offset > 0;
            entity.DateCreated = DateTimeOffset.Now;
            entity.UserId = userId.ToObjectId();

            return entity;
        }

        /// <summary>
        /// Creates and returns a <see cref="CustomerPointOffsetLogResponseModel"/> from the current <see cref="UserPointOffsetLogEntity"/>
        /// </summary>
        /// <returns></returns>
        public CustomerPointOffsetLogResponseModel ToResponseModel()
            => EntityHelpers.ToResponseModel<CustomerPointOffsetLogResponseModel>(this);

        /// <summary>
        /// Returns a string that represents the current object
        /// </summary>
        /// <returns></returns>
        public override string ToString() => $"Note: {Note}, Offset: {Offset}";

        #endregion
    }
}
