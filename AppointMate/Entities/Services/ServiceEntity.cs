using MongoDB.Bson;

using System.Runtime.InteropServices.ObjectiveC;

namespace AppointMate
{
    /// <summary>
    /// Represents a service document in the MongoDB
    /// </summary>
    public class ServiceEntity: StandardEntity, IDescriptable, INoteable
    {
        #region Private Members

        /// <summary>
        /// The member of the <see cref="Description"/> property
        /// </summary>
        private string? mDescription;

        /// <summary>
        /// The member of the <see cref="SmallDescription"/> property
        /// </summary>
        private string? mSmallDescription;

        /// <summary>
        /// The member of the <see cref="Note"/> property
        /// </summary>
        private string? mNote;

        /// <summary>
        /// The member of the <see cref="Categories"/> property
        /// </summary>
        private IEnumerable<EmbeddedCategoryEntity>? mCategories;

        #endregion

        #region Public Properties

        /// <summary>
        /// A flag indicating whether it is at home
        /// </summary>
        public bool IsAtHome { get; set; }

        /// <summary>
        /// The description
        /// </summary>
        public string Description
        {
            get => mDescription ?? string.Empty;
            set => mDescription = value;
        }

        /// <summary>
        /// The small description
        /// </summary>
        public string SmallDescription
        {
            get => mSmallDescription ?? string.Empty;
            set => mSmallDescription = value;
        }

        /// <summary>
        /// The number of sessions range
        /// </summary>
        public SessionsRange SessionsRange { get; set; }

        /// <summary>
        /// The duration range
        /// </summary>
        public DurationRange DurationRange { get; set; }

        /// <summary>
        /// A flag indicating whether it is on sale or not
        /// </summary>
        public bool IsOnSale { get; set; }

        /// <summary>
        /// The price
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// The price on sale
        /// </summary>
        public decimal OnSalePrice { get; set; }

        /// <summary>
        /// The regular price 
        /// </summary>
        public decimal RegularPrice { get; set; }

        /// <summary>
        /// The note
        /// </summary>
        public string Note
        {
            get => mNote ?? string.Empty;
            set => mNote = value;
        }

        /// <summary>
        /// The categories
        /// </summary>
        public IEnumerable<EmbeddedCategoryEntity> Categories
        {
            get => mCategories ?? Enumerable.Empty<EmbeddedCategoryEntity>();
            set => mCategories = value;
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public ServiceEntity() : base()
        {

        }

        #endregion
    }

    /// <summary>
    /// A minimal version of the <see cref="ServiceEntity"/> that contains the fields that are 
    /// more frequently used when embedding documents in the MongoDB
    /// </summary>
    public class EmbeddedServiceEntity : EmbeddedStandardEntity, ICompanyIdentifiable<ObjectId>
    {
        #region Private Members

        /// <summary>
        /// The member of the <see cref="SmallDescription"/> property
        /// </summary>
        private string? mSmallDescription;

        #endregion

        #region Public Properties

        /// <summary>
        /// The company id
        /// </summary>
        public ObjectId CompanyId { get; set; }

        /// <summary>
        /// The small description
        /// </summary>
        public string SmallDescription
        {
            get => mSmallDescription ?? string.Empty;
            set => mSmallDescription = value;
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public EmbeddedServiceEntity() : base()
        {

        }

        #endregion
    }
}
