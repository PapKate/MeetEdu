using Microsoft.AspNetCore.Components;

namespace MeetBase.Blazor
{
    /// <summary>
    /// The markdown text input
    /// </summary>
    public partial class MarkDownInput
    {
        #region Private Members

        private bool mIsTextReadOnly = true;

        private string mInputText = string.Empty;

        #endregion

        #region Public Properties

        /// <summary>
        /// The id
        /// </summary>
        [Parameter]
        public string? Id { get; set; }

        /// <summary>
        /// The placeholder
        /// </summary>
        [Parameter]
        public string? Placeholder { get; set; }

        /// <summary>
        /// A multi-line input will be shown if it is set to greater than one
        /// </summary>
        [Parameter]
        public int Lines { get; set; }

        /// <summary>
        /// The text
        /// </summary>
        [Parameter]
        public string? Text { get; set; }

        /// <summary>
        /// The text
        /// </summary>
        [Parameter]
        public string? EditButtonColor { get; set; }

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
        public MarkDownInput() : base()
        {

        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Saves the description
        /// </summary>
        private async void SaveButton_Onclick()
        {
            mIsTextReadOnly = true;
            Text = mInputText;
            await TextChanged.InvokeAsync(Text);
        }

        /// <summary>
        /// Saves the description
        /// </summary>
        private void CancelButton_Onclick()
        {
            mIsTextReadOnly = true;
            mInputText = Text ?? string.Empty;
        }

        /// <summary>
        /// Saves the description
        /// </summary>
        private void EditButton_OnClick()
        {
            mIsTextReadOnly = false;
            mInputText = Text ?? string.Empty;
        }

        #endregion

        #region Public Events

        /// <summary>
        /// Fires when the text is changed
        /// </summary>
        [Parameter]
        public EventCallback<string?> TextChanged { get; set; }

        #endregion
    }
}
