using MongoDB.Driver.Linq;

using System.Linq.Expressions;

namespace AppointMate
{
    /// <summary>
    /// Extension methods for <see cref="IMongoQueryable{T}"/>
    /// </summary>
    public static class IMongoQueryableExtensions
    {
        /// <summary>
        /// Sorts the elements of a sequence
        /// </summary>
        /// <typeparam name="T">The type of the elements of source.</typeparam>
        /// <param name="source">A sequence of values to order.</param>
        /// <param name="keySelector">A function to extract a key from an element.</param>
        /// <param name="orderCondition">The order condition.</param>
        /// <returns></returns>
        public static IOrderedMongoQueryable<T> OrderBy<T>(this IMongoQueryable<T> source, Expression<Func<T, object>> keySelector, OrderCondition orderCondition)
            => orderCondition == OrderCondition.Ascending ? source.OrderBy(keySelector) : source.OrderByDescending(keySelector);
    }

    /// <summary>
    /// Extension methods for <see cref="IOrderedMongoQueryable{T}"/>
    /// </summary>
    public static class IOrderedMongoQueryableExtensions
    {
        /// <summary>
        /// Performs a subsequent ordering of the elements
        /// </summary>
        /// <typeparam name="T">The type of the elements of source.</typeparam>
        /// <param name="source">A sequence of values to order.</param>
        /// <param name="keySelector">A function to extract a key from an element.</param>
        /// <param name="orderCondition">The order condition,</param>
        /// <returns></returns>
        public static IOrderedMongoQueryable<T> ThenBy<T>(this IOrderedMongoQueryable<T> source, Expression<Func<T, object>> keySelector, OrderCondition orderCondition)
            => orderCondition == OrderCondition.Ascending ? source.ThenBy(keySelector) : source.ThenByDescending(keySelector);
    }
}
