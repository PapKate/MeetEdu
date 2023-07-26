using Microsoft.AspNetCore.Identity;

using MongoDB.Bson;

namespace AppointMate
{
    /// <summary>
    /// Represents a company contact message document in the MongoDB
    /// </summary>
    public class CompanyContactMessageEntity : DateEntity, ICompanyIdentifiable<ObjectId>
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
        /// The company id
        /// </summary>
        public ObjectId CompanyId { get; set; }

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
        public CompanyContactMessageEntity() : base()
        {

        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Creates and returns a <see cref="CompanyContactMessageEntity"/> from the specified <paramref name="model"/>
        /// </summary>
        /// <param name="model">The model</param>
        /// <param name="companyId">The company id</param>
        /// <returns></returns>
        public static CompanyContactMessageEntity FromRequestModel(CompanyContactMessageRequestModel model, ObjectId companyId)
        {
            var entity = new CompanyContactMessageEntity();

            DI.Mapper.Map(model, entity);
            entity.CompanyId = companyId;
            return entity;
        }

        /// <summary>
        /// Creates and returns a <see cref="CompanyContactMessageResponseModel"/> from the current <see cref="CompanyContactMessageEntity"/>
        /// </summary>
        /// <returns></returns>
        public CompanyContactMessageResponseModel ToResponseModel()
            => EntityHelpers.ToResponseModel<CompanyContactMessageResponseModel>(this);

        #endregion
    }
}
