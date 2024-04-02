using Microsoft.AspNetCore.Components;

using System.ComponentModel.DataAnnotations;

namespace MeetEdu
{
    /// <summary>
    /// The professor contact form
    /// </summary>
    public partial class ProfessorContactForm
    {
        #region Private Members

        /// <summary>
        /// The appointment
        /// </summary>
        private AppointmentRequestModel mAppointment = new();

        /// <summary>
        /// The phone number
        /// </summary>
        private string? mPhoneNumber;

        /// <summary>
        /// The flag indicating whether the appointment is gonna be remote or not
        /// </summary>
        private bool mIsRemote;

        /// <summary>
        /// The selected rule
        /// </summary>
        private AppointmentRuleResponseModel? mRule;

        /// <summary>
        /// The vector component
        /// </summary>
        private DynamicComponent? mVectorComponent;

        #endregion

        #region Public Properties

        /// <summary>
        /// The professor
        /// </summary>
        [Parameter]
        public ProfessorResponseModel? Professor { get; set; }

        /// <summary>
        /// The appointment template
        /// </summary>
        [Parameter]
        public AppointmentContactMessageTemplate? FormTemplate { get; set; }

        /// <summary>
        /// The appointment rules
        /// </summary>
        [Parameter]
        public IEnumerable<AppointmentRuleResponseModel>? AppointmentRules { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public ProfessorContactForm() : base()
        {

        }

        #endregion

        #region Protected Methods

        /// <inheritdoc/>
        protected override void OnAfterRender(bool firstRender)
        {
            base.OnAfterRender(firstRender);
            if (firstRender)
            {
                if (mVectorComponent?.Instance is BaseVector vector)
                {
                    vector.Color = Professor!.User!.Color;
                }
            }

        }

        #endregion
    }
}
