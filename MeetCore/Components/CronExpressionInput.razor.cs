using MeetBase;
using MeetBase.Blazor;

using Microsoft.AspNetCore.Components;

namespace MeetCore
{
    /// <summary>
    /// An input for creating a CRON expression
    /// </summary>
    public partial class CronExpressionInput
    {
        #region Private Members

        private List<int> mPossibleValues = new List<int>()
        {
            0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29,
            30, 31, 32, 33, 34, 35, 36, 37, 38, 39, 40, 41, 42, 43, 44, 45, 46, 47, 48, 49, 50, 51, 52, 53, 54, 55, 56, 57, 58, 59
        };

        /// <summary>
        /// A list containing all the buttons
        /// </summary>
        private List<TextButton> mMinuteButtons = new List<TextButton>();

        #endregion

        #region Public Properties

        /// <summary>
        /// A list containing the selected minutes
        /// </summary>
        [Parameter]
        public List<int>? Value { get; set; }

        /// <summary>
        /// The selected color
        /// </summary>
        [Parameter]
        public string SelectedColor { get; set; } = PaletteColors.Blue;

        /// <summary>
        /// The default color
        /// </summary>
        [Parameter]
        public string DefaultColor { get; set; } = PaletteColors.White;

        /// <summary>
        /// The addition css classes
        /// </summary>
        [Parameter]
        public string? CssClasses { get; set; }

        #endregion

        #region Protected Properties

        /// <summary>
        /// The minute button
        /// </summary>
        protected TextButton? MinuteButton
        {
            set
            {
                if (value is null)
                    return;
                mMinuteButtons.Add(value);
                SetDefaultButtonStyle(value);
            }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public CronExpressionInput() : base()
        {

        }

        #endregion

        #region Protected Methods

        /// <inheritdoc/>
        protected override void OnAfterRender(bool firstRender)
        {
            base.OnAfterRender(firstRender);

            if (firstRender)
            {
                // If no selected values are preset...
                if (Value.IsNullOrEmpty())
                    Value = new();
                else
                {
                    foreach (var value in Value)
                    {
                        var button = mMinuteButtons.First(x => x.Text == value.ToString());
                        SetSelectedButtonStyle(button);
                    }
                }
            }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Sets the button style to the default one
        /// </summary>
        /// <param name="button">The button</param>
        private void SetDefaultButtonStyle(TextButton button)
        {
            // Sets the default colors
            button.BackColor = DefaultColor;
            button.BorderColor = DefaultColor.ToDarkerColor(2);
            button.ForeColor = DefaultColor.DarkOrWhite();
            button.Style = string.Empty;
            button.InvokeStateHasChanged();
        }

        /// <summary>
        /// Sets the button style to the selected one
        /// </summary>
        /// <param name="button">The button</param>
        private void SetSelectedButtonStyle(TextButton button)
        {
            // Sets the selected colors
            button.BackColor = SelectedColor;
            button.BorderColor = SelectedColor;
            button.ForeColor = SelectedColor.DarkOrWhite();
            button.Style = $"filter: {Personalization.LightFilterDropShadow};";
            button.InvokeStateHasChanged();
        }

        /// <summary>
        /// Sets the style of the button with the specified <paramref name="value"/> and
        /// modifies the <see cref="mMinuteButtons"/> list accordingly
        /// </summary>
        /// <param name="value">The value</param>
        private async void MinuteButton_OnClick(int value)
        {
            // Gets the button
            var button = mMinuteButtons.First(x => x.Text == value.ToString());

            // If the value is already selected...
            if (Value!.Any(x => x == value))
            {
                // Removes the value from the list
                Value!.Remove(value);

                // Resets the colors
                SetDefaultButtonStyle(button);

                // Returns
                return;
            }

            // Adds the value from the list
            Value!.Add(value);
            await ValueChanged.InvokeAsync(Value);

            SetSelectedButtonStyle(button);
        }

        /// <summary>
        /// Clears the list and resets the buttons to the default styles
        /// </summary>
        private async void ClearButton_OnClick()
        {
            Value!.Clear();
            mMinuteButtons.ForEach(SetDefaultButtonStyle);
            await ValueChanged.InvokeAsync(Value);
        }

        /// <summary>
        /// Adds all the value st the list and sets the buttons to the selected style
        /// </summary>
        private async void AllButton_OnClick()
        {
            Value = new();
            Value.AddRange(mPossibleValues);
            mMinuteButtons.ForEach(SetSelectedButtonStyle);
            await ValueChanged.InvokeAsync(Value);
        }

        #endregion

        #region Public Events

        /// <summary>
        /// Fires when the <see cref="Value"/> changes
        /// </summary>
        [Parameter]
        public EventCallback<List<int>> ValueChanged { get; set; }

        #endregion
    }
}
