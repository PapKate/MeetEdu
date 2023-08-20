using MongoDB.Bson;

namespace AppointMate
{
    /// <summary>
    /// Represents a company layout document in the MongoDB
    /// </summary>
    public class DepartmentLayoutEntity : DateEntity, IDescriptable, IDepartmentIdentifiable<ObjectId>
    {
        #region Private Members

        /// <summary>
        /// The member of the <see cref="Description"/> property
        /// </summary>
        private string? mDescription;

        /// <summary>
        /// The member of the <see cref="Rooms"/> property
        /// </summary>
        private IList<DepartmentLayoutDataModel>? mRooms;

        #endregion

        #region Public Properties

        /// <summary>
        /// The company id
        /// </summary>
        public ObjectId DepartmentId { get; set; }

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
        public IList<DepartmentLayoutDataModel> Rooms
        {
            get
            {
                if (mRooms is null)
                    mRooms = new List<DepartmentLayoutDataModel>();

                return mRooms;
            }
            set => mRooms = value;
        }

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
        public static DepartmentLayoutEntity FromRequestModel(CompanyLayoutRequestModel model, ObjectId companyId)
        {
            var entity = new DepartmentLayoutEntity();

            DI.Mapper.Map(model, entity);
            entity.DepartmentId = companyId;
            return entity;
        }

        /// <summary>
        /// Creates and returns a <see cref="CompanyLayoutResponseModel"/> from the current <see cref="DepartmentLayoutEntity"/>
        /// </summary>
        /// <returns></returns>
        public CompanyLayoutResponseModel ToResponseModel()
            => EntityHelpers.ToResponseModel<CompanyLayoutResponseModel>(this);

        #endregion
    }
}
