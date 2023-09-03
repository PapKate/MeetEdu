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
        /// The name
        /// </summary>
        [Parameter]
        public string? Name { get; set; }

        /// <summary>
        /// The image
        /// </summary>
        [Parameter]
        public string? Image { get; set; }

        /// <summary>
        /// The color
        /// </summary>
        [Parameter]
        public string? Color { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public UniversityBox() : base()
        {

        }

        #endregion
    }
}
