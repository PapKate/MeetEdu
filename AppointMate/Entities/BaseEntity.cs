using MongoDB.Bson;

namespace AppointMate
{
    public class BaseEntity : IIdentifiable<ObjectId>
    {
        #region Public Properties

        /// <summary>
        /// The id
        /// </summary>
        public ObjectId Id { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public BaseEntity() : base()
        {

        }

        #endregion
    }

    /// <summary>
    /// The base for all the embedded entities
    /// </summary>
    public class BaseEmbeddedEntity : BaseEntity, IEmbeddableIdentifiable<ObjectId>
    {
        #region Public Properties

        /// <summary>
        /// The id of the entity that was used for creating the current 
        /// </summary>
        public ObjectId Source { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public BaseEmbeddedEntity() : base()
        {

        }

        #endregion
    }
}
