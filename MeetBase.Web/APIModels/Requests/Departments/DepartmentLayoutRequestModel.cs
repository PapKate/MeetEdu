using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;

namespace MeetBase.Web
{
    /// <summary>
    /// Request model for a department layout
    /// </summary>
    public class DepartmentLayoutRequestModel : StandardRequestModel
    {
        #region Public Properties

        /// <summary>
        /// The department id
        /// </summary>
        public string? DepartmentId { get; set; }

        /// <summary>
        /// The description
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// The note
        /// </summary>
        public string? Note { get; set; }

        /// <summary>
        /// The image URL
        /// </summary>
        public Uri? ImageFile { get; set; }

        /// <summary>
        /// The display theme
        /// </summary>
        public RoomDisplayTheme DisplayTheme { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public DepartmentLayoutRequestModel() : base()
        {

        }

        #endregion
    }
}
