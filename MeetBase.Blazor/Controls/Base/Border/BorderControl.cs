using Microsoft.AspNetCore.Components;

using System.Threading.Tasks;

using static MeetBase.Blazor.PaletteColors;

namespace MeetBase.Blazor
{
    /// <summary>
    /// A <see cref="ForegroundControl"/> that has a <see cref="BorderBrush"/> property that changes based on its state
    /// </summary>
    public class BorderControl : ForegroundControl
    {
        #region Private Members

        /// <summary>
        /// The member of the <see cref="BorderColor"/> property
        /// </summary>
        private string? mBorderColor = LightGray;

        #endregion

        #region Public Properties

        /// <summary>
        /// The border brush
        /// </summary>
        public string? BorderBrush { get; private set; }

        /// <summary>
        /// The default border color
        /// </summary>
        [Parameter]
        public string? BorderColor 
        {
            get => mBorderColor;
            set
            {
                // If the value didn't change
                if(mBorderColor == value)
                    // Return
                    return;

                // Set the value
                mBorderColor = value;

                // If we shouldn't automatically set the hover and the clicked colors...
                if (!AutoSetStateBorderColors)
                    // Return
                    return;

                HoverBorderColor = value;
                ClickedBorderColor = value;
            }
        }

        /// <summary>
        /// The disabled border color 
        /// </summary>
        [Parameter]
        public string? DisabledBorderColor { get; set; }

        /// <summary>
        /// The hover border color
        /// </summary>
        [Parameter]
        public string? HoverBorderColor { get; set; }

        /// <summary>
        /// The clicked border color 
        /// </summary>
        [Parameter]
        public string? ClickedBorderColor { get; set; }

        /// <summary>
        /// The selected border color
        /// </summary>
        [Parameter]
        public string? IsSelectedBorderColor { get; set; } = Blue;

        /// <summary>
        /// Automatically sets the state back color properties when the <see cref="BorderColor"/> is set.
        /// The default value is <see cref="true"/>.
        /// </summary>
        [Parameter]
        public bool AutoSetStateBorderColors { get; set; } = true;

        /// <summary>
        /// The border's radius
        /// </summary>
        [Parameter]
        public string? BorderRadius { get; set; }

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
        /// The box shadow
        /// </summary>
        [Parameter]
        public string? BoxShadow { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public BorderControl()
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
        protected override sealed Task OnInitializedForegroundAsync()
        {
            BorderBrush = SelectColorBasedOnState();

            return OnInitializedBorderAsync();
        }

        /// <summary>
        /// Method invoked when the component is ready to start, having received its initial
        /// parameters from its parent in the render tree. Override this method if you will
        /// perform an asynchronous operation and want the component to refresh when that
        /// operation is completed.
        /// </summary>
        /// <returns></returns>
        protected virtual Task OnInitializedBorderAsync() => Task.CompletedTask;

        /// <summary>
        /// Handles the change of the state of the component.
        /// </summary>
        protected override void OnStateChanged()
        {
            base.OnStateChanged();

            BorderBrush = SelectColorBasedOnState();
            
            // If it is disabled...
            if(!IsEnabled)
                // Remove the box shadow
                BoxShadow = string.Empty;

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
                return DisabledBorderColor;
            else if (IsSelected)
                return IsSelectedBorderColor;
            else if (IsMouseDown)
                return ClickedBorderColor;
            else if (IsMouseOver)
                return HoverBorderColor;

            return BorderColor;
        }

        #endregion
    }
}
