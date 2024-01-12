using MeetBase;
using MeetBase.Web;

using Microsoft.AspNetCore.Components;

namespace MeetCore
{
    /// <summary>
    /// The presenter for a layout room
    /// </summary>
    public partial class LayoutPresenter
    {
        #region Private Members

        /// <summary>
        /// The CSS class for the display theme
        /// </summary>
        private string mRoomDisplayThemeClass = "layoutRoomRightContainer";

        #endregion

        #region Public Properties

        /// <summary>
        /// The layout room
        /// </summary>
        [Parameter]
        public DepartmentLayoutResponseModel? Layout { get; set; }

        /// <summary>
        /// A flag indicating whether it is editable or not
        /// </summary>
        [Parameter]
        public bool IsEditable { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public LayoutPresenter() : base()
        {

        }

        #endregion

        #region Protected Methods

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        protected override void OnInitialized()
        {
            base.OnInitialized();

            if (Layout is not null) 
            {
                if (Layout.DisplayTheme == ImageDisplayTheme.Left)
                    mRoomDisplayThemeClass = "layoutRoomLeftContainer";
                else if (Layout.DisplayTheme == ImageDisplayTheme.Right)
                    mRoomDisplayThemeClass = "layoutRoomRightContainer";
                else
                    mRoomDisplayThemeClass = "layoutRoomCenterContainer";
            }
        }

        #endregion

        #region Private Methods

        private async void EditLayoutRoom()
        {
            await EditButtonClicked.InvokeAsync(Layout);
        }

        #endregion

        #region Public Events

        /// <summary>
        /// Fires when the edit button is clicked
        /// </summary>
        [Parameter]
        public EventCallback<DepartmentLayoutResponseModel> EditButtonClicked { get; set; }

        #endregion
    }
}
