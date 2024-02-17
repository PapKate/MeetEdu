namespace MeetBase.Blazor
{
    /// <summary>
    /// The coffee contact vector
    /// </summary>
    public partial class CoffeeContactVector : BaseVector
    {
        #region Constants

        /// <summary>
        /// The coffee contact cup handle color CSS variable
        /// </summary>
        public const string CoffeeContactCupHandle = "--coffeeContactCupHandle";

        /// <summary>
        /// The coffee contact cup body color CSS variable
        /// </summary>
        public const string CoffeeContactCupBody = "--coffeeContactCupBody";

        /// <summary>
        /// The coffee contact cup body dark color CSS variable
        /// </summary>
        public const string CoffeeContactCupBodyDark = "--coffeeContactCupBodyDark";

        /// <summary>
        /// The coffee contact cup plate color CSS variable
        /// </summary>
        public const string CoffeeContactCupPlate = "--coffeeContactCupPlate";

        /// <summary>
        /// The coffee contact cup plate dark color CSS variable
        /// </summary>
        public const string CoffeeContactCupPlateDark = "--coffeeContactCupPlateDark";

        /// <summary>
        /// The laptop frame dark color CSS variable
        /// </summary>
        public const string LaptopFrameDark = "--laptopFrameDark";

        /// <summary>
        /// The laptop frame dark shadow color CSS variable
        /// </summary>
        public const string LaptopFrameDarkShadow = "--laptopFrameDarkShadow";

        /// <summary>
        /// The laptop frame color CSS variable
        /// </summary>
        public const string LaptopFrame = "--laptopFrame";

        /// <summary>
        /// The laptop frame shadow color CSS variable
        /// </summary>
        public const string LaptopFrameShadow = "--laptopFrameShadow";

        /// <summary>
        /// The laptop screen body color CSS variable
        /// </summary>
        public const string LaptopScreenBody = "--laptopScreenBody";

        /// <summary>
        /// The laptop screen body shadow color CSS variable
        /// </summary>
        public const string LaptopScreenBodyShadow = "--laptopScreenBodyShadow";

        /// <summary>
        /// The laptop screen text color CSS variable
        /// </summary>
        public const string LaptopScreenText = "--laptopScreenText";

        /// <summary>
        /// The laptop screen face color CSS variable
        /// </summary>
        public const string LaptopScreenFace = "--laptopScreenFace";

        /// <summary>
        /// The laptop keyboard color CSS variable
        /// </summary>
        public const string LaptopKeyboard = "--laptopKeyboard";

        /// <summary>
        /// The laptop keyboard dark color CSS variable
        /// </summary>
        public const string LaptopKeyboardDark = "--laptopKeyboardDark";

        #endregion

        #region Public Properties

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public override string Name => "CoffeeContactVector";

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public CoffeeContactVector() : base()
        {

        }

        #endregion
    }
}
