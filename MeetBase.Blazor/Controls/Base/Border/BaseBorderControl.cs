using Microsoft.AspNetCore.Components;

namespace MeetBase.Blazor
{
    /// <summary>
    /// A <see cref="BaseForegroundControl"/> that has a <see cref="BorderBrush"/> property
    /// </summary>
    public class BaseBorderControl : BaseForegroundControl
    {
        #region Public Properties

        /// <summary>
        /// The border's style
        /// </summary>
        [Parameter]
        public BorderStyle BorderStyle { get; set; } = BorderStyle.None;

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
        /// The box shadow
        /// </summary>
        [Parameter]
        public string? BoxShadow { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public BaseBorderControl()
        {

        }

        #endregion

        #region Protected Methods

        protected override void OnStateChanged()
        {
            base.OnStateChanged();

            // If it is disabled...
            if (!IsEnabled)
                // Remove the box shadow
                BoxShadow = string.Empty;
        }

        #endregion
    }
}
