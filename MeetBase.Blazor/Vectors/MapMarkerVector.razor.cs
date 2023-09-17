namespace MeetBase.Blazor
{
    public partial class MapMarkerVector : BaseVector
    {
        #region Constants

        /// <summary>
        /// The map marker body color CSS variable
        /// </summary>
        public const string MapMarkerBodyColorVariable = "--mapMarkerBodyColor";

        /// <summary>
        /// The map marker circle color CSS variable
        /// </summary>
        public const string MapMarkerCircleColorVariable = "--mapMarkerCircleDarkColor";

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public MapMarkerVector() : base()
        {

        }

        #endregion
    }
}
