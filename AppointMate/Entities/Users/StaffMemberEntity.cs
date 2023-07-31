using AutoMapper;

using MongoDB.Bson;
using MongoDB.Driver;

namespace AppointMate
{
    /// <summary>
    /// Represents a staff member document in the MongoDB
    /// </summary>
    public class StaffMemberEntity : DateEntity, IUserIdentifiable<ObjectId>, ICompanyIdentifiable<ObjectId>
    {
        #region Private Members

        /// <summary>
        /// The member of the <see cref="Quote"/> property
        /// </summary>
        private string? mQuote;

        /// <summary>
        /// The member of the <see cref="Labels"/> property
        /// </summary>
        private IEnumerable<EmbeddedLabelEntity>? mLabels;

        #endregion

        #region Public Properties

        /// <summary>
        /// The id of the user
        /// </summary>
        public ObjectId UserId { get; set; }

        /// <summary>
        /// The company id
        /// </summary>
        public ObjectId CompanyId { get; set; }

        /// <summary>
        /// The quote
        /// </summary>
        public string Quote
        {
            get => mQuote ?? string.Empty;
            set => mQuote = value;
        }

        /// <summary>
        /// A flag indicating whether they are an owner
        /// </summary>
        public bool IsOwner { get; set; }

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
        public StaffMemberEntity() : base()
        {

        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Creates and returns a <see cref="StaffMemberEntity"/> from the specified <paramref name="model"/>
        /// </summary>
        /// <param name="companyId"></param>
        /// <param name="userId"></param>
        /// <param name="model">The model</param>
        /// <returns></returns>
        public static StaffMemberEntity FromRequestModel(ObjectId companyId, ObjectId userId, StaffMemberRequestModel model)
        {
            var entity = new StaffMemberEntity();

            DI.Mapper.Map(model, entity);
            entity.UserId = userId;
            entity.CompanyId = companyId;

            UpdateNonAutoMapperValues(model, entity);

            return entity;
        }

        /// <summary>
        /// Creates and returns a <see cref="StaffMemberResponseModel"/> from the current <see cref="StaffMemberEntity"/>
        /// </summary>
        /// <returns></returns>
        public StaffMemberResponseModel ToResponseModel()
            => EntityHelpers.ToResponseModel<StaffMemberResponseModel>(this);

        /// <summary>
        /// Updates the values of the specified <paramref name="entity"/> with the values of the specified <paramref name="model"/>.
        /// NOTE: This method only affects the properties that can't be mapped by the <see cref="Mapper"/> and are not null!
        /// </summary>
        /// <param name="model">The model</param>
        /// <param name="entity">The entity</param>
        /// <returns></returns>
        public static async void UpdateNonAutoMapperValues(StaffMemberRequestModel model, StaffMemberEntity entity)
        {
            // If there are labels...
            if(model.Labels is null)
                // Return
                return;

            var labels = await AppointMateDbMapper.StaffMemberLabels.SelectAsync(x => model.Labels.Any(y => y == x.Id.ToString()));

            entity.Labels = labels.Select(x => x.ToEmbeddedEntity()); 
        }

        /// <summary>
        /// Creates and returns a <see cref="EmbeddedStaffMemberEntity"/> from the current <see cref="StaffMemberEntity"/>
        /// </summary>
        /// <returns></returns>
        public EmbeddedStaffMemberEntity ToEmbeddedEntity()
            => EntityHelpers.ToEmbeddedEntity<EmbeddedStaffMemberEntity>(this);

        #endregion
    }

    /// <summary>
    /// A minimal version of the <see cref="StaffMemberEntity"/> that contains the fields that are 
    /// more frequently used when embedding documents in the MongoDB
    /// </summary>
    public class EmbeddedStaffMemberEntity : EmbeddedBaseEntity
    {
        #region Private Members

        /// <summary>
        /// The member of the <see cref="Quote"/> property
        /// </summary>
        private string? mQuote;

        /// <summary>
        /// The member of the <see cref="Labels"/> property
        /// </summary>
        private IEnumerable<string>? mLabels;

        #endregion

        #region Public Properties

        /// <summary>
        /// The id of the user
        /// </summary>
        public ObjectId UserId { get; set; }

        /// <summary>
        /// The company id
        /// </summary>
        public ObjectId CompanyId { get; set; }

        /// <summary>
        /// The quote
        /// </summary>
        public string Quote
        {
            get => mQuote ?? string.Empty;
            set => mQuote = value;
        }

        /// <summary>
        /// A flag indicating whether they are an owner
        /// </summary>
        public bool IsOwner { get; set; }

        /// <summary>
        /// The labels
        /// </summary>
        public IEnumerable<string> Labels
        {
            get => mLabels ?? Enumerable.Empty<string>();
            set => mLabels = value;
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public EmbeddedStaffMemberEntity() : base()
        {

        }

        #endregion
    }
}
