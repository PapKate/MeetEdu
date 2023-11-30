using MongoDB.Bson;

namespace MeetEdu
{
    /// <summary>
    /// Represents a company layout document in the MongoDB
    /// </summary>
    public class DepartmentLayoutEntity : StandardEntity, IDescriptable, INoteable, IDepartmentIdentifiable<ObjectId>
    {
        #region Private Members

        /// <summary>
        /// The member of the <see cref="Description"/> property
        /// </summary>
        private string? mDescription;

        /// <summary>
        /// The member of the <see cref="Note"/> property
        /// </summary>
        private string? mNote;

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
        /// The note
        /// </summary>
        public string Note
        {
            get => mNote ?? string.Empty;
            set => mNote = value;
        }

        /// <summary>
        /// The image URL
        /// </summary>
        public Uri? ImageUrl { get; set; }

        /// <summary>
        /// The display theme
        /// </summary>
        public RoomDisplayTheme DisplayTheme { get; set; }

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
