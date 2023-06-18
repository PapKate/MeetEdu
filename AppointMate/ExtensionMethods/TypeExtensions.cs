using System.Collections;

namespace AppointMate
{
    /// <summary>
    /// Extension methods for <see cref="Type"/>
    /// </summary>
    public static class TypeExtensions
    {
        #region Public Methods

        /// <summary>
        /// Checks whether the specified <paramref name="type"/> implements the <see cref="IEnumerable"/>
        /// or it's it self the <see cref="IEnumerable"/>
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool IsEnumerable(this Type type)
        {
            if (typeof(IEnumerable).IsAssignableFrom(type))
                return true;

            return type.GetInterfaces().Any(x => x == typeof(IEnumerable));
        }

        /// <summary>
        /// Checks whether the specified <paramref name="type"/> implements the <see cref="IEnumerable{T}"/>
        /// or it's it self the <see cref="IEnumerable{T}"/>
        /// </summary>
        /// <param name="type">The type</param>
        /// <returns></returns>
        public static bool IsGenericIEnumerable(this Type type)
        {
            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(IEnumerable<>))
                return true;

            return type.GetInterfaces().Any(x => x.IsGenericType && x.GetGenericTypeDefinition() == typeof(IEnumerable<>));
        }

        #endregion
    }
}
