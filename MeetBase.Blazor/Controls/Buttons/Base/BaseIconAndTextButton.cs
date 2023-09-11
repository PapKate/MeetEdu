using Microsoft.AspNetCore.Components;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetBase.Blazor
{
    public class BaseIconAndTextButton : BaseButton, IVectorImagable, ITextConfiguration
    {
        #region Public Properties

        /// <summary>
        /// The icon's placement
        /// </summary>
        [Parameter]
        public ContentPlacement IconPlacement { get; set; }
        
        /// <summary>
        /// The SVG icon path
        /// </summary>
        [Parameter]
        public VectorSource? VectorSource { get; set; }

        /// <summary>
        /// The text
        /// </summary>
        [Parameter]
        public string? Text { get; set; }

        /// <summary>
        /// The font family
        /// </summary>
        [Parameter]
        public string? FontFamily { get; set; }

        /// <summary>
        /// The font size
        /// </summary>
        [Parameter]
        public string? FontSize { get; set; }

        /// <summary>
        /// The font weight
        /// </summary>
        [Parameter]
        public string? FontWeight { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public BaseIconAndTextButton()
        {

        }

        #endregion
    }
}
