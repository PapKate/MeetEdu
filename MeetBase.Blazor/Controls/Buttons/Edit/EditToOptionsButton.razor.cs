using Microsoft.AspNetCore.Components;

using static MeetBase.Blazor.Personalization;

namespace MeetBase.Blazor
{
    public partial class EditToOptionsButton
    {
        #region Private Members

        private bool mIsEditButtonVisible = true;

        #endregion

        #region Public Properties

        /// <summary>
        /// A flag indicating whether it has an overlay or not
        /// </summary>
        [Parameter]
        public bool HasOverlay { get; set; }

        /// <summary>
        /// The options menu alignment
        /// </summary>
        [Parameter]
        public Alignment Alignment { get; set; }

        /// <summary>
        /// The edit button
        /// </summary>
        [Parameter]
        public OptionButton? EditButton { get; set; }

        /// <summary>
        /// The option buttons
        /// </summary>
        [Parameter]
        public IEnumerable<OptionButton>? OptionButtons { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public EditToOptionsButton() : base()
        {

        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Sets the border radius of the button with the specified <paramref name="index"/>
        /// </summary>
        /// <param name="index">The button index</param>
        /// <returns></returns>
        private string SetButtonBorderRadius(int index)
        {
            // If it is the first button...
            if(index == 0)
            {
                // If it is horizontally aligned...
                if (Alignment == Alignment.Horizontal)
                    return $"{LargeBorderRadius} 0 0 {LargeBorderRadius}";

                // Else...
                else
                    return $"{LargeBorderRadius} {LargeBorderRadius} 0 0";
            }
            // Else if it is the last button...
            else if(index == OptionButtons?.Count() - 1)
            {
                // If it is horizontally aligned...
                if (Alignment == Alignment.Horizontal)
                    return $"0 {LargeBorderRadius} {LargeBorderRadius} 0";
                // Else...
                else
                    return $"0 0 {LargeBorderRadius} {LargeBorderRadius}";
            }

            return "0";
        }

        /// <summary>
        /// Hides the edit button 
        /// </summary>
        private async void EditButton_OnClick()
        {
            mIsEditButtonVisible = false;
            await EditButton!.OnClick.InvokeAsync();
        }

        #endregion
    }
}
