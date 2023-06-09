namespace AppointMate
{
    /// <summary>
    /// The company room response model
    /// </summary>
    public class CompanyRoomRequestModel : StandardRequestModel, IImageable
    {
        #region Public Properties

        /// <summary>
        /// The note
        /// </summary>
        public string? Note { get; set; }

        /// <summary>
        /// The small image URL
        /// </summary>
        public Uri? SmallImageUrl { get; set; }

        /// <summary>
        /// The image URL
        /// </summary>
        public Uri? NormalImageUrl { get; set; }

        /// <summary>
        /// The large image URL
        /// </summary>
        public Uri? LargeImageUrl { get; set; }

        /// <summary>
        /// The display theme
        /// </summary>
        public RoomDisplayTheme? DisplayTheme { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public CompanyRoomRequestModel() : base()
        {

        }

        #endregion
    }
}
