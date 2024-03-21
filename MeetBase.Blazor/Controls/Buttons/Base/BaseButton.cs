using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

using System;
using System.Threading.Tasks;

namespace MeetBase.Blazor
{
    /// <summary>
    /// The base for all the buttons
    /// </summary>
    public class BaseButton : BorderControl
    {
        #region Private Members

        /// <summary>
        /// The animation class name
        /// </summary>
        protected string mButtonAnimation = string.Empty;

        /// <summary>
        /// The top of the element
        /// </summary>
        protected string mTop = "0px";

        /// <summary>
        /// The left of the element
        /// </summary>
        protected string mLeft = "0px";

        #endregion

        #region Public Properties

        /// <summary>
        /// A flag indicating whether the button has animation
        /// </summary>
        [Parameter]
        public bool HasAnimation { get; set; } = true;

        /// <summary>
        /// The text's alignment
        /// </summary>
        [Parameter]
        public TextAlign TextAlign { get; set; } = TextAlign.Center;

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public BaseButton()
        {

        }

        #endregion

        #region Protected Methods

        protected override async void OnMouseDownCore(MouseEventArgs e)
        {
            base.OnMouseDownCore(e);
            await OnMiddleButtonClick.InvokeAsync(e);
        }

        protected async Task OnBaseButtonClick(MouseEventArgs e)
        {
            await OnClick.InvokeAsync(e);

            // If it has animation
            if(HasAnimation)
            {
                mButtonAnimation = "buttonAnimation";
                mTop = $"{e.OffsetY}px";
                mLeft = $"{e.OffsetX}px";

                await Task.Delay(MeetBase.Blazor.Personalization.RippleAnimationDelay);
                mButtonAnimation = string.Empty;
            }
        }

        #endregion

        #region Public Events

        /// <summary>
        /// On click event
        /// </summary>
        [Parameter]
        public EventCallback<MouseEventArgs> OnClick { get; set; }

        /// <summary>
        /// On mouse down event
        /// </summary>
        [Parameter]
        public EventCallback<MouseEventArgs> OnMiddleButtonClick { get; set; }

        #endregion
    }
}
