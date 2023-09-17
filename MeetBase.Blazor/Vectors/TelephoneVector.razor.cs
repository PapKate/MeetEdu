using Microsoft.AspNetCore.Components;

namespace MeetBase.Blazor
{
    /// <summary>
    /// The telephone vector
    /// </summary>
    public partial class TelephoneVector : BaseVector
    {
        #region Constants

        /// <summary>
        /// The color CSS variable
        /// </summary>
        public const string TelephoneBodyColorVariable = "--telephoneBodyColor";

        /// <summary>
        /// The dark color CSS variable
        /// </summary>
        public const string TelephoneDarkColorVariable = "--telephoneDarkColor";

        /// <summary>
        /// The darker color CSS variable
        /// </summary>
        public const string TelephoneDarkerColorVariable = "--telephoneDarkerColor";

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public TelephoneVector() : base()
        {

        }

        #endregion
    }
}
