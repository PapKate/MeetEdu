namespace MeetBase
{
    /// <summary>
    /// Helper methods for <see cref="Type"/>
    /// </summary>
    public static class TypeHelpers
    {
        #region Public Methods

        /// <summary>
        /// Gets the T from the <see cref="IEnumerable{T}"/> of the specified <paramref name="type"/>
        /// when it implements the <see cref="IEnumerable{T}"/> interface
        /// </summary>
        /// <param name="type">The type that implements the <see cref="IEnumerable{T}"/> interface</param>
        /// <returns></returns>3
        public static Type GetNonEnumerableType(Type type)
        {
            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(IEnumerable<>))
                return type.GetGenericArguments()[0];

            foreach (var iEnumerable in type.GetInterfaces())
                if (iEnumerable.IsGenericType && iEnumerable.GetGenericTypeDefinition() == typeof(IEnumerable<>))
                    return iEnumerable.GetGenericArguments()[0];

            return type;
        }

        #endregion
    }
}
