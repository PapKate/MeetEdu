namespace AppointMate
{
    /// <summary>
    /// Represents a company layout
    /// </summary>
    public class CompanyLayoutResponseModel : DateResponseModel, IDescriptable, ICompanyIdentifiable<string>
    {
        #region Private Members

        /// <summary>
        /// The member of the <see cref="CompanyId"/> property
        /// </summary>
        private string? mCompanyId;

        /// <summary>
        /// The member of the <see cref="Description"/> property
        /// </summary>
        private string? mDescription;

        /// <summary>
        /// The member of the <see cref="Rooms"/> property
        /// </summary>
        private IEnumerable<CompanyLayoutRoomResponseModel>? mRooms;

        #endregion

        #region Public Properties

        /// <summary>
        /// The company id
        /// </summary>
        public string CompanyId
        {
            get => mCompanyId ?? string.Empty;
            set => mCompanyId = value;
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
        /// The rooms
        /// </summary>
        public IEnumerable<CompanyLayoutRoomResponseModel> Rooms 
        { 
            get => mRooms ?? Enumerable.Empty<CompanyLayoutRoomResponseModel>();
            set => mRooms = value;
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public CompanyLayoutResponseModel() : base()
        {

        }

        #endregion
    }
}
