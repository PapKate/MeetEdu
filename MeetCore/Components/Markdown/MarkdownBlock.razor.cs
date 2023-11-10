using Microsoft.AspNetCore.Components;

namespace MeetCore
{
    /// <summary>
    /// The markdown text block
    /// </summary>
    public partial class MarkdownBlock
    {
        #region Public Properties

        /// <summary>
        /// The CSS classes
        /// </summary>
        [Parameter]
        public string? CssClass { get; set; }

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
        public MarkdownBlock() : base()
        {

        }

        #endregion
    }
}
