using AutoMapper;
using MongoDB.Bson;
using MongoDB.Driver;

namespace AppointMate
{
    /// <summary>
    /// Represents a member document in the MongoDB
    /// </summary>
    public class MemberEntity: DateEntity, IUserIdentifiable<ObjectId>
    {
        #region Public Properties

        /// <summary>
        /// The id of the user
        /// </summary>
        public ObjectId UserId { get; set; }

        /// <summary>
        /// The total number of appointments
        /// </summary>
        public uint TotalAppointments { get; set; }

        /// <summary>
        /// The total number of saved companies
        /// </summary>
        public uint TotalSavedCompanies { get; set; }

        /// <summary>
        /// The total number of saved professors
        /// </summary>
        public uint TotalSavedProfessors { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public MemberEntity() : base()
        {

        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Creates and returns a <see cref="MemberEntity"/> from the specified <paramref name="model"/>
        /// </summary>
        /// <param name="model">The model</param>
        /// <returns></returns>
        public static MemberEntity FromRequestModel(CustomerRequestModel model)
        {
            var entity = new MemberEntity();

            DI.Mapper.Map(model, entity);

            return entity;
        }

        /// <summary>
        /// Creates and returns a <see cref="CustomerResponseModel"/> from the current <see cref="MemberEntity"/>
        /// </summary>
        /// <returns></returns>
        public CustomerResponseModel ToResponseModel()
            => EntityHelpers.ToResponseModel<CustomerResponseModel>(this);

        /// <summary>
        /// Updates the values of the specified <paramref name="entity"/> with the values of the specified <paramref name="model"/>.
        /// NOTE: This method only affects the properties that can't be mapped by the <see cref="Mapper"/> and are not null!
        /// </summary>
        /// <param name="model">The model</param>
        /// <param name="entity">The entity</param>
        /// <returns></returns>
        public static async void UpdateNonAutoMapperValues(CustomerRequestModel model, MemberEntity entity)
        {
            var customerSessions = await AppointMateDbMapper.CustomerServiceSessions.SelectAsync(x => x.CustomerId == entity.Id);
            entity.TotalAppointments = (uint)customerSessions.Count();

            var customerFavoriteCompanies = await AppointMateDbMapper.UserFavoriteCompanies.SelectAsync(x => x.UserId == entity.Id);
            entity.TotalSavedCompanies = (uint)customerSessions.Count();
            
            var customerReviews = await AppointMateDbMapper.CustomerServiceReviews.SelectAsync(x => x.CustomerId == entity.Id);
            entity.TotalSavedProfessors = (uint)customerReviews.Count();
        }

        /// <summary>
        /// Creates and returns a <see cref="EmbeddedMemberEntity"/> from the current <see cref="MemberEntity"/>
        /// </summary>
        /// <returns></returns>
        public EmbeddedMemberEntity ToEmbeddedEntity()
            => EntityHelpers.ToEmbeddedEntity<EmbeddedMemberEntity>(this);

        #endregion
    }

    /// <summary>
    /// A minimal version of the <see cref="MemberEntity"/> that contains the fields that are 
    /// more frequently used when embedding documents in the MongoDB
    /// </summary>
    public class EmbeddedMemberEntity : EmbeddedBaseEntity, IUserIdentifiable<ObjectId>
    {
        #region Public Properties

        /// <summary>
        /// The id of the user
        /// </summary>
        public ObjectId UserId { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public EmbeddedMemberEntity() : base()
        {

        }

        #endregion
    }
}
