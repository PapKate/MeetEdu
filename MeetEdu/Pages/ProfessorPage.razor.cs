using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace MeetEdu
{
    /// <summary>
    /// The professor page
    /// </summary>
    public partial class ProfessorPage
    {
        #region Private Members

        /// <summary>
        /// The layouts
        /// </summary>
        private IEnumerable<LayoutResponseModel>? mLayouts;

        /// <summary>
        /// The appointment rules
        /// </summary>
        private IEnumerable<AppointmentRuleResponseModel>? mAppointmentRules;

        #endregion

        #region Public Properties

        /// <summary>
        /// The id
        /// </summary>
        [Parameter]
        public string? Id { get; set; }

        /// <summary>
        /// The professor
        /// </summary>
        public ProfessorResponseModel? Model { get; set; }

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
        /// The JS runtime service
        /// </summary>
        [Inject]
        protected IJSRuntime JSRuntime { get; set; } = default!;

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public ProfessorPage() : base()
        {

        }

        #endregion

        #region Protected Methods

        /// <inheritdoc/>
        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();

            if (Id is null)
                return;

            var result = await Controller.GetProfessorAsync(Id);

            if (result?.Value is null)
                return;

            Model = result.Value;

            var layoutsResult = await Controller.GetProfessorOfficeLayoutsAsync(new ProfessorAPIArgs() { IncludeProfessors = new List<string>() { Model.Id } });

            mLayouts = layoutsResult.Value;

            var rules = await Controller.GetAppointmentRulesAsync(new AppointmentRuleAPIArgs() { IncludeProfessors = new List<string>() { Model.Id } });

            mAppointmentRules = rules.Value;
        }

        /// <inheritdoc/>
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender);

            if (firstRender)
            {
                if (Model!.User!.Location is not null)
                {
                    await Task.Delay(100);
                    await JSRuntime.InvokeVoidAsync("ShowLeafletMap", Model!.Id, Model.User.Location.Latitude, Model.User.Location.Longitude);
                }
            }
        }

        #endregion
    }
}
