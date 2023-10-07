using MeetBase;

using Microsoft.AspNetCore.Components;

namespace MeetCore
{
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
        public DepartmentLayoutRoom? LayoutRoom { get; set; }

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

            if (LayoutRoom is not null) 
            {
                if (LayoutRoom.DisplayTheme == RoomDisplayTheme.Left)
                    mRoomDisplayThemeClass = "layoutRoomLeftContainer";
                else if (LayoutRoom.DisplayTheme == RoomDisplayTheme.Right)
                    mRoomDisplayThemeClass = "layoutRoomRightContainer";
                else
                    mRoomDisplayThemeClass = "layoutRoomCenterContainer";
            }
        }

        #endregion

        #region Private Methods

        private async void EditLayoutRoom()
        {
            await EditButtonClicked.InvokeAsync(LayoutRoom);
        }

        #endregion

        #region Public Events

        /// <summary>
        /// Fires when the edit button is clicked
        /// </summary>
        [Parameter]
        public EventCallback<DepartmentLayoutRoom> EditButtonClicked { get; set; }

        #endregion
    }
}
