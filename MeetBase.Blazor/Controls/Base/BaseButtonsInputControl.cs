
using Microsoft.AspNetCore.Components;

using System.Collections.Generic;
using System.Linq;

namespace MeetBase.Blazor
{
    /// <summary>
    /// The base for all the input controls that have selection buttons
    /// </summary>
    public class BaseButtonsInputControl : BaseBorderControl, ITextConfiguration
    {
        #region Public Properties

        /// <summary>
        /// The back color for the buttons
        /// </summary>
        [Parameter]
        public string? ButtonBackColor { get; set; }

        /// <summary>
        /// The fore color of the buttons
        /// </summary>
        [Parameter]
        public string? ButtonForeColor { get; set; }

        /// <summary>
        /// The selected back color of the buttons
        /// </summary>
        [Parameter]
        public string? IsSelectedButtonBackColor { get; set; }

        /// <summary>
        /// The selected fore color of the buttons
        /// </summary>
        [Parameter]
        public string? IsSelectedButtonForeColor { get; set; }

        /// <summary>
        /// The font family
        /// </summary>
        [Parameter]
        public string? FontFamily { get; set; }

        /// <summary>
        /// The font size
        /// </summary>
        [Parameter]
        public string? FontSize { get; set; }

        /// <summary>
        /// The font weight
        /// </summary>
        [Parameter]
        public string? FontWeight { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public BaseButtonsInputControl() :  base()
        {

        }

        #endregion

        #region Protected Methods

        /// <summary>
        /// Sets the button's color according to the <see cref="BaseControl.IsSelected"/>
        /// </summary>
        /// <param name="buttons">The list with the buttons</param>
        /// <param name="id">The clicked button's id</param>
        /// <param name="defaultColor">The default color</param>
        /// <param name="selectedColor">The selected color</param>
        /// <returns></returns>
        protected string SetButtonColor<T>(IEnumerable<T> buttons, string id, string defaultColor, string selectedColor) 
            where T : BaseButton
        {
            //If a button with the given id exists in the list...
            if (buttons.FirstOrDefault(x => x.Id == id) != null)
            {
                var allIdButtons = buttons.Where(x => x.Id == id);

                // If any button with the id is selected...
                if (allIdButtons.Any(x => x.IsSelected))
                {
                    // Returns the selected button color 
                    return selectedColor;
                }

                // Else...
                else
                    // Returns the default button color
                    return defaultColor;
            }

            // Returns the default button color
            return defaultColor;
        }

        /// <summary>
        /// Sets as selected the <paramref name="buttons"/> with the given <paramref name="value"/> 
        /// </summary>
        /// <param name="value">The button's id</param>
        /// <param name="buttons">The buttons' list</param>
        protected string SetSelectedButton<T>(string value, IEnumerable<T> buttons)
            where T : BaseButton
        {
            buttons.ForEach(x => x.IsSelected = false);

            // Gets the first button with id the given value
            var clickedButtons = buttons.Where(x => x.Id == value);

            // Sets it as selected
            clickedButtons.ForEach(x => x.IsSelected = true);

            return value;
        }

        #endregion
    }
}
