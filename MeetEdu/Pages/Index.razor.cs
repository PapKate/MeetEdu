using Amazon.Runtime.Internal.Transform;

using Microsoft.AspNetCore.Components;

using MudBlazor;

namespace MeetEdu
{
    /// <summary>
    /// The main page
    /// </summary>
    public partial class Index
    {
        #region Private Members

        /// <summary>
        /// The search text
        /// </summary>
        private string? mSearchText;

        /// <summary>
        /// The universities
        /// </summary>
        private IEnumerable<UniversityResponseModel>? mUniversities;

        /// <summary>
        /// The search bar
        /// </summary>
        private SearchBar? mSearchBar;

        #endregion

        #region Protected Properties

        /// <summary>
        /// The navigation manager service
        /// </summary>
        [Inject]
        protected NavigationManager? NavigationManager { get; set; }

        /// <summary>
        /// The controller
        /// </summary>
        [Inject]
        protected MeetEduController Controller { get; set; } = default!;

        /// <summary>
        /// The controller
        /// </summary>
        [Inject]
        protected MeetCoreController CoreController { get; set; } = default!;

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public Index() : base()
        {

        }

        #endregion

        #region Protected Methods

        /// <inheritdoc/>
        protected override async Task OnInitializedAsync()
        {
            var response = await Controller.GetUniversitiesAsync(null);

            if (response is null || response.Value.IsNullOrEmpty())
                return;

            mUniversities = response.Value;

            //AddMockData();
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Navigates to the respected page of professors or departments of the current department
        /// </summary>
        private void UniversityBox_OnClick(string universityId)
        {
            // If no navigation manager is found...
            if (NavigationManager is null)
                // Returns
                return;

            NavigationManager.NavigateToDepartmentsPage(new Dictionary<string, string?>() 
            {
                new("UniversityId", universityId)
            });
        }

        private async void AddMockData()
        {
            var entryPoint = new EntryPoint(CoreController);
            
            // Add the universities
            //await entryPoint.AddUniversitiesAsync();

            if(EntryPoint.Universities.IsNullOrEmpty())
            {
                EntryPoint.Universities = mUniversities!.ToList();
            }

            await entryPoint.AddDepartmentsAsync();

        }

        #endregion
    }
}
