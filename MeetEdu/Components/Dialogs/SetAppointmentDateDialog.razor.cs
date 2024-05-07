using Microsoft.AspNetCore.Components;
using MudBlazor;
using static MeetBase.Blazor.PaletteColors;

namespace MeetEdu
{
    /// <summary>
    /// The dialog for setting an appointment date
    /// </summary>
    public partial class SetAppointmentDateDialog
    {
        #region Private Members

        /// <summary>
        /// A list containing all the buttons
        /// </summary>
        private List<TextButton> mSlotButtons = new List<TextButton>();

        /// <summary>
        /// The current selected index
        /// </summary>
        private int mSlotIndex = 0;

        #endregion

        #region Public Properties

        /// <summary>
        /// The dialog instance
        /// </summary>
        [CascadingParameter]
        public MudDialogInstance MudDialog { get; set; } = default!;

        /// <summary>
        /// The possible appointment dates
        /// </summary>
        [Parameter]
        public IEnumerable<IReadOnlyRangeable<DateTimeOffset>>? Slots { get; set; }

        /// <summary>
        /// The selected slot
        /// </summary>
        [Parameter]
        public IReadOnlyRangeable<DateTimeOffset>? Value { get; set; }

        /// <summary>
        /// The color
        /// </summary>
        [Parameter]
        public string? Color { get; set; }

        #endregion

        #region Protected Properties

        /// <summary>
        /// The minute button
        /// </summary>
        protected TextButton? SlotButton
        {
            set
            {
                if (value is null)
                    return;
                mSlotButtons.Add(value);
                SetDefaultButtonStyle(value);
            }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public SetAppointmentDateDialog()
        {

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
            button.BackColor = White;
            button.BorderColor = White.ToDarkerColor(2);
            button.ForeColor = White.DarkOrWhite();
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
            button.BackColor = Color;
            button.BorderColor = Color;
            button.ForeColor = Color.DarkOrWhite();
            button.Style = $"filter: {Personalization.LightFilterDropShadow};";
            button.InvokeStateHasChanged();
        }

        /// <summary>
        /// Sets the style of the button with the specified <paramref name="index"/> and
        /// modifies the <see cref="mSlotButtons"/> list accordingly
        /// </summary>
        /// <param name="index">The button index</param>
        private async void SlotButton_OnClick(int index)
        {
            // Gets the previously selected button
            var previousButton = mSlotButtons[mSlotIndex];

            // Reverts to the default button style
            SetDefaultButtonStyle(previousButton);

            // Gets the button
            var button = mSlotButtons[index];

            // Sets the selected button style
            SetSelectedButtonStyle(button);
            mSlotIndex = index;
            Value = Slots!.ToList().ElementAt(index);
            await ValueChanged.InvokeAsync(Value);
        }

        private void Save()
        {
            MudDialog.Close(DialogResult.Ok(Value));
        }

        private void Cancel()
        {
            MudDialog.Cancel();
        }

        #endregion

        #region Public Events

        /// <summary>
        /// Fires when the <see cref="Value"/> is changed
        /// </summary>
        [Parameter]
        public EventCallback<IReadOnlyRangeable<DateTimeOffset>> ValueChanged { get; set; }

        #endregion
    }
}
