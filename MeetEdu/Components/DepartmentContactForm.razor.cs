using Microsoft.AspNetCore.Components;

namespace MeetEdu
{
    /// <summary>
    /// The department contact form
    /// </summary>
    public partial class DepartmentContactForm
    {
        #region Private Members

        /// <summary>
        /// The appointment
        /// </summary>
        private DepartmentContactMessageRequestModel mContactMessage = new();

        /// <summary>
        /// The phone number
        /// </summary>
        private string? mPhoneNumber;

        /// <summary>
        /// The vector component
        /// </summary>
        private DynamicComponent? mVectorComponent;

        #endregion

        #region Public Properties

        /// <summary>
        /// The department
        /// </summary>
        [Parameter]
        public DepartmentResponseModel? Department { get; set; }

        /// <summary>
        /// The department message template
        /// </summary>
        [Parameter]
        public DepartmentContactMessageTemplate? FormTemplate { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public DepartmentContactForm() : base()
        {

        }

        #endregion

        #region Protected Methods

        /// <inheritdoc/>
        protected override void OnAfterRender(bool firstRender)
        {
            base.OnAfterRender(firstRender);
            if(firstRender)
            {
                if (mVectorComponent?.Instance is BaseVector vector)
                {
                    vector.Color = Department!.Color;
                }
            }

        }

        #endregion
    }
}
