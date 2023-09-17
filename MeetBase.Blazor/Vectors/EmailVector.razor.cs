using Microsoft.AspNetCore.Components;

namespace MeetBase.Blazor
{
    /// <summary>
    /// The email vector
    /// </summary>
    public partial class EmailVector : BaseVector
    {
        #region Constants

        /// <summary>
        /// The envelope top color CSS variable
        /// </summary>
        public const string EnvelopeTopColorVariable = "--envelopeTopcolor";

        /// <summary>
        /// The envelope sides shadow color CSS variable
        /// </summary>
        public const string EnvelopeSidesShadowColorVariable = "--envelopeSidesShadowColor";

        /// <summary>
        /// The envelope sides color CSS variable
        /// </summary>
        public const string EnvelopeSidesColorVariable = "--envelopeSidesColor";

        /// <summary>
        /// The envelope body color CSS variable
        /// </summary>
        public const string EnvelopeBodyColorVariable = "--envelopeBodyColor";

        /// <summary>
        /// The envelope body shadow color CSS variable
        /// </summary>
        public const string EnvelopeBodyShadowColorVariable = "--envelopeBodyShadowColor";

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public EmailVector() : base()
        {

        }

        #endregion
    }
}
