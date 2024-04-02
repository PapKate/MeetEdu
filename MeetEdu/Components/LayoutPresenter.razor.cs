using Microsoft.AspNetCore.Components;

namespace MeetEdu
{
    /// <summary>
    /// The layout presenter
    /// </summary>
    public partial class LayoutPresenter
    {
        #region Public Properties

        /// <summary>
        /// The model
        /// </summary>
        [Parameter]
        public LayoutResponseModel? Model { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public LayoutPresenter() : base()
        {

        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Sets the CSS class for the layout container
        /// </summary>
        /// <returns></returns>
        private string SetLayoutRoomTheme()
        {
            if(Model is null)
                return string.Empty;

            if (Model.DisplayTheme == ImageDisplayTheme.Left)
                return "layoutRoomLeftContainer";
            else if (Model.DisplayTheme == ImageDisplayTheme.Right)
                return "layoutRoomRightContainer";
            else
                return "layoutRoomCenterContainer";
        }

        #endregion
    }
}
