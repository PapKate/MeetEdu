using Microsoft.AspNetCore.Components;

namespace MeetBase.Blazor
{
    public partial class VectorIcon : IVectorImagable
    {
        #region Public Properties

        /// <summary>
        /// The vector source
        /// </summary>
        [Parameter]
        public VectorSource? VectorSource { get; set; }

        /// <summary>
        /// The color
        /// </summary>
        [Parameter]
        public string? Color { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public VectorIcon() : base()
        {

        }

        #endregion
    }
}