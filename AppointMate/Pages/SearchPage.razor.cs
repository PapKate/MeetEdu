using Microsoft.AspNetCore.Components;

namespace AppointMate
{
    /// <summary>
    /// The search page
    /// </summary>
    public partial class SearchPage
    {
        #region Public Properties

        /// <summary>
        /// The search text
        /// </summary>
        [Parameter]
        public string? SearchText { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public SearchPage() : base()
        {

        }

        #endregion
    }
}
