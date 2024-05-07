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

        #region Methods

        /// <summary>
        /// Creates and returns a <see cref="IReadOnlyRangeable{T}"/>
        /// </summary>
        /// <param name="minimum">The minimum value</param>
        /// <param name="maximum">The maximum value</param>
        /// <returns></returns>
        public static IReadOnlyRangeable<T> Create(T minimum, T maximum)
            => new Range<T>(minimum, maximum);

        #endregion
    }
}
