using MongoDB.Bson;

namespace MeetEdu
{
    /// <summary>
    /// Extension methods for strings
    /// </summary>
    public static class StringExtensions
    {
        #region Public Methods

        /// <summary>
        /// Converts the specified <paramref name="s"/> to an <see cref="ObjectId"/>
        /// </summary>
        /// <param name="s">The string to convert</param>
        /// <returns></returns>
        public static ObjectId ToObjectId(this string s) => new ObjectId(s);

        #endregion
    }
}
