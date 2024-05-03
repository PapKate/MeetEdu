using AutoMapper;

namespace MeetEdu
{
    /// <summary>
    /// Represents a secretary document in the MongoDB
    /// </summary>
    public class SecretaryEntity : StaffMemberEntity, IEmbeddedable<EmbeddedSecretaryEntity>
    {
        #region Public Properties

        /// <summary>
        /// The role
        /// </summary>
        public SecretaryRole Role { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public SecretaryEntity() : base()
        {

        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Creates and returns a <see cref="SecretaryEntity"/> from the specified <paramref name="model"/>
        /// </summary>
        /// <param name="model">The model</param>
        /// <returns></returns>
        public static async Task<SecretaryEntity> FromRequestModelAsync(SecretaryRequestModel model)
        {
            var entity = new SecretaryEntity();

            DI.Mapper.Map(model, entity);

            entity = await UpdateNonAutoMapperValuesAsync(model, entity);

            return entity;
        }

        /// <summary>
        /// Updates the values of the specified <paramref name="entity"/> with the values of the specified <paramref name="model"/>.
        /// NOTE: This method only affects the properties that can't be mapped by the <see cref="Mapper"/> and are not null!
        /// </summary>
        /// <param name="model">The model</param>
        /// <param name="entity">The entity</param>
        /// <returns></returns>
        public static async Task<SecretaryEntity> UpdateNonAutoMapperValuesAsync(SecretaryRequestModel model, SecretaryEntity entity)
        {
            entity.User = !model.UserId.IsNullOrEmpty() ? await EntityHelpers.GetUserAsync(model.UserId) : null;
            entity.Department = !model.DepartmentId.IsNullOrEmpty() ? await EntityHelpers.GetDepartmetAsync(model.DepartmentId) : null;

            return entity;
        }

        /// <summary>
        /// Creates and returns a <see cref="SecretaryResponseModel"/> from the current <see cref="SecretaryEntity"/>
        /// </summary>
        /// <returns></returns>
        public SecretaryResponseModel ToResponseModel()
            => EntityHelpers.ToResponseModel<SecretaryResponseModel>(this);

        /// <summary>
        /// Creates and returns a <see cref="EmbeddedSecretaryEntity"/> from the current <see cref="SecretaryEntity"/>
        /// </summary>
        /// <returns></returns>
        public EmbeddedSecretaryEntity ToEmbeddedEntity()
            => EntityHelpers.ToEmbeddedEntity<EmbeddedSecretaryEntity>(this);

        #endregion
    }

    /// <summary>
    /// A minimal version of the <see cref="SecretaryEntity"/> that contains the fields that are 
    /// more frequently used when embedding documents in the MongoDB
    /// </summary>
    public class EmbeddedSecretaryEntity : EmbeddedStaffMemberEntity
    {
        #region Public Properties

        /// <summary>
        /// The role
        /// </summary>
        public SecretaryRole Role { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public EmbeddedSecretaryEntity() : base()
        {

        }

        #endregion
    }
}
