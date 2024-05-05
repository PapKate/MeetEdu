using MongoDB.Bson;

namespace MeetEdu
{
    /// <summary>
    /// Represents a professor contact message document in the MongoDB
    /// </summary>
    public class ProfessorContactMessageEntity : BaseContactEntity, IEmbeddedable<EmbeddedProfessorContactMessageEntity>, IProfessorIdentifiable<ObjectId>
    {
        #region Public Properties

        /// <summary>
        /// The professor id
        /// </summary>
        public ObjectId ProfessorId { get; set; }

        /// <summary>
        /// The professor
        /// </summary>
        public EmbeddedProfessorEntity? Professor { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public ProfessorContactMessageEntity() : base()
        {

        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Creates and returns a <see cref="ProfessorContactMessageEntity"/> from the specified <paramref name="model"/>
        /// </summary>
        /// <param name="model">The model</param>
        /// <param name="professorId">The professor id</param>
        /// <returns></returns>
        public static ProfessorContactMessageEntity FromRequestModel(ProfessorContactMessageRequestModel model, ObjectId professorId)
        {
            var entity = new ProfessorContactMessageEntity();

            DI.Mapper.Map(model, entity);
            entity.ProfessorId = professorId;
            return entity;
        }

        /// <summary>
        /// Creates and returns a <see cref="ProfessorContactMessageResponseModel"/> from the current <see cref="ProfessorContactMessageEntity"/>
        /// </summary>
        /// <returns></returns>
        public ProfessorContactMessageResponseModel ToResponseModel()
            => EntityHelpers.ToResponseModel<ProfessorContactMessageResponseModel>(this);

        /// <summary>
        /// Creates and returns a <see cref="EmbeddedProfessorContactMessageEntity"/> from the current <see cref="ProfessorContactMessageEntity"/>
        /// </summary>
        /// <returns></returns>
        public EmbeddedProfessorContactMessageEntity ToEmbeddedEntity()
            => EntityHelpers.ToEmbeddedEntity<EmbeddedProfessorContactMessageEntity>(this);

        #endregion
    }

    /// <summary>
    /// A minimal version of the <see cref="ProfessorContactMessageEntity"/> that contains the fields that are 
    /// more frequently used when embedding documents in the MongoDB
    /// </summary>
    public class EmbeddedProfessorContactMessageEntity : EmbeddedBaseContactEntity
    {
        #region Public Properties

        /// <summary>
        /// The professor id
        /// </summary>
        public ObjectId ProfessorId { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public EmbeddedProfessorContactMessageEntity() : base()
        {

        }

        #endregion
    }
}
