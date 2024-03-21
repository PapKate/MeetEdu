using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

using System;
using System.Threading.Tasks;

namespace MeetBase.Blazor
{
    /// <summary>
    /// The base for all the controls
    /// </summary>
    public class BaseControl : ComponentBase
    {
        #region Private Members

        /// <summary>
        /// The member of the <see cref="IsSelected"/> property
        /// </summary>
        private bool mIsSelected;

        /// <summary>
        /// The member of the <see cref="IsEnabled"/> property
        /// </summary>
        private bool mIsEnabled = true;

        #endregion

        #region Public Properties

        /// <summary>
        /// The element's id
        /// </summary>
        [Parameter]
        public string? Id { get; set; }

        /// <summary>
        /// The width
        /// </summary>
        [Parameter]
        public string? Width { get; set; }

        /// <summary>
        /// The height
        /// </summary>
        [Parameter]
        public string? Height { get; set; }

        /// <summary>
        /// The additional CSS class names
        /// </summary>
        [Parameter]
        public string? CssClasses { get; set; }

        /// <summary>
        /// The additional CSS style
        /// </summary>
        [Parameter]
        public string? Style { get; set; }

        /// <summary>
        /// Indicates whether it is disabled
        /// </summary>
        [Parameter]
        public bool IsEnabled
        {
            get => mIsEnabled;

            set
            {
                // If the value didn't change...
                if (mIsEnabled == value)
                    // Return
                    return;

                // Create the context
                var context = new ValueChangedContext<bool>(mIsEnabled, value);

                // Set the value
                mIsEnabled = value;

                // Handle the change
                OnIsDisabledChanged(context);
            }
        }

        /// <summary>
        /// Indicates whether it is selected
        /// </summary>
        [Parameter]
        public bool IsSelected
        {
            get => mIsSelected;

            set
            {
                // If the value didn't change...
                if (mIsSelected == value)
                    // Return
                    return;

                // Create the context
                var context = new ValueChangedContext<bool>(mIsSelected, value);
                
                // Set the value
                mIsSelected = value;

                // Handle the change
                OnIsSelectedChangedCore(context);
            }
        }

        /// <summary>
        /// A flag indicating whether the cursor is hovering above the control
        /// </summary>
        public bool IsMouseOver { get; private set; }

        /// <summary>
        /// A flag indicating whether the cursor is pressed over the control
        /// </summary>
        public bool IsMouseDown { get; private set; }

        /// <summary>
        /// The child content
        /// </summary>
        [Parameter]
        public RenderFragment? ChildContent { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public BaseControl()
        {
            Id = Guid.NewGuid().ToNormalizedString();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Calls the <see cref="ComponentBase.StateHasChanged"/> method
        /// </summary>
        public void InvokeStateHasChanged()
        {
            StateHasChanged();
            OnStateChanged();
        }

        #endregion

        #region Protected Methods

        /// <summary>
        /// Handles the mouse over
        /// </summary>
        protected void OnMouseOver(MouseEventArgs e)
        {
            // If it is disabled...
            if (!IsEnabled)
                // Return
                return;

            IsMouseOver = true;

            OnStateChanged();
            OnMouseOverCore();
        }

        /// <summary>
        /// Handles the mouse over
        /// </summary>
        protected virtual void OnMouseOverCore() { }

        /// <summary>
        /// Handles the mouse out
        /// </summary>
        protected void OnMouseOut(MouseEventArgs e)
        {
            // If it is disabled...
            if (!IsEnabled)
                // Return
                return;

            IsMouseOver = false;

            OnStateChanged();
            OnMouseOutCore();
        }

        /// <summary>
        /// Handles the mouse out
        /// </summary>
        protected virtual void OnMouseOutCore() 
        {
        }

        /// <summary>
        /// Handles the mouse down
        /// </summary>
        protected void OnMouseDown(MouseEventArgs e)
        {
            // If it is disabled...
            if (!IsEnabled)
                // Return
                return;

            IsMouseDown = true;

            OnStateChanged();
            OnMouseDownCore(e);
        }

        /// <summary>
        /// Handles the mouse down
        /// </summary>
        protected virtual void OnMouseDownCore(MouseEventArgs e) { }

        /// <summary>
        /// Handles the right click
        /// </summary>
        protected virtual void OnContextMenuCore() { }

        /// <summary>
        /// Handles the mouse up
        /// </summary>
        protected void OnMouseUp(MouseEventArgs e)
        {
            // If it is disabled...
            if (!IsEnabled)
                // Return
                return;

            IsMouseDown = false;

            OnStateChanged();
            OnMouseUpCore();
        }

        /// <summary>
        /// Handles the mouse up
        /// </summary>
        protected virtual void OnMouseUpCore()
        {

        }

        /// <summary>
        /// Handles the change of the state of the component.
        /// </summary>
        protected virtual void OnStateChanged() { }

        /// <summary>
        /// Handles the change of the <see cref="BaseControl.IsEnabled"/> property
        /// </summary>
        /// <param name="context">The context</param>
        protected virtual void OnIsDisabledChanged(ValueChangedContext<bool> context)
        {

        }

        /// <summary>
        /// Handles the change of the <see cref="BaseControl.IsSelected"/> property internally
        /// </summary>
        /// <param name="context">The context</param>
        protected virtual void OnIsSelectedChanged(ValueChangedContext<bool> context)
        {

        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Handles the change of the <see cref="BaseControl.IsEnabled"/> property internally
        /// </summary>
        /// <param name="context">The context</param>
        private async void OnIsDisabledChangedCore(ValueChangedContext<bool> context)
        {
            OnStateChanged();

            OnIsDisabledChanged(context);

            await IsDisabledChanged.InvokeAsync(context);
        }

        /// <summary>
        /// Handles the change of the <see cref="BaseControl.IsSelected"/> property internally
        /// </summary>
        /// <param name="context">The context</param>
        private async void OnIsSelectedChangedCore(ValueChangedContext<bool> context)
        {
            OnStateChanged();

            OnIsSelectedChanged(context);

            await IsSelectedChanged.InvokeAsync(context);
        }

        #endregion

        #region Public Events

        /// <summary>
        /// Fires every time the <see cref="IsEnabled"/> property changes
        /// </summary>
        [Parameter]
        public EventCallback<ValueChangedContext<bool>> IsDisabledChanged { get; set; }

        /// <summary>
        /// Fires every time the <see cref="IsSelected"/> property changes
        /// </summary>
        [Parameter]
        public EventCallback<ValueChangedContext<bool>> IsSelectedChanged { get; set; }

        #endregion
    }
}
