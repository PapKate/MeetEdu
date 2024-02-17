using MongoDB.Bson;

namespace MeetEdu
{
    /// <summary>
    /// Represents a department layout document in the MongoDB
    /// </summary>
    public class DepartmentLayoutEntity : LayoutEntity, IDepartmentIdentifiable<ObjectId>
    {
        #region Public Properties

        /// <summary>
        /// The company id
        /// </summary>
        public ObjectId DepartmentId { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public DepartmentLayoutEntity() : base()
        {

        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Creates and returns a <see cref="DepartmentLayoutEntity"/> from the specified <paramref name="model"/>
        /// </summary>
        /// <param name="model">The model</param>
        /// <param name="companyId">The company id</param>
        /// <returns></returns>
        public static DepartmentLayoutEntity FromRequestModel(DepartmentLayoutRequestModel model, ObjectId companyId)
        {
            var entity = new DepartmentLayoutEntity();

            DI.Mapper.Map(model, entity);
            entity.DepartmentId = companyId;
            return entity;
        }

        /// <summary>
        /// Creates and returns a <see cref="DepartmentLayoutResponseModel"/> from the current <see cref="DepartmentLayoutEntity"/>
        /// </summary>
        /// <returns></returns>
        public DepartmentLayoutResponseModel ToResponseModel()
            => EntityHelpers.ToResponseModel<DepartmentLayoutResponseModel>(this);

        #endregion
    }
}
