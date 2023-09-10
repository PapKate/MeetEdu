namespace MeetBase
{
    /// <summary>
    /// Provides enumeration over the conditions
    /// </summary>
    public enum Condition
    {
        /// <summary>
        /// All the conditions must be satisfied
        /// </summary>
        AND = 0,

        /// <summary>
        /// At least one condition must be satisfied
        /// </summary>
        OR = 1,
    }
}
