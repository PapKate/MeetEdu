using Microsoft.AspNetCore.Components;

namespace MeetEdu
{
    /// <summary>
    /// The page container
    /// </summary>
    public partial class PageContainer
    {
        #region Public Properties

        /// <summary>
        /// The id
        /// </summary>
        [Parameter]
        public string? Id { get; set; }

        /// <summary>
        /// The child content
        /// </summary>
        [Parameter]
        public RenderFragment? ChildContent { get; set; }

        #endregion
    }
}
