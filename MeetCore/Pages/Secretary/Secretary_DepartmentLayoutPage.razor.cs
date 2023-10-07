using MeetBase;

using Microsoft.AspNetCore.Components.Forms;
using System.Reflection;

using static MeetBase.Blazor.PaletteColors;

namespace MeetCore
{
    /// <summary>
    /// The department layout page
    /// </summary>
    public partial class Secretary_DepartmentLayoutPage : BasePage
    {
        #region Private Members
        
        /// <summary>
        /// The photo input label
        /// </summary>
        private string mPhotoLabel = "Department logo";

        private IEnumerable<DepartmentLayoutRoom> Elements = new List<DepartmentLayoutRoom>();

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public Secretary_DepartmentLayoutPage() : base()
        {

        }

        #endregion

        #region Protected Methods

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        protected override void OnInitialized()
        {
            Elements = new List<DepartmentLayoutRoom>()
            {
                new()
                {
                    Color = Red,
                    Name = "Name",
                    DisplayTheme = RoomDisplayTheme.Left,
                    Note = "Tis is a  note"
                },
                new()
                {
                    Color = Green,
                    Name = "Name",
                    DisplayTheme = RoomDisplayTheme.Center,
                    Note = "Tis is a  note"
                },
                new()
                {
                    Color = Blue,
                    Name = "Name",
                    DisplayTheme = RoomDisplayTheme.Right,
                    Note = "Tis is a  note"
                }
            };
        } 

        #endregion
    }
}
