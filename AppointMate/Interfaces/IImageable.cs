namespace AppointMate
{
    /// <summary>
    /// Provides abstractions for a model that provides URL for accessing a small, normal and large image
    /// </summary>
    public interface IImageable
    {
        #region Properties

        /// <summary>
        /// The small image URL
        /// </summary>
        Uri? SmallImageUrl { get; set; }

        /// <summary>
        /// The image URL
        /// </summary>
        Uri? NormalImageUrl { get; set; }

        /// <summary>
        /// The large image URL
        /// </summary>
        Uri? LargeImageUrl { get; set; }

        #endregion
    }
}
