using Microsoft.AspNetCore.Components;

namespace MeetBase.Blazor
{
    /// <summary>
    /// A <see cref="BaseBackgroundControl"/> that has a <see cref="Foreground"/> property
    /// </summary>
    public class BaseForegroundControl : BaseBackgroundControl
    {
        #region Public Properties

        /// <summary>
        /// The foreground
        /// </summary>
        [Parameter]
        public string? Foreground { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public BaseForegroundControl()
        {

        }

        #endregion
    }
}
