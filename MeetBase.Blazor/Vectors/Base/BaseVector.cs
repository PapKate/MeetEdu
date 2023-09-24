using Microsoft.AspNetCore.Components;

namespace MeetBase.Blazor
{
    public class BaseVector : ComponentBase
    {
        #region Protected Members

        /// <summary>
        /// A flag indicating whether the more fitting color to contrast the <see cref="Color"/> is <see cref="PaletteColors.DarkGray"/> or <see cref="PaletteColors.White"/>
        /// </summary>
        protected bool mIsDark = true;

        /// <summary>
        /// According to the <see cref="mIsDark"/> it is either <see cref="PaletteColors.DarkGray"/> or <see cref="PaletteColors.White"/>
        /// </summary>
        protected string mDarkOrWhiteColor = PaletteColors.DarkGray;

        #endregion

        #region Private Members

        /// <summary>
        /// The member of the <see cref="Color"/> property
        /// </summary>
        private string? mColor = PaletteColors.Red;

        #endregion

        #region Public Properties

        /// <summary>
        /// The color
        /// </summary>
        [Parameter]
        public string Color 
        {
            get => mColor ?? PaletteColors.Black;
            set
            {
                mColor = value ?? PaletteColors.Black;
                mDarkOrWhiteColor = mColor.DarkOrWhite();
                mIsDark = mDarkOrWhiteColor == PaletteColors.DarkGray;
            }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public BaseVector() : base()
        {

        }

        #endregion
    }
}
