using Amazon.Auth.AccessControlPolicy;

namespace MeetEdu
{
    /// <summary>
    /// Provides abstractions for an object with an assignable logical condition state
    /// </summary>
    public interface IConditionable : IReadOnlyConditionable
    {
        #region Properties

        /// <summary>
        /// The condition.
        /// </summary>
        new Condition Condition { get; set; }

        /// <summary>
        /// The condition.
        /// </summary>
        Condition IReadOnlyConditionable.Condition => Condition;

        #endregion
    }

    /// <summary>
    /// Provides abstractions for an object with a logical condition state
    /// </summary>
    public interface IReadOnlyConditionable
    {
        #region Properties

        /// <summary>
        /// The condition.
        /// </summary>
        Condition Condition { get; }

        #endregion
    }
}
