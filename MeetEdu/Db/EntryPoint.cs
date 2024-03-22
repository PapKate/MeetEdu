using Microsoft.AspNetCore.Components;

namespace MeetEdu
{
    /// <summary>
    /// Contains methods for filling the DB with data
    /// </summary>
    public class EntryPoint
    {
        #region Protected Properties

        /// <summary>
        /// The controller
        /// </summary>
        [Inject]
        protected MeetCoreController Controller { get; set; } = default!;

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public EntryPoint() : base()
        {

        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Adds labels to the DB
        /// </summary>
        /// <returns></returns>
        public async Task AddUniversityLabelsAsync()
        {
            var labels = new List<LabelRequestModel>()
            { 
            
            };

            foreach (var label in labels)
            {
                await Controller.AddUniversityLabelAsync(label);
            }
        }

        /// <summary>
        /// Adds universities to the DB
        /// </summary>
        /// <returns></returns>
        public async Task AddUniversitiesAsync()
        {

            var universities = new List<UniversityRequestModel>()
            { 
                new()
                {
                    
                }
            };
        }

        /// <summary>
        /// Adds departments to the DB
        /// </summary>
        /// <returns></returns>
        public async Task AddDepartmentsAsync()
        {

        }

        /// <summary>
        /// Adds secretaries to the DB
        /// </summary>
        /// <returns></returns>
        public async Task AddSecretariesAsync()
        {
            var users = new List<UserRequestModel>()
            {
                
            };

            var secretaries = new List<SecretaryRequestModel>()
            {
                new()
                {
                    // Architecture
                    DepartmentId = "65fc1663de2b55b1a071c2dd",
                    
                }
            };


        }

        #endregion
    }
}
