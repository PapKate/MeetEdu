using AutoMapper;
using MongoDB.Bson;

namespace MeetEdu
{
    /// <summary>
    /// Represents a professor document in the MongoDB
    /// </summary>
    public class ProfessorEntity : StaffMemberEntity
    {
        #region Private Members

        /// <summary>
        /// The member of the <see cref="Field"/> property
        /// </summary>
        private string? mField;

        /// <summary>
        /// The member of the <see cref="ResearchInterests"/> property
        /// </summary>
        private string? mResearchInterests;

        /// <summary>
        /// The member of the <see cref="Websites"/> property
        /// </summary>
        private IEnumerable<Uri>? mWebsites;

        #endregion

        #region Public Properties

        /// <summary>
        /// The personal websites 
        /// </summary>
        public IEnumerable<Uri> Websites 
        { 
            get => mWebsites ?? Enumerable.Empty<Uri>();
            set => mWebsites = value;
        }

        /// <summary>
        /// The field of study
        /// </summary>
        public string Field
        {
            get => mField ?? string.Empty;
            set => mField = value;
        }

        /// <summary>
        /// The research interests
        /// </summary>
        public string ResearchInterests
        {
            get => mResearchInterests ?? string.Empty;
            set => mResearchInterests = value;
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public ProfessorEntity() : base()
        {

        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Creates and returns a <see cref="ProfessorEntity"/> from the specified <paramref name="model"/>
        /// </summary>
        /// <param name="model">The model</param>
        /// <returns></returns>
        public static async Task<ProfessorEntity> FromRequestModelAsync(ProfessorRequestModel model)
        {
            var entity = new ProfessorEntity();

            DI.Mapper.Map(model, entity);

            entity.User = !model.UserId.IsNullOrEmpty() ? await EntityHelpers.GetUserAsync(model.UserId) : null;

            return entity;
        }

        /// <summary>
        /// Creates and returns a <see cref="ProfessorResponseModel"/> from the current <see cref="ProfessorEntity"/>
        /// </summary>
        /// <returns></returns>
        public ProfessorResponseModel ToResponseModel()
            => EntityHelpers.ToResponseModel<ProfessorResponseModel>(this);

        /// <summary>
        /// Creates and returns a <see cref="EmbeddedProfessorEntity"/> from the current <see cref="ProfessorEntity"/>
        /// </summary>
        /// <returns></returns>
        public EmbeddedProfessorEntity ToEmbeddedEntity()
            => EntityHelpers.ToEmbeddedEntity<EmbeddedProfessorEntity>(this);

        #endregion
    }

    /// <summary>
    /// A minimal version of the <see cref="ProfessorEntity"/> that contains the fields that are 
    /// more frequently used when embedding documents in the MongoDB
    /// </summary>
    public class EmbeddedProfessorEntity : EmbeddedStaffMemberEntity
    {
        #region Private Members

        /// <summary>
        /// The member of the <see cref="Field"/> property
        /// </summary>
        private string? mField;

        #endregion

        #region Public Properties

        /// <summary>
        /// The field of study
        /// </summary>
        public string Field
        {
            get => mField ?? string.Empty;
            set => mField = value;
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public EmbeddedProfessorEntity() : base()
        {

        }

        #endregion
    }
}
