using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace MeetEdu
{
    public partial class SecretaryPreview
    {
        #region Public Properties

        /// <summary>
        /// The model
        /// </summary>
        [Parameter]
        public EmbeddedSecretaryResponseModel? Model { get; set; }

        /// <summary>
        /// The department
        /// </summary>
        [Parameter]
        public DepartmentResponseModel? Department { get; set; }

        #endregion

        #region Protected Properties

        /// <summary>
        /// The navigation manager service
        /// </summary>
        [Inject]
        protected NavigationManager? NavigationManager { get; set; }

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
        public SecretaryPreview() : base()
        {

        }

        #endregion
    }
}
