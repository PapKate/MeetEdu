namespace MeetBase
{
    /// <summary>
    /// Marks an item as ignorable
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = true)]
    public class IgnoreAttribute : Attribute
    {
        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public IgnoreAttribute()
        {

        }

        #endregion
    }
}
