
using AutoMapper;

namespace AppointMate
{
    /// <summary>
    /// Represents a company document in the MongoDB
    /// </summary>
    public class CompanyEntity : StandardEntity, IImageable, INoteable
    {
        #region Private Members

        /// <summary>
        /// The member of the <see cref="Note"/> property
        /// </summary>
        private string? mNote;

        /// <summary>
        /// The member of the <see cref="Categories"/> property
        /// </summary>
        private IEnumerable<CompanyType>? mCategories;

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
        /// The categories
        /// </summary>
        public IEnumerable<CompanyType> Categories
        {
            get => mCategories ?? Enumerable.Empty<CompanyType>();
            set => mCategories = value;
        }

        /// <summary>
        /// The description
        /// </summary>
        public string Description
        {
            get => mDescription ?? string.Empty;
            set => mDescription = value;
        }

        /// <summary>
        /// A flag indicating whether the company provides at home services
        /// </summary>
        public bool HasAtHomeServices { get; set; }

        /// <summary>
        /// The radius for the distance where at home services can be provided in Km
        /// </summary>
        public double AtHomeRadius { get; set; }

        /// <summary>
        /// The note
        /// </summary>
        public string Note
        {
            get => mNote ?? string.Empty;
            set => mNote = value;
        }

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

        /// <summary>
        /// The average number of stars from the customer reviews
        /// </summary>
        public double TotalReviewStars { get; set; }

        /// <summary>
        /// The number of customer reviews
        /// </summary>
        public uint TotalReviews { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public CompanyEntity() : base()
        {

        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Creates and returns a <see cref="CompanyEntity"/> from the specified <paramref name="model"/>
        /// </summary>
        /// <param name="model">The model</param>
        /// <returns></returns>
        public static CompanyEntity FromRequestModel(CompanyRequestModel model)
        {
            var entity = new CompanyEntity();

            DI.Mapper.Map(model, entity);

            UpdateNonAutoMapperValues(model, entity);

            return entity;
        }

        /// <summary>
        /// Creates and returns a <see cref="CompanyResponseModel"/> from the current <see cref="CompanyEntity"/>
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
        public static async void UpdateNonAutoMapperValues(CompanyRequestModel model, CompanyEntity entity)
        {
            var reviews = await AppointMateDbMapper.CustomerServiceReviews.SelectAsync(x => x.CompanyId == entity.Id);

            // If there are reviews...
            if(reviews is not null)
            {
                var reviewsCount = (uint)reviews.Count();
                entity.TotalReviewStars = double.Round(reviews.Sum(x => x.NumberOfStars) / reviewsCount, 2);
                entity.TotalReviews = reviewsCount;
            }

            // If there are labels...
            if (model.Labels is not null)
            {
                var labels = await AppointMateDbMapper.CompanyLabels.SelectAsync(x => model.Labels.Any(y => y == x.Id.ToString()));

                entity.Labels = labels.Select(x => x.ToEmbeddedEntity());
            }
        }

        /// <summary>
        /// Creates and returns a <see cref="EmbeddedCompanyEntity"/> from the current <see cref="CompanyEntity"/>
        /// </summary>
        /// <returns></returns>
        public EmbeddedCompanyEntity ToEmbeddedEntity()
            => EntityHelpers.ToEmbeddedEntity<EmbeddedCompanyEntity>(this);

        #endregion
    }

    /// <summary>
    /// The embedded company
    /// </summary>
    public class EmbeddedCompanyEntity : EmbeddedStandardEntity
    {
        #region Private Members

        /// <summary>
        /// The member of the <see cref="Categories"/> property
        /// </summary>
        private IEnumerable<CompanyType>? mCategories;

        #endregion

        #region Public Properties

        /// <summary>
        /// The categories
        /// </summary>
        public IEnumerable<CompanyType> Categories
        {
            get => mCategories ?? Enumerable.Empty<CompanyType>();
            set => mCategories = value;
        }

        /// <summary>
        /// The average number of stars from the customer reviews
        /// </summary>
        public double TotalReviewStars { get; set; }

        /// <summary>
        /// The number of customer reviews
        /// </summary>
        public uint TotalReviews { get; set; }

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
        public EmbeddedCompanyEntity() : base()
        {
            
        }

        #endregion
    }
}
