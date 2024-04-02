using Microsoft.AspNetCore.Components;

namespace MeetEdu
{
    public partial class FacultyPage
    {
        #region Private Members

        /// <summary>
        /// The staff members
        /// </summary>
        private IEnumerable<StaffMemberResponseModel>? mStaffMembers;

        #endregion

        #region Public Properties

        /// <summary>
        /// The search text
        /// </summary>
        [Parameter]
        [SupplyParameterFromQuery]
        public string? SearchText { get; set; }

        /// <summary>
        /// The university id
        /// </summary>
        [Parameter]
        [SupplyParameterFromQuery]
        public string? UniversityId { get; set; }

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

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public FacultyPage() : base()
        {

        }

        #endregion

        #region Protected Methods

        /// <inheritdoc/>
        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();

            var args = new DepartmentRelatedAPIArgs();

            //if (!UniversityId.IsNullOrEmpty())
            //    args.IncludeUniversities = new List<string>() { UniversityId };

            var staffMembers = new List<StaffMemberResponseModel>();

            var professorsResult = await Controller.GetProfessorsAsync(args);
            var secretaryResult = await Controller.GetSecretariesAsync(args);

            if (!professorsResult.Value.IsNullOrEmpty())
                staffMembers.AddRange(professorsResult.Value);

            //if(!secretaryResult.Value.IsNullOrEmpty())
            //    staffMembers.AddRange(secretaryResult.Value);

            mStaffMembers = staffMembers;
        }

        #endregion
    }
}
