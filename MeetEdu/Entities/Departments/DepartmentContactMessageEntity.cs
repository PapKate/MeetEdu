using MongoDB.Bson;

namespace MeetEdu
{
    /// <summary>
    /// Represents a department contact message document in the MongoDB
    /// </summary>
    public class DepartmentContactMessageEntity : BaseContactEntity, IEmbeddedable<EmbeddedDepartmentContactMessageEntity>, IDepartmentIdentifiable<ObjectId>
    {
        #region Public Properties

        /// <summary>
        /// The department id
        /// </summary>
        public ObjectId DepartmentId { get; set; }

        /// <summary>
        /// The role
        /// </summary>
        public SecretaryRole Role { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public DepartmentContactMessageEntity() : base()
        {

        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Creates and returns a <see cref="DepartmentContactMessageEntity"/> from the specified <paramref name="model"/>
        /// </summary>
        /// <param name="model">The model</param>
        /// <param name="departmentId">The department id</param>
        /// <returns></returns>
        public static DepartmentContactMessageEntity FromRequestModel(DepartmentContactMessageRequestModel model, ObjectId departmentId)
        {
            var entity = new DepartmentContactMessageEntity();

            DI.Mapper.Map(model, entity);
            entity.DepartmentId = departmentId;
            return entity;
        }

        /// <summary>
        /// Creates and returns a <see cref="DepartmentContactMessageResponseModel"/> from the current <see cref="DepartmentContactMessageEntity"/>
        /// </summary>
        /// <returns></returns>
        public DepartmentContactMessageResponseModel ToResponseModel()
            => EntityHelpers.ToResponseModel<DepartmentContactMessageResponseModel>(this);

        /// <summary>
        /// Creates and returns a <see cref="EmbeddedDepartmentContactMessageEntity"/> from the current <see cref="DepartmentContactMessageEntity"/>
        /// </summary>
        /// <returns></returns>
        public EmbeddedDepartmentContactMessageEntity ToEmbeddedEntity()
            => EntityHelpers.ToEmbeddedEntity<EmbeddedDepartmentContactMessageEntity>(this);

        #endregion
    }

    /// <summary>
    /// A minimal version of the <see cref="DepartmentContactMessageEntity"/> that contains the fields that are 
    /// more frequently used when embedding documents in the MongoDB
    /// </summary>
    public class EmbeddedDepartmentContactMessageEntity : EmbeddedBaseContactEntity
    {
        #region Public Properties

        /// <summary>
        /// The company id
        /// </summary>
        public ObjectId DepartmentId { get; set; }

        /// <summary>
        /// The role
        /// </summary>
        public SecretaryRole Role { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public EmbeddedDepartmentContactMessageEntity() : base()
        {

        }

        #endregion
    }

}
