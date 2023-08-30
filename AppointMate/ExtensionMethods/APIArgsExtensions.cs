
using Amazon.SecurityToken.Model;

using AppointMate.Helpers;

using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

using System.Linq;

namespace AppointMate
{
    /// <summary>
    /// Extension methods for <see cref="APIArgs"/>
    /// </summary>
    public static class APIArgsExtensions
    {
        #region Public Methods

        /// <summary>
        /// Creates the filters for the specified <paramref name="args"/>
        /// </summary>
        /// <typeparam name="T">The document type</typeparam>
        /// <param name="args">The arguments</param>
        /// <returns></returns>
        public static List<FilterDefinition<T>> CreateFilters<T>(this StafMemberAPIArgs args)
            where T : BaseEntity, IDepartmentIdentifiable<ObjectId>
        {
            var filters = new List<FilterDefinition<T>>();

            // If there is a limit to the companies to include...
            if (!args.IncludeDepartments.IsNullOrEmpty())
            {
                var ids = args.IncludeDepartments.Select(x => x.ToObjectId()).ToList();
                filters.Add(Builders<T>.Filter.In(x => x.DepartmentId, ids));
            }

            // If there is a limit to the companies to exclude...
            if (!args.ExcludeDepartments.IsNullOrEmpty())
            {
                var ids = args.ExcludeDepartments.Select(x => x.ToObjectId()).ToList();
                filters.Add(Builders<T>.Filter.Nin(x => x.DepartmentId, ids));
            }

            return filters;
        }

        /// <summary>
        /// Combines the specified <paramref name="filters"/>
        /// </summary>
        /// <typeparam name="T">The document type</typeparam>
        /// <param name="filters">The filters</param>
        /// <returns></returns>
        public static FilterDefinition<T> AggregateFilters<T>(this List<FilterDefinition<T>> filters)
            where T : BaseEntity, IDepartmentIdentifiable<ObjectId> 
            => Builders<T>.Filter.And(filters);

        #endregion
    }
}
