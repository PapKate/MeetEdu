namespace MeetBase
{
    /// <summary>
    /// Provides name meta data to an item
    /// </summary>
    public class NameAttribute : Attribute
    {
        #region Public Properties

        /// <summary>
        /// The name
        /// </summary>
        public string Name { get; }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="name">The name</param>
        public NameAttribute(string name)
        {
            Name = name.NotNullOrEmpty();
        }

        #endregion
    }
}
