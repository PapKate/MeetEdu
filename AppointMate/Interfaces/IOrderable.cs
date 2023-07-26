namespace AppointMate
{
    /// <summary>
    /// Provides abstractions for an object that has assignable ordering rules
    /// </summary>
    public interface IOrderable : IReadOnlyOrderable
    {
        #region Properties

        /// <summary>
        /// The ordering rules
        /// </summary>
        new IEnumerable<OrderRule>? Rules { get; set; }

        /// <summary>
        /// The ordering rules
        /// </summary>
        IEnumerable<OrderRule>? IReadOnlyOrderable.Rules => Rules;

        #endregion
    }

    /// <summary>
    /// Provides abstractions for an object with ordering rules
    /// </summary>
    public interface IReadOnlyOrderable
    {
        #region Properties

        /// <summary>
        /// The order rules
        /// </summary>
        IEnumerable<OrderRule>? Rules { get; }

        #endregion
    }
}
