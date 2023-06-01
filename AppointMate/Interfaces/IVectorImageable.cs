namespace AppointMate
{
    /// <summary>
    /// Provides abstractions for an object that presents a vector image
    /// </summary>
    public interface IVectorImageable
    {
        #region Properties

        /// <summary>
        /// The source of the image
        /// </summary>
        VectorSource? VectorSource { get; set; }

        #endregion
    }

    /// <summary>
    /// Provides abstractions for an object that presents an icon path
    /// </summary>
    public interface IIconPathDatable
    {
        #region Properties

        /// <summary>
        /// The icon path data
        /// </summary>
        string IconPathData { get; set; }

        #endregion
    }
}
