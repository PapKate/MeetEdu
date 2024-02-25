
using Amazon.SecurityToken.Model;

using MeetEdu.Helpers;

using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

using System.Linq;

namespace MeetEdu
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
        public static List<FilterDefinition<T>> CreateFilters<T>(this AppointmentRuleAPIArgs args)
            where T : BaseEntity, IProfessorIdentifiable<ObjectId>
        {
            var filters = new List<FilterDefinition<T>>();

            // If there is a limit to the departments to include...
            if (!args.IncludeProfessors.IsNullOrEmpty())
            {
                var ids = args.IncludeProfessors.Select(x => x.ToObjectId()).ToList();
                filters.Add(Builders<T>.Filter.In(x => x.ProfessorId, ids));
            }

            // If there is a limit to the departments to exclude...
            if (!args.ExcludeProfessors.IsNullOrEmpty())
            {
                var ids = args.ExcludeProfessors.Select(x => x.ToObjectId()).ToList();
                filters.Add(Builders<T>.Filter.Nin(x => x.ProfessorId, ids));
            }

            return filters;
        }

        /// <summary>
        /// Creates the filters for the specified <paramref name="args"/>
        /// </summary>
        /// <typeparam name="T">The document type</typeparam>
        /// <param name="args">The arguments</param>
        /// <returns></returns>
        public static List<FilterDefinition<T>> CreateFilters<T>(this DepartmentRelatedAPIArgs args)
            where T : BaseEntity, IDepartmentIdentifiable<ObjectId>
        {
            var filters = new List<FilterDefinition<T>>();

            // If there is a limit to the departments to include...
            if (!args.IncludeDepartments.IsNullOrEmpty())
            {
                var ids = args.IncludeDepartments.Select(x => x.ToObjectId()).ToList();
                filters.Add(Builders<T>.Filter.In(x => x.DepartmentId, ids));
            }

            // If there is a limit to the departments to exclude...
            if (!args.ExcludeDepartments.IsNullOrEmpty())
            {
                var ids = args.ExcludeDepartments.Select(x => x.ToObjectId()).ToList();
                filters.Add(Builders<T>.Filter.Nin(x => x.DepartmentId, ids));
            }

            return filters;
        }

        /// <summary>
        /// Creates the filters for the specified <paramref name="args"/>
        /// </summary>
        /// <param name="args">The arguments</param>
        /// <returns></returns>
        public static List<FilterDefinition<DepartmentEntity>> CreateFilters(this DepartmentAPIArgs args)
        {
            var filters = new List<FilterDefinition<DepartmentEntity>>();

            // If there is a limit to the universities to include...
            if (!args.IncludeUniversities.IsNullOrEmpty())
            {
                var ids = args.IncludeUniversities.Select(x => x.ToObjectId()).ToList();
                filters.Add(Builders<DepartmentEntity>.Filter.In(x => x.UniversityId, ids));
            }

            // If there is a limit to the universities to exclude...
            if (!args.ExcludeUniversities.IsNullOrEmpty())
            {
                var ids = args.ExcludeUniversities.Select(x => x.ToObjectId()).ToList();
                filters.Add(Builders<DepartmentEntity>.Filter.Nin(x => x.UniversityId, ids));
            }

            // If there is a limit to the fields to include...
            if (!args.IncludeFields.IsNullOrEmpty())
                filters.Add(Builders<DepartmentEntity>.Filter.AnyIn(x => x.Fields, args.IncludeFields));

            // If there is a limit to the fields to exclude...
            if (!args.ExcludeFields.IsNullOrEmpty())
                filters.Add(Builders<DepartmentEntity>.Filter.AnyNin(x => x.Fields, args.ExcludeFields));

            // If there is a limit to the labels to include...
            if (!args.IncludeLabels.IsNullOrEmpty())
            {
                var ids = args.IncludeLabels.Select(x => x.ToObjectId()).ToList();
                filters.Add(Builders<DepartmentEntity>.Filter.In(x => x.UniversityId, ids));
            }

            // If there is a limit to the labels to exclude...
            if (!args.ExcludeLabels.IsNullOrEmpty())
            {
                var ids = args.ExcludeLabels.Select(x => x.ToObjectId()).ToList();
                filters.Add(Builders<DepartmentEntity>.Filter.Nin(x => x.UniversityId, ids));
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
            where T : BaseEntity
            => Builders<T>.Filter.And(filters);

        #endregion
    }
}
