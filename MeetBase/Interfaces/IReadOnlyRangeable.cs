namespace MeetBase
{
    /// <summary>
    /// Provides abstractions for an object that has values that define a range
    /// </summary>
    /// <typeparam name="T">The type of the values</typeparam>
    public interface IReadOnlyRangeable<out T>
        where T : IComparable<T>
    {
        #region Properties

        /// <summary>
        /// The minimum value
        /// </summary>
        T Minimum { get; }

        /// <summary>
        /// The maximum value
        /// </summary>
        T Maximum { get; }

        #endregion
    }
}
