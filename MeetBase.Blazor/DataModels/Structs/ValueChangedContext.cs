namespace MeetBase.Blazor
{
    /// <summary>
    /// Provides information related to a value change
    /// </summary>
    public struct ValueChangedContext<T>
    {
        #region Public Properties

        /// <summary>
        /// The old value
        /// </summary>
        public T OldValue { get; }

        /// <summary>
        /// The new value
        /// </summary>
        public T NewValue { get; }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="oldValue">The old value</param>
        /// <param name="newValue">The new value</param>
        public ValueChangedContext(T oldValue, T newValue)
        {
            OldValue = oldValue;
            NewValue = newValue;
        }

        #endregion
    }
}
