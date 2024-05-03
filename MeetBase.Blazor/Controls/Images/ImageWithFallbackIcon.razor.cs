using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetBase.Blazor
{
    public partial class ImageWithFallbackIcon
    {
        #region Protected Members

        protected bool mIsFallbackIconVisible = false;

        #endregion

        #region Public Properties

        /// <summary>
        /// The additional css classes
        /// </summary>
        [Parameter]
        public string? CssClasses { get; set; }

        /// <summary>
        /// The id
        /// </summary>
        [Parameter]
        public string? Id { get; set; }

        /// <summary>
        /// The source of the image
        /// </summary>
        [Parameter]
        public string? ImageSource { get; set; }

        /// <summary>
        /// The alternate text
        /// </summary>
        [Parameter]
        public string? AlternateText { get; set; }

        /// <summary>
        /// The vector source of the fallback icon
        /// </summary>
        [Parameter]
        public VectorSource? FallbackIconVectorSource { get; set; }

        /// <summary>
        /// The background of the fallback icon
        /// </summary>
        [Parameter]
        public string? FallbackIconBackground { get; set; }

        /// <summary>
        /// The color of the fallback icon
        /// </summary>
        [Parameter]
        public string? FallbackIconColor { get; set; }

        /// <summary>
        /// The margin of the fallback icon
        /// </summary>
        [Parameter]
        public string? FallbackIconMargin { get; set; }

        /// <summary>
        /// The aspect ration of the component
        /// </summary>
        [Parameter]
        public string? AspectRatio { get; set; }

        /// <summary>
        /// The object's resized fit 
        /// </summary>
        [Parameter]
        public Stretch Stretch { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public ImageWithFallbackIcon() : base()
        {

        }

        #endregion

        #region Protected Methods

        protected override void OnInitialized()
        {
            base.OnInitialized();

            if (ImageSource is null)
                mIsFallbackIconVisible = true;
        }

        protected void OnImageError()
        {
            mIsFallbackIconVisible = true;
        }

        #endregion
    }
}
