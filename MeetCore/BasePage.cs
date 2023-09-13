using Microsoft.AspNetCore.Components;

namespace MeetCore
{
    /// <summary>
    /// The base page template
    /// </summary>
    public class BasePage : ComponentBase
    {
        #region Public Properties

        /// <summary>
        /// The id
        /// </summary>
        [Parameter]
        public string? Id { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public BasePage() : base()
        {

        }

        #endregion
    }
}
