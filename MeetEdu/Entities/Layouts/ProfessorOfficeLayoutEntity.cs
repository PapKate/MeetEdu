using MongoDB.Bson;

namespace MeetEdu
{
    /// <summary>
    /// Represents a department layout document in the MongoDB
    /// </summary>
    public class ProfessorOfficeLayoutEntity : LayoutEntity, IProfessorIdentifiable<ObjectId>
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
        public ProfessorOfficeLayoutEntity() : base()
        {

        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Creates and returns a <see cref="ProfessorOfficeLayoutEntity"/> from the specified <paramref name="model"/>
        /// </summary>
        /// <param name="model">The model</param>
        /// <param name="professorId">The company id</param>
        /// <returns></returns>
        public static ProfessorOfficeLayoutEntity FromRequestModel(ProfessorOfficeLayoutRequestModel model, ObjectId professorId)
        {
            var entity = new ProfessorOfficeLayoutEntity();

            DI.Mapper.Map(model, entity);
            entity.ProfessorId = professorId;
            return entity;
        }

        /// <summary>
        /// Creates and returns a <see cref="ProfessorOfficeLayoutResponseModel"/> from the current <see cref="ProfessorOfficeLayoutEntity"/>
        /// </summary>
        /// <returns></returns>
        public ProfessorOfficeLayoutResponseModel ToResponseModel()
            => EntityHelpers.ToResponseModel<ProfessorOfficeLayoutResponseModel>(this);

        #endregion
    }
}
