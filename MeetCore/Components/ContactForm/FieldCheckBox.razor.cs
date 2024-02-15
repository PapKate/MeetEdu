using Microsoft.AspNetCore.Components;

namespace MeetCore
{
    /// <summary>
    /// The check box
    /// </summary>
    public partial class FieldCheckBox
    {
        #region Public Properties

        /// <summary>
        /// The text
        /// </summary>
        [Parameter]
        public string? Text { get; set; }

        /// <summary>
        /// A flag indicating whether it is checked or not
        /// </summary>
        [Parameter]
        public bool IsChecked { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public FieldCheckBox() : base()
        {

        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Changes the <see cref="IsChecked"/> value
        /// </summary>
        private void OnIsCheckedChanged(bool value)
        {
            IsChecked = value;
            IsCheckedChanged.InvokeAsync(IsChecked);
        }

        #endregion

        #region Public Events

        /// <summary>
        /// Fires when the <see cref="IsChecked"/>'s value changes
        /// </summary>
        [Parameter]
        public EventCallback<bool> IsCheckedChanged { get; set; }

        #endregion
    }
}
