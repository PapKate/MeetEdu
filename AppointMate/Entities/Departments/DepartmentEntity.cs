
using AutoMapper;

namespace AppointMate
{
    /// <summary>
    /// Represents a department document in the MongoDB
    /// </summary>
    public class DepartmentEntity : StandardEntity, IImageable, INoteable
    {
        #region Private Members

        /// <summary>
        /// The member of the <see cref="Note"/> property
        /// </summary>
        private string? mNote;

        /// <summary>
        /// The member of the <see cref="Category"/> property
        /// </summary>
        private IEnumerable<DepartmentType>? mCategories;

        /// <summary>
        /// The member of the <see cref="Description"/> property
        /// </summary>
        private string? mDescription;

        /// <summary>
        /// The member of the <see cref="Labels"/> property
        /// </summary>
        private IEnumerable<EmbeddedLabelEntity>? mLabels;

        #endregion

        #region Public Properties

        /// <summary>
        /// The category
        /// </summary>
        public DepartmentType Category { get; set; }

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
        /// The number of staff members
        /// </summary>
        public uint TotalStaffMembers { get; set; }

        /// <summary>
        /// The work hours
        /// </summary>
        public WeeklySchedule? WorkHours { get; set; }

        /// <summary>
        /// The image URL
        /// </summary>
        public Uri? ImageUrl { get; set; }

        /// <summary>
        /// The location
        /// </summary>
        public Location? Location { get; set; }

        /// <summary>
        /// The billing information
        /// </summary>
        public Billing? Billing { get; set; }

        /// <summary>
        /// The shipping information
        /// </summary>
        public Shipping? Shipping { get; set; }

        /// <summary>
        /// The labels
        /// </summary>
        public IEnumerable<EmbeddedLabelEntity> Labels
        {
            get => mLabels ?? Enumerable.Empty<EmbeddedLabelEntity>();
            set => mLabels = value;
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public DepartmentEntity() : base()
        {

        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Creates and returns a <see cref="DepartmentEntity"/> from the specified <paramref name="model"/>
        /// </summary>
        /// <param name="model">The model</param>
        /// <returns></returns>
        public static DepartmentEntity FromRequestModel(CompanyRequestModel model)
        {
            var entity = new DepartmentEntity();

            DI.Mapper.Map(model, entity);

            UpdateNonAutoMapperValues(model, entity);

            return entity;
        }

        /// <summary>
        /// Creates and returns a <see cref="CompanyResponseModel"/> from the current <see cref="DepartmentEntity"/>
        /// </summary>
        /// <returns></returns>
        public CompanyResponseModel ToResponseModel()
            => EntityHelpers.ToResponseModel<CompanyResponseModel>(this);

        /// <summary>
        /// Updates the values of the specified <paramref name="entity"/> with the values of the specified <paramref name="model"/>.
        /// NOTE: This method only affects the properties that can't be mapped by the <see cref="Mapper"/> and are not null!
        /// </summary>
        /// <param name="model">The model</param>
        /// <param name="entity">The entity</param>
        /// <returns></returns>
        public static async void UpdateNonAutoMapperValues(CompanyRequestModel model, DepartmentEntity entity)
        {
            var reviews = await AppointMateDbMapper.CustomerServiceReviews.SelectAsync(x => x.CompanyId == entity.Id);

            // If there are reviews...
            if (reviews is not null)
            {
                var reviewsCount = (uint)reviews.Count();
                entity.TotalStaffMembers = reviewsCount;
            }

            // If there are labels...
            if (model.Labels is not null)
            {
                var labels = await AppointMateDbMapper.CompanyLabels.SelectAsync(x => model.Labels.Any(y => y == x.Id.ToString()));

                entity.Labels = labels.Select(x => x.ToEmbeddedEntity());
            }
        }

        /// <summary>
        /// Creates and returns a <see cref="EmbeddedDepartmentEntity"/> from the current <see cref="DepartmentEntity"/>
        /// </summary>
        /// <returns></returns>
        public EmbeddedDepartmentEntity ToEmbeddedEntity()
            => EntityHelpers.ToEmbeddedEntity<EmbeddedDepartmentEntity>(this);

        #endregion
    }

    /// <summary>
    /// A minimal version of the <see cref="DepartmentEntity"/> that contains the fields that are 
    /// more frequently used when embedding documents in the MongoDB
    /// </summary>
    public class EmbeddedDepartmentEntity : EmbeddedStandardEntity, IImageable
    {
        #region Private Members

        /// <summary>
        /// The member of the <see cref="Categories"/> property
        /// </summary>
        private IEnumerable<DepartmentType>? mCategories;

        #endregion

        #region Public Properties

        /// <summary>
        /// The number of staff members
        /// </summary>
        public uint TotalStaffMembers { get; set; }

        /// <summary>
        /// The image URL
        /// </summary>
        public Uri? ImageUrl { get; set; }

        /// <summary>
        /// The location
        /// </summary>
        public Location? Location { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public EmbeddedDepartmentEntity() : base()
        {
            
        }

        #endregion
    }
}
