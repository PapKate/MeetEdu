using MongoDB.Bson;

namespace AppointMate
{
    /// <summary>
    /// Request model used for a company layout
    /// </summary>
    public class CompanyLayoutRequestModel : BaseRequestModel
    {
        #region Public Properties

        /// <summary>
        /// The company id
        /// </summary>
        public string? CompanyId { get; set; }

        /// <summary>
        /// The description
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// The rooms
        /// </summary>
        public IEnumerable<CompanyLayoutRoomDataModel>? Rooms { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public CompanyLayoutRequestModel() : base()
        {

        }

        #endregion
    }
}
