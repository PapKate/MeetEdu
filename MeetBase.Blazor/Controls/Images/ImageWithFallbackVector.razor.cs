using Microsoft.AspNetCore.Components;

namespace MeetBase.Blazor
{
    public partial class ImageWithFallbackVector : BaseImageWithFallback
    {
        #region Private Members

        /// <summary>
        /// The vector component
        /// </summary>
        private DynamicComponent? mVectorComponent;

        #endregion

        #region Public Properties

        /// <summary>
        /// The vector type
        /// </summary>
        [Parameter]
        [EditorRequired]
        public required Type VectorType { get; set; }

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
                    vector.Color = FallbackIconColor;
                }
            }
        }

        #endregion
    }
}
