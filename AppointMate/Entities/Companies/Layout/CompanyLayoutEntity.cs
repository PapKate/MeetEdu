using AutoMapper;

using Microsoft.AspNetCore.Http;
using MongoDB.Bson;
using MongoDB.Driver;

using System.ComponentModel.Design;

namespace AppointMate
{
    /// <summary>
    /// Represents a company layout document in the MongoDB
    /// </summary>
    public class CompanyLayoutEntity : DateEntity, IDescriptable, ICompanyIdentifiable<ObjectId>
    {
        #region Private Members

        /// <summary>
        /// The member of the <see cref="Description"/> property
        /// </summary>
        private string? mDescription;

        /// <summary>
        /// The member of the <see cref="Rooms"/> property
        /// </summary>
        private IList<CompanyLayoutRoomDataModel>? mRooms;

        #endregion

        #region Public Properties

        /// <summary>
        /// The company id
        /// </summary>
        public ObjectId CompanyId { get; set; }

        /// <summary>
        /// The description
        /// </summary>
        public string Description
        {
            get => mDescription ?? string.Empty;
            set => mDescription = value;
        }

        /// <summary>
        /// The rooms
        /// </summary>
        public IList<CompanyLayoutRoomDataModel> Rooms
        {
            get
            {
                if (mRooms is null)
                    mRooms = new List<CompanyLayoutRoomDataModel>();

                return mRooms;
            }
            set => mRooms = value;
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public CompanyLayoutEntity() : base()
        {

        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Creates and returns a <see cref="CompanyLayoutEntity"/> from the specified <paramref name="model"/>
        /// </summary>
        /// <param name="model">The model</param>
        /// <param name="companyId">The company id</param>
        /// <returns></returns>
        public static CompanyLayoutEntity FromRequestModel(CompanyLayoutRequestModel model, ObjectId companyId)
        {
            var entity = new CompanyLayoutEntity();

            DI.Mapper.Map(model, entity);
            entity.CompanyId = companyId;
            return entity;
        }

        /// <summary>
        /// Creates and returns a <see cref="CompanyLayoutResponseModel"/> from the current <see cref="CompanyLayoutEntity"/>
        /// </summary>
        /// <returns></returns>
        public CompanyLayoutResponseModel ToResponseModel()
            => EntityHelpers.ToResponseModel<CompanyLayoutResponseModel>(this);

        #endregion
    }
}
