using Microsoft.AspNetCore.Identity;

using MongoDB.Bson;

namespace MeetEdu
{
    /// <summary>
    /// Represents a department contact message document in the MongoDB
    /// </summary>
    public class DepartmentContactMessageEntity : DateEntity, IDepartmentIdentifiable<ObjectId>, IMemberIdentifiable<ObjectId?>
    {
        #region Private Members

        /// <summary>
        /// The member of the <see cref="FirstName"/> property
        /// </summary>
        private string? mFirstName;

        /// <summary>
        /// The member of the <see cref="LastName"/> property
        /// </summary>
        private string? mLastName;

        /// <summary>
        /// The member of the <see cref="Message"/> property
        /// </summary>
        private string? mMessage;

        /// <summary>
        /// The member of the <see cref="Email"/> property
        /// </summary>
        private string? mEmail;

        #endregion

        #region Public Properties

        /// <summary>
        /// The department id
        /// </summary>
        public ObjectId DepartmentId { get; set; }

        /// <summary>
        /// The member id
        /// </summary>
        public ObjectId? MemberId { get; set; }

        /// <summary>
        /// The first name
        /// </summary>
        public string FirstName 
        { 
            get => mFirstName ?? string.Empty;
            set => mFirstName = value;
        }

        /// <summary>
        /// The last name
        /// </summary>
        public string LastName
        {
            get => mLastName ?? string.Empty;
            set => mLastName = value;
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
        /// The phone number
        /// </summary>
        public PhoneNumber? PhoneNumber { get; set; }

        /// <summary>
        /// The message
        /// </summary>
        public string Message
        {
            get => mMessage ?? string.Empty;
            set => mMessage = value;
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public DepartmentContactMessageEntity() : base()
        {

        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Creates and returns a <see cref="DepartmentContactMessageEntity"/> from the specified <paramref name="model"/>
        /// </summary>
        /// <param name="model">The model</param>
        /// <param name="departmentId">The department id</param>
        /// <returns></returns>
        public static DepartmentContactMessageEntity FromRequestModel(DepartmentContactMessageRequestModel model, ObjectId departmentId)
        {
            var entity = new DepartmentContactMessageEntity();

            DI.Mapper.Map(model, entity);
            entity.DepartmentId = departmentId;
            return entity;
        }

        /// <summary>
        /// Creates and returns a <see cref="DepartmentContactMessageResponseModel"/> from the current <see cref="DepartmentContactMessageEntity"/>
        /// </summary>
        /// <returns></returns>
        public DepartmentContactMessageResponseModel ToResponseModel()
            => EntityHelpers.ToResponseModel<DepartmentContactMessageResponseModel>(this);

        /// <summary>
        /// Creates and returns a <see cref="EmbeddedDepartmentContactMessageEntity"/> from the current <see cref="DepartmentContactMessageEntity"/>
        /// </summary>
        /// <returns></returns>
        public EmbeddedDepartmentContactMessageEntity ToEmbeddedEntity()
            => EntityHelpers.ToEmbeddedEntity<EmbeddedDepartmentContactMessageEntity>(this);

        #endregion
    }

    /// <summary>
    /// A minimal version of the <see cref="DepartmentContactMessageEntity"/> that contains the fields that are 
    /// more frequently used when embedding documents in the MongoDB
    /// </summary>
    public class EmbeddedDepartmentContactMessageEntity : BaseEmbeddedEntity
    {
        #region Private Members

        /// <summary>
        /// The member of the <see cref="Email"/> property
        /// </summary>
        private string? mEmail;

        #endregion

        #region Public Properties

        /// <summary>
        /// The company id
        /// </summary>
        public ObjectId DepartmentId { get; set; }

        /// <summary>
        /// The email
        /// </summary>
        public string Email
        {
            get => mEmail ?? string.Empty;
            set => mEmail = value;
        }

        /// <summary>
        /// The phone number
        /// </summary>
        public PhoneNumber? PhoneNumber { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public EmbeddedDepartmentContactMessageEntity() : base()
        {

        }

        #endregion
    }
}
