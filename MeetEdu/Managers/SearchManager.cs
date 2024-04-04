using Microsoft.AspNetCore.Components;

namespace MeetEdu
{
    /// <summary>
    /// The manager for searching
    /// </summary>
    public class SearchManager
    {
        #region Public Properties

        /// <summary>
        /// The type of the search result
        /// </summary>
        public SearchResultType SearchResultType { get; set; }

        /// <summary>
        /// The search text
        /// </summary>
        public string? Text { get; set; }

        #endregion

        #region Protected Properties

        /// <summary>
        /// The navigation manager service
        /// </summary>
        [Inject]
        protected NavigationManager? NavigationManager { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public SearchManager() : base()
        {

        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Switches between <see cref="SearchResultType.Departments"/> and <see cref="SearchResultType.Faculty"/>
        /// </summary>
        public void SwitchSearchReultType()
        {
            if(SearchResultType == SearchResultType.Departments)
                SearchResultType = SearchResultType.Faculty;
            else
                SearchResultType = SearchResultType.Departments;
        }

        #endregion
    }
}
