using Microsoft.AspNetCore.Components;

namespace MeetBase.Blazor
{
    public class BaseBackgroundAndBorderControl : BaseBackgroundControl
    {
        #region Public Properties

        /// <summary>
        /// The border's style
        /// </summary>
        [Parameter]
        public BorderStyle BorderStyle { get; set; } = BorderStyle.Solid;

        /// <summary>
        /// The border's width
        /// </summary>
        [Parameter]
        public string? BorderThickness { get; set; }

        /// <summary>
        /// The border's radius
        /// </summary>
        [Parameter]
        public string? BorderRadius { get; set; }

        /// <summary>
        /// The border brush
        /// </summary>
        [Parameter]
        public string? BorderBrush { get; set; }

        /// <summary>
        /// Indicates whether the button has round corners
        /// </summary>
        [Parameter]
        public bool HasRoundCorners { get; set; }

        /// <summary>
        /// The box shadow
        /// </summary>
        [Parameter]
        public string? BoxShadow { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public BaseBackgroundAndBorderControl()
        {

        }

        #endregion
    }
}
