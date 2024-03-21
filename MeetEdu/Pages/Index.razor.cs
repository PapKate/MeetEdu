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
        /// The controller
        /// </summary>
        [Inject]
        protected MeetEduController Controller { get; set; } = default!;

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
            await base.OnInitializedAsync();

            var response = await Controller.GetUniversitiesAsync(null);

            if (response is null || response.Value.IsNullOrEmpty())
                return;

            mUniversities = response.Value;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Navigates to the respected page of professors or departments of the current department
        /// </summary>
        private async void UniversityBox_OnClick(string universityId)
        {
            if(mSearchBar!.IsSearchForDepartments)
            {
                var deparmtnets = await Controller.GetDepartmentsAsync(new DepartmentAPIArgs() 
                { 
                    IncludeUniversities = new List<string>() 
                    { 
                        universityId 
                    } 
                });
            }
            else
            {
                var professors = await Controller.GetProfessorsAsync(new DepartmentRelatedAPIArgs()
                {
                    IncludeUniversities = new List<string>()
                    {
                        universityId
                    }
                });
            }
        }

        #endregion
    }
}
