﻿namespace AppointMate
{
    /// <summary>
    /// Represents a company layout document in the MongoDB
    /// </summary>
    public class DepartmentLayoutResponseModel : DateResponseModel, IDescriptable, IDepartmentIdentifiable<string>
    {
        #region Private Members

        /// <summary>
        /// The member of the <see cref="DepartmentId"/> property
        /// </summary>
        private string? mDepartmentId;

        /// <summary>
        /// The member of the <see cref="Description"/> property
        /// </summary>
        private string? mDescription;

        /// <summary>
        /// The member of the <see cref="Rooms"/> property
        /// </summary>
        private IList<DepartmentLayoutRoomDataModel>? mRooms;

        #endregion

        #region Public Properties

        /// <summary>
        /// The department id
        /// </summary>
        public string DepartmentId
        {
            get => mDepartmentId ?? string.Empty;
            set => mDepartmentId = value;
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
        /// The layout
        /// </summary>
        public IList<DepartmentLayoutRoomDataModel> Rooms
        {
            get
            {
                if (mRooms is null)
                    mRooms = new List<DepartmentLayoutRoomDataModel>();

                return mRooms;
            }
            set => mRooms = value;
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public DepartmentLayoutResponseModel() : base()
        {

        }

        #endregion
    }
}
