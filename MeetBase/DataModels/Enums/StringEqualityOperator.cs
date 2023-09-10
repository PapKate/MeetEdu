namespace MeetBase
{
    /// <summary>
    /// Provides enumeration over the string equality operator
    /// </summary>
    public enum StringEqualityOperator
    {
        /// <summary>
        /// String contains a sub string value
        /// </summary>
        Contains = 0,

        /// <summary>
        /// String is equal to a value
        /// </summary>
        Equals = 1,

        /// <summary>
        /// String that starts with a value
        /// </summary>
        StartsWith = 2,

        /// <summary>
        /// String that ends with a value
        /// </summary>
        EndsWith = 3,

        /// <summary>
        /// String that satisfies a regex
        /// </summary>
        Regex = 4
    }
}
