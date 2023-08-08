using MongoDB.Bson;

namespace AppointMate
{
    /// <summary>
    /// Request model used for a label
    /// </summary>
    public class LabelRequestModel : StandardRequestModel
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

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public LabelRequestModel()
        {

        }

        #endregion
    }
}
