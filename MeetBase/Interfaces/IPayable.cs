namespace MeetBase
{
    /// <summary>
    /// Provides abstractions for an object that has assignable values that
    /// represent a payment
    /// </summary>
    public interface IPayable : IReadOnlyPayable, IDateCreatable, INoteable
    {
        #region Properties

        /// <summary>
        /// The amount
        /// </summary>
        new decimal Amount { get; set; }

        /// <summary>
        /// The amount
        /// </summary>
        decimal IReadOnlyPayable.Amount => Amount;

        #endregion
    }

    /// <summary>
    /// Provides abstractions for an object that has values that
    /// represent a payment
    /// </summary>
    public interface IReadOnlyPayable : IReadOnlyDateCreatable, IReadOnlyNoteable
    {
        #region Properties

        /// <summary>
        /// The amount
        /// </summary>
        decimal Amount { get; }

        #endregion
    }
}
