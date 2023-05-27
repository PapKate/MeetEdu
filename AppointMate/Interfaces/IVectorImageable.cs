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
}
