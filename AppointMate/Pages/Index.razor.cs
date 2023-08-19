using Microsoft.AspNetCore.Components;

namespace AppointMate
{
    public partial class Index
    {
        #region Public Properties

        /// <summary>
        /// The search text
        /// </summary>
        [Parameter]
        public string? SearchText { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public Index() : base()
        {

        }

        #endregion
    }
}
