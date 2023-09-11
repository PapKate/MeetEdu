using Microsoft.AspNetCore.Components;

using static MeetBase.Blazor.PaletteColors;

namespace MeetBase.Blazor
{
    /// <summary>
    /// A <see cref="BackgroundControl"/> that has a <see cref="Foreground"/> property that changes based on its state
    /// </summary>
    public class ForegroundControl : BackgroundControl
    {
        #region Private Members

        /// <summary>
        /// The member of the <see cref="ForeColor"/> property
        /// </summary>
        private string? mForeColor = Gray;

        #endregion

        #region Public Properties

        /// <summary>
        /// The foreground
        /// </summary>
        public string? Foreground { get; private set; }

        /// <summary>
        /// The default foreground color
        /// </summary>
        [Parameter]
        public string? ForeColor
        {
            get => mForeColor;

            set
            {
                // If the value didn't change...
                if (mForeColor == value)
                    // Return
                    return;

                // Set the value
                mForeColor = value;

                // If we shouldn't automatically set the hover and the clicked colors...
                if (!AutoSetStateForeColors)
                    // Return
                    return;

                HoverForeColor = value;
                ClickedForeColor = value;
            }
        }

        /// <summary>
        /// The foreground color for when component is disabled
        /// </summary>
        [Parameter]
        public string? DisabledForeColor { get; set; } = Gray;

        /// <summary>
        /// The hover fore color
        /// </summary>
        [Parameter]
        public string? HoverForeColor { get; set; } = Gray;

        /// <summary>
        /// The clicked fore color
        /// </summary>
        [Parameter]
        public string? ClickedForeColor { get; set; } = Gray;

        /// <summary>
        /// The selected fore color
        /// </summary>
        [Parameter]
        public string? IsSelectedForeColor { get; set; } = White;

        /// <summary>
        /// Automatically sets the state fore color properties when the <see cref="ForeColor"/> is set.
        /// The default value is <see cref="true"/>.
        /// </summary>
        [Parameter]
        public bool AutoSetStateForeColors { get; set; } = true;

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public ForegroundControl()
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
        protected override sealed Task OnInitializedBackgroundAsync()
        {
            Foreground = SelectColorBasedOnState();

            return OnInitializedForegroundAsync();
        }

        /// <summary>
        /// Method invoked when the component is ready to start, having received its initial
        /// parameters from its parent in the render tree. Override this method if you will
        /// perform an asynchronous operation and want the component to refresh when that
        /// operation is completed.
        /// </summary>
        /// <returns></returns>
        protected virtual Task OnInitializedForegroundAsync() => Task.CompletedTask;

        /// <summary>
        /// Handles the change of the state of the component.
        /// </summary>
        protected override void OnStateChanged()
        {
            base.OnStateChanged();

            Foreground = SelectColorBasedOnState();
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
                return DisabledForeColor;
            else if (IsSelected)
                return IsSelectedForeColor;
            else if (IsMouseDown)
                return ClickedForeColor;
            else if (IsMouseOver)
                return HoverForeColor;

            return ForeColor;
        }

        #endregion
    }
}
