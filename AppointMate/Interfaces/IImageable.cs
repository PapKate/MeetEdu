namespace MeetEdu
{
    /// <summary>
    /// Provides abstractions for a model that provides URL for accessing an image
    /// </summary>
    public interface IImageable
    {
        #region Properties

        /// <summary>
        /// The image URL
        /// </summary>
        Uri? ImageUrl { get; set; }

        #endregion
    }
}
