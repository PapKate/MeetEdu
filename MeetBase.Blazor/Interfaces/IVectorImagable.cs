namespace MeetBase.Blazor
{
    /// <summary>
    /// Provides abstractions for a UI element that has an icon
    /// </summary>
    public interface IVectorImagable
    {
        #region Properties

        /// <summary>
        /// The vector source
        /// </summary>
        VectorSource? VectorSource { get; set; }

        #endregion
    }

    /// <summary>
    /// Provides abstractions for a UI element that has text
    /// </summary>
    public interface ITextConfiguration
    {
        #region Properties

        /// <summary>
        /// The font family
        /// </summary>
        string? FontFamily { get; set; }

        /// <summary>
        /// The font size
        /// </summary>
        string? FontSize { get; set; }

        /// <summary>
        /// The font weight
        /// </summary>
        string? FontWeight { get; set; }

        #endregion
    }
}
