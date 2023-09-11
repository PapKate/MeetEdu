using Microsoft.AspNetCore.Components;

using System.Drawing;
using System.Threading.Tasks;

using static MeetBase.Blazor.PaletteColors;

namespace MeetBase.Blazor
{
    /// <summary>
    /// A control that has a <see cref="Background"/> property that changes based on its state
    /// </summary>
    public class BackgroundControl : BaseControl
    {
        #region Private Members

        /// <summary>
        /// The member of the <see cref="BackColor"/> property
        /// </summary>
        private string? mBackColor;

        #endregion

        #region Public Properties

        /// <summary>
        /// The background
        /// </summary>
        public string? Background { get; private set; }

        /// <summary>
        /// The default background color
        /// </summary>
        [Parameter]
        public string? BackColor
        {
            get => mBackColor;

            set
            {
                // If the value didn't change...
                if (mBackColor == value)
                    // Return
                    return;

                // Set the value
                mBackColor = value;

                // If we shouldn't automatically set the hover and the clicked colors...
                if (!AutoSetStateBackColors)
                    // Return
                    return;
                if(mBackColor.IsHexValue())
                {
                    HoverBackColor = ColorHelpers.FromHex(value).ToDarkerColor(1).ToHex();
                    ClickedBackColor = ColorHelpers.FromHex(value).ToDarkerColor(2).ToHex();
                }
                else
                {
                    HoverBackColor = value;
                    ClickedBackColor = value;
                }
            }
        }

        /// <summary>
        /// The disabled back color
        /// </summary>
        [Parameter]
        public string? DisabledBackColor { get; set; } = LightGray;

        /// <summary>
        /// The hover back color
        /// </summary>
        [Parameter]
        public string? HoverBackColor { get; set; }

        /// <summary>
        /// The clicked back color
        /// </summary>
        [Parameter]
        public string? ClickedBackColor { get; set; }

        /// <summary>
        /// The selected back color
        /// </summary>
        [Parameter]
        public string? IsSelectedBackColor { get; set; } = Blue;

        /// <summary>
        /// Automatically sets the state back color properties when the <see cref="BackColor"/> is set.
        /// The default value is <see cref="true"/>.
        /// </summary>
        [Parameter]
        public bool AutoSetStateBackColors { get; set; } = true;

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public BackgroundControl()
        {

        }

        #endregion

        #region Protected Methods

        /// <summary>
        /// Method invoked when the component is ready to start, having received its initial
        /// parameters from its parent in the render tree. Override this method if you will
        /// perform an asynchronous operation and want the component to refresh when that
        /// operation is completed.
        /// </summary>
        /// <returns></returns>
        protected override sealed Task OnInitializedAsync()
        {
            Background = SelectColorBasedOnState();

            return OnInitializedBackgroundAsync();
        }

        /// <summary>
        /// Method invoked when the component is ready to start, having received its initial
        /// parameters from its parent in the render tree. Override this method if you will
        /// perform an asynchronous operation and want the component to refresh when that
        /// operation is completed.
        /// </summary>
        /// <returns></returns>
        protected virtual Task OnInitializedBackgroundAsync() => Task.CompletedTask;

        /// <summary>
        /// Handles the change of the state of the component.
        /// </summary>
        protected override void OnStateChanged()
        {
            Background = SelectColorBasedOnState();
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Selects the color based on the current state of the component
        /// </summary>
        /// <returns></returns>
        private string? SelectColorBasedOnState()
        {
            if (!IsEnabled)
                return DisabledBackColor;
            else if (IsSelected)
                return IsSelectedBackColor;
            else if (IsMouseDown)
                return ClickedBackColor;
            else if (IsMouseOver)
                return HoverBackColor;

            return BackColor;
        }

        #endregion
    }
}
