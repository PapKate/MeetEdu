
using AutoMapper;

using MeetBase;

using MongoDB.Bson;

namespace MeetEdu
{
    /// <summary>
    /// Represents a department document in the MongoDB
    /// </summary>
    public class DepartmentEntity : StandardEntity, IImageable, INoteable, IUniversityIdentifiable<ObjectId>, IEmbeddedable<EmbeddedDepartmentEntity>
    {
        #region Private Members

        /// <summary>
        /// The member of the <see cref="Email"/> property
        /// </summary>
        private string? mEmail;

        /// <summary>
        /// The member of the <see cref="Note"/> property
        /// </summary>
        private string? mNote;

        /// <summary>
        /// The member of the <see cref="Quote"/> property
        /// </summary>
        private string? mQuote;

        /// <summary>
        /// The member of the <see cref="Description"/> property
        /// </summary>
        private string? mDescription;

        /// <summary>
        /// The member of the <see cref="LayoutDescription"/> property
        /// </summary>
        private string? mLayoutDescription;

        /// <summary>
        /// The member of the <see cref="SecretaryDescription"/> property
        /// </summary>
        private string? mSecretaryDescription;

        /// <summary>
        /// The member of the <see cref="Fields"/> property
        /// </summary>
        private IEnumerable<string>? mFields;

        /// <summary>
        /// The member of the <see cref="Websites"/> property
        /// </summary>
        private IEnumerable<Website>? mWebsites;

        /// <summary>
        /// The member of the <see cref="Labels"/> property
        /// </summary>
        private IEnumerable<EmbeddedLabelEntity>? mLabels;

        /// <summary>
        /// The member of the <see cref="Secretaries"/> property
        /// </summary>
        private IEnumerable<EmbeddedSecretaryEntity>? mSecretaries;

        #endregion

        #region Public Properties

        /// <summary>
        /// The university id
        /// </summary>
        public ObjectId UniversityId { get; set; }

        /// <summary>
        /// The related websites 
        /// </summary>
        public IEnumerable<Website> Websites
        {
            get => mWebsites ?? Enumerable.Empty<Website>();
            set => mWebsites = value;
        }

        /// <summary>
        /// The email
        /// </summary>
        public string Email
        {
            get => mEmail ?? string.Empty;
            set => mEmail = value;
        }

        /// <summary>
        /// The category
        /// </summary>
        public DepartmentType Category { get; set; }

        /// <summary>
        /// The fields of study
        /// </summary>
        public IEnumerable<string> Fields
        {
            get => mFields ?? Enumerable.Empty<string>();
            set => mFields = value;
        }

        /// <summary>
        /// The quote
        /// </summary>
        public string Quote
        {
            get => mQuote ?? string.Empty;
            set => mQuote = value;
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
        /// The layout description
        /// </summary>
        public string LayoutDescription
        {
            get => mLayoutDescription ?? string.Empty;
            set => mLayoutDescription = value;
        }

        /// <summary>
        /// The secretary description
        /// </summary>
        public string SecretaryDescription
        {
            get => mSecretaryDescription ?? string.Empty;
            set => mSecretaryDescription = value;
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
        /// The phone number
        /// </summary>
        public PhoneNumber? PhoneNumber { get; set; }

        /// <summary>
        /// The contact message template
        /// </summary>
        public DepartmentContactMessageTemplate? ContactMessageTemplate { get; set; }

        /// <summary>
        /// The labels
        /// </summary>
        public IEnumerable<EmbeddedLabelEntity> Labels
        {
            get => mLabels ?? Enumerable.Empty<EmbeddedLabelEntity>();
            set => mLabels = value;
        }

        /// <summary>
        /// The secretaries
        /// </summary>
        public IEnumerable<EmbeddedSecretaryEntity> Secretaries
        {
            get => mSecretaries ?? Enumerable.Empty<EmbeddedSecretaryEntity>();
            set => mSecretaries = value;
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
        public static DepartmentEntity FromRequestModel(DepartmentRequestModel model)
        {
            var entity = new DepartmentEntity();

            DI.Mapper.Map(model, entity);

            UpdateNonAutoMapperValues(model, entity);

            return entity;
        }

        /// <summary>
        /// Creates and returns a <see cref="DepartmentResponseModel"/> from the current <see cref="DepartmentEntity"/>
        /// </summary>
        /// <returns></returns>
        public DepartmentResponseModel ToResponseModel()
            => EntityHelpers.ToResponseModel<DepartmentResponseModel>(this);

        /// <summary>
        /// Updates the values of the specified <paramref name="entity"/> with the values of the specified <paramref name="model"/>.
        /// NOTE: This method only affects the properties that can't be mapped by the <see cref="Mapper"/> and are not null!
        /// </summary>
        /// <param name="model">The model</param>
        /// <param name="entity">The entity</param>
        /// <returns></returns>
        public static async void UpdateNonAutoMapperValues(DepartmentRequestModel model, DepartmentEntity entity)
        {
            if (!model.LabelIds.IsNullOrEmpty())
            {
                var formattedLabelIds = model.LabelIds.Select(l => l.ToObjectId()).ToList();
                var labels = await MeetEduDbMapper.UniversityLabels.SelectAsync(x => formattedLabelIds.Contains(x.Id));

                entity.Labels = labels.Select(l => l.ToEmbeddedEntity()).ToList();
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
    public class EmbeddedDepartmentEntity : EmbeddedStandardEntity, IImageable, IUniversityIdentifiable<ObjectId>
    {
        #region Public Properties

        /// <summary>
        /// The university id
        /// </summary>
        public ObjectId UniversityId { get; set; }

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
