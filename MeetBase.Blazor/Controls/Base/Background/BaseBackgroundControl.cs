using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Components;

namespace MeetBase.Blazor
{
    /// <summary>
    /// A control that has a <see cref="Background"/> property
    /// </summary>
    public class BaseBackgroundControl : BaseControl
    {
        #region Public Properties

        /// <summary>
        /// The background
        /// </summary>
        [Parameter]
        public string? Background { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public BaseBackgroundControl()
        {

        }

        #endregion

    }
}
