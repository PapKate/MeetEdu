using Microsoft.AspNetCore.Components;

namespace MeetEdu
{
    /// <summary>
    /// The University display box
    /// </summary>
    public partial class UniversityBox
    {
        #region Public Properties

        /// <summary>
        /// The university
        /// </summary>
        [Parameter]
        public UniversityResponseModel? University { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public UniversityBox() : base()
        {

        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Navigates to the page of professors or departments
        /// </summary>
        private async void UniversityButton_OnClick()
        {
            await OnClick.InvokeAsync();
        }

        #endregion

        #region Public Events

        /// <summary>
        /// Fires when the university box is clicked
        /// </summary>
        [Parameter]
        public EventCallback OnClick { get; set; }

        #endregion
    }
}
