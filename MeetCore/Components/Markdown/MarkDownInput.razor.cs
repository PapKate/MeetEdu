using MeetBase.Blazor;
using MeetBase.Web;

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

using MudBlazor;

namespace MeetCore
{
    /// <summary>
    /// The markdown text input
    /// </summary>
    public partial class MarkDownInput
    {
        #region Private Members

        private bool mIsTextReadOnly = true;

        private string mInputText = string.Empty;
        private string mText = string.Empty;

        #endregion

        #region Public Properties

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
            mText = mInputText;
            await TextChanged.InvokeAsync(mText);
        }

        /// <summary>
        /// Saves the description
        /// </summary>
        private void CancelButton_Onclick()
        {
            mIsTextReadOnly = true;
            mInputText = mText ?? string.Empty;
        }

        /// <summary>
        /// Saves the description
        /// </summary>
        private void EditButton_OnClick()
        {
            mIsTextReadOnly = false;
            mInputText = mText ?? string.Empty;
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
