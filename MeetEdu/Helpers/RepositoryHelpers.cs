using MeetBase.Web;

using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

using System.Linq.Expressions;
using System.Threading;

namespace MeetEdu
{
    /// <summary>
    /// Helper Methods for the repositories
    /// </summary>
    public static class RepositoryHelpers
    {
        #region Public Methods

        /// <summary>
        /// Adds the specified <paramref name="entity"/> to the specified <paramref name="collection"/>
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity</typeparam>
        /// <param name="collection">The collection</param>
        /// <param name="entity">The entity</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns></returns>
        public static async Task<TEntity> AddAsync<TEntity>(this IMongoCollection<TEntity> collection, TEntity entity, CancellationToken cancellationToken = default)
            where TEntity : BaseEntity
        {
            await collection.InsertOneAsync(entity, null, cancellationToken);

            return entity;
        }

        /// <summary>
        /// Adds the specified <paramref name="entities"/> to the specified <paramref name="collection"/>
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity</typeparam>
        /// <param name="collection">The collection</param>
        /// <param name="entities">The entity</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns></returns>
        public static async Task<IEnumerable<TEntity>> AddRangeAsync<TEntity>(this IMongoCollection<TEntity> collection, IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
            where TEntity : BaseEntity
        {
            await collection.InsertManyAsync(entities, null, cancellationToken);
            return entities;
        }

        /// <summary>
        /// Returns the first document that matches the requirements and null if none is found
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity</typeparam>
        /// <param name="collection">The collection</param>
        /// <param name="filter">The filter expression</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns></returns>
        public static async Task<TEntity> FirstOrDefaultAsync<TEntity>(this IMongoCollection<TEntity> collection, Expression<Func<TEntity, bool>> filter, CancellationToken cancellationToken = default)
            where TEntity : BaseEntity
        {
            var result = await collection.FindAsync<TEntity>(filter);

            return await result.FirstOrDefaultAsync(cancellationToken);
        }

        /// <summary>
        /// Returns the first document that matches the requirements and null if none is found
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity</typeparam>
        /// <param name="collection">The collection</param>
        /// <param name="id">The id of the document</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns></returns>
        public static Task<TEntity> FirstOrDefaultAsync<TEntity>(this IMongoCollection<TEntity> collection, ObjectId id, CancellationToken cancellationToken = default)
            where TEntity : BaseEntity
            => collection.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        /// <summary>
        /// Returns the first document that matches the requirements
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity</typeparam>
        /// <param name="collection">The collection</param>
        /// <param name="filter">The filter expression</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns></returns>
        public static async Task<TEntity> FirstAsync<TEntity>(this IMongoCollection<TEntity> collection, Expression<Func<TEntity, bool>> filter, CancellationToken cancellationToken = default)
            where TEntity : BaseEntity
        {
            var result = await collection.FindAsync<TEntity>(filter);

            return await result.FirstAsync(cancellationToken);
        }

        /// <summary>
        /// Returns the first document that matches the requirements and null if none is found
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity</typeparam>
        /// <param name="collection">The collection</param>
        /// <param name="id">The id of the document</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns></returns>
        public static Task<TEntity> FirstAsync<TEntity>(this IMongoCollection<TEntity> collection, ObjectId id, CancellationToken cancellationToken = default)
            where TEntity : BaseEntity
            => collection.FirstAsync(x => x.Id == id, cancellationToken);

        /// <summary>
        /// Returns the documents that match the requirements and null if none is found
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity</typeparam>
        /// <param name="collection">The collection</param>
        /// <param name="filter">The filter</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns></returns>
        public static async Task<IEnumerable<TEntity>> SelectAsync<TEntity>(this IMongoCollection<TEntity> collection, Expression<Func<TEntity, bool>> filter, CancellationToken cancellationToken = default)
            where TEntity : BaseEntity
        {
            var items = await collection.FindAsync(filter, cancellationToken: cancellationToken);

            return await items.ToListAsync();
        }

        /// <summary>
        /// Returns the first document that matches the requirements and throws a <see cref="InvalidOperationException"/> if none is found
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity</typeparam>
        /// <param name="collection">The collection</param>
        /// <param name="filter">The filter expression</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns></returns>
        public static async Task<bool> AnyAsync<TEntity>(this IMongoCollection<TEntity> collection, Expression<Func<TEntity, bool>> filter, CancellationToken cancellationToken = default)
            where TEntity : BaseEntity
        {
            var result = await collection.FindAsync<TEntity>(filter);

            return await result.AnyAsync(cancellationToken);
        }

        /// <summary>
        /// Returns the first document that matches the requirements and throws a <see cref="InvalidOperationException"/> if none is found
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity</typeparam>
        /// <param name="collection">The collection</param>
        /// <param name="id">The id of the document</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns></returns>
        public static Task<bool> AnyAsync<TEntity>(this IMongoCollection<TEntity> collection, ObjectId id, CancellationToken cancellationToken = default)
            where TEntity : BaseEntity
            => collection.AnyAsync(x => x.Id == id, cancellationToken);

        /// <summary>
        /// Updates the specified <paramref name="entity"/> while keeping its id the same.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity</typeparam>
        /// <param name="collection">The collection</param>
        /// <param name="entity">The new updated document</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns></returns>
        public static async Task<TEntity> UpdateAsync<TEntity>(this IMongoCollection<TEntity> collection, TEntity entity, CancellationToken cancellationToken = default)
            where TEntity : BaseEntity
        {
            var result = await collection.ReplaceOneAsync(x => x.Id == entity.Id, entity, new ReplaceOptions() { IsUpsert = false }, cancellationToken);

            return entity;
        }

        /// <summary>
        /// Updates the <typeparamref name="TEntity"/> with the specified <paramref name="id"/>
        /// </summary>
        /// <param name="collection">The collection</param>
        /// <param name="id">The id</param>
        /// <param name="model">The model</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns></returns>
        public static async Task<TEntity?> UpdateAsync<TEntity, TRequestModel>(this IMongoCollection<TEntity> collection, ObjectId id, TRequestModel model, CancellationToken cancellationToken = default)
            where TEntity : BaseEntity
            where TRequestModel : BaseRequestModel
        {
            var entity = await collection.FirstOrDefaultAsync(x => x.Id == id);

            if (entity is null)
                return null;

            DI.Mapper.Map(model, entity);
            return await collection.UpdateAsync(entity, cancellationToken);
        }

        /// <summary>
        /// Deletes the entity with the specified <paramref name="id"/> from the collection.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity</typeparam>
        /// <param name="collection">The collection</param>
        /// <param name="id">The id</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns></returns>
        public static async Task<TEntity> DeleteAsync<TEntity>(this IMongoCollection<TEntity> collection, ObjectId id, CancellationToken cancellationToken = default)
            where TEntity : BaseEntity
            => await collection.FindOneAndDeleteAsync(x => x.Id == id, cancellationToken: cancellationToken);

        #region Images

        /// <summary>
        /// Sets an image
        /// </summary>
        /// <param name="id">The id</param>
        /// <param name="directoryPath">The relative directory path</param>
        /// <param name="file">The file</param>
        /// <param name="updateModelAction"></param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns></returns>
        public static async Task<WebServerFailable<TEntity>> SetImageAsync<TRequest, TEntity>(ObjectId id, string directoryPath, IFormFile file, Func<TRequest, Task<WebServerFailable<TEntity>>> updateModelAction, CancellationToken cancellationToken = default)
            where TEntity : BaseEntity
            where TRequest : BaseRequestModel, IImageable, new()
        {
            var webRootPath = DI.GetRequiredService<IWebHostEnvironment>().WebRootPath;
            
            var path = Path.Combine(webRootPath, directoryPath);
            if(Directory.Exists(path))
            {
                var folderImages = Directory.GetFiles(path);
                var filteredImages = folderImages?.Where(x => x.Contains(id.ToString() + "-"));
                if(!filteredImages.IsNullOrEmpty())
                    foreach (var image in filteredImages)
                        File.Delete(image);
            }

            // Creates a directory for the object if it doesn't already exist
            Directory.CreateDirectory(Path.Combine(webRootPath, directoryPath));

            // Creates the relative path where the image will be saved
            var relativePath = Path.Combine(directoryPath, id.ToString() + "-" + Path.GetFileNameWithoutExtension(Path.GetRandomFileName()) + ".png");
            // Creates the path in the wwwroot folder where the image will be saved
            var filePath = Path.Combine(webRootPath, relativePath);

            using var stream = new FileStream(filePath, FileMode.Create);
            // Copies the file to the location
            await file.CopyToAsync(stream, cancellationToken);

            var link = Path.Combine(MeetEduConstants.HostURL, relativePath);
            // Gets the source of the image's location
            var url = new Uri(Path.Combine(link.Replace("\\", "/")));
            // Creates a model with the image source
            var model = new TRequest() { ImageUrl = url };

            // Updates the entity
            return await updateModelAction(model);
        }

        #endregion

        #endregion
    }
}
