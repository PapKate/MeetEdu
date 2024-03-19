
using AutoMapper;
using AutoMapper.EquivalencyExpression;
using AutoMapper.Internal;

using MongoDB.Bson;

using System.Reflection;

namespace MeetEdu
{
    /// <summary>
    /// Extension methods for <see cref="IServiceCollection"/>
    /// </summary>
    public static class IServiceCollectionExtensions
    {
        #region Constants

        /// <summary>
        /// The maps of the namespaces between the entities and the DTOs
        /// </summary>
        public static readonly IEnumerable<EntityToDTONamespaceMap> EntityToDTONamespaceMaps = new List<EntityToDTONamespaceMap>()
        {
            // Atom.Web.Server.Argon -> Argon.Web
            new(typeof(MemberEntity).Namespace!, typeof(MemberResponseModel).Namespace!)
        };

        #endregion

        #region Public Methods

        /// <summary>
        /// Adds the pre-configured <see cref="Mapper"/> in the <paramref name="services"/>
        /// </summary>
        /// <param name="services">The services</param>
        /// <returns></returns>
        public static IServiceCollection AddMapper(this IServiceCollection services)
        {
            // The request model types
            var requestModelTypes = new List<Type>();
            // The response model types
            var responseModelTypes = new List<Type>();
            // The entity types
            var entityTypes = new List<Type>();
            // The embedded entity types
            var embeddedEntityTypes = new List<Type>();
            // The API args types
            var apiArgTypes = new List<Type>();

            // Create the configuration
            var configuration = new MapperConfiguration((cfg) =>
            {
                cfg.AddCollectionMappers();
                //cfg.Internal().AllowAdditiveTypeMapCreation = true;
                cfg.Internal().MethodMappingEnabled = false;
                
                var assemblies = new List<Assembly>
                {
                    // For the request models
                    Assembly.GetAssembly(typeof(UserRequestModel))!,
                    Assembly.GetAssembly(typeof(UserEntity))!
                };

                // For every Atom assembly...
                foreach (var assembly in assemblies)
                {
                    // For every type
                    foreach (var type in assembly.GetTypes())
                    {
                        if (type.Name.EndsWith(FrameworkConstructionExtensions.RequestModelSuffix))
                            requestModelTypes.Add(type);

                        if (type.Name.EndsWith(FrameworkConstructionExtensions.EntitySuffix))
                        {
                            entityTypes.Add(type);

                            if (type.Name.Contains(FrameworkConstructionExtensions.EmbeddedPrefix))
                                embeddedEntityTypes.Add(type);
                        }

                        if (type.Name.EndsWith(FrameworkConstructionExtensions.ResponseModelSuffix))
                            responseModelTypes.Add(type);
                    }
                }

                // For every request model type...(RequestModel / CreateRequestModel / UpdateRequestModel -> Entity)
                foreach (var requestModelType in requestModelTypes)
                {
                    if (requestModelType == typeof(ProfessorRequestModel))
                        continue;

                    // Get the namespace map for the request DTO if any
                    var map = EntityToDTONamespaceMaps.FirstOrDefault(x => x.DTONamespace == requestModelType.Namespace);

                    // Get the prefix of the request model
                    var requestModelNamePrefix = requestModelType.Name.Replace(FrameworkConstructionExtensions.RequestModelSuffix, string.Empty);

                    // Get the entity type is any with the same prefix
                    var entityType = entityTypes.FirstOrDefault(x => (map is null ? true : map.EntityNamespace == x.Namespace) && x.Name.Replace(FrameworkConstructionExtensions.EntitySuffix, string.Empty) == requestModelNamePrefix);

                    // If there is an entity type...
                    if (entityType != null)
                        // Create a map
                        cfg.CreateMap(requestModelType, entityType);
                }

                // For every entity...(Entity -> Entity), (Entity -> EmbeddedEntity), (Entity -> ResponseModel)
                foreach (var entityType in entityTypes)
                {
                    // Get the namespace map for the entity if any
                    var map = EntityToDTONamespaceMaps.FirstOrDefault(x => x.EntityNamespace == entityType.Namespace);

                    // Create the map
                    cfg.CreateMap(entityType, entityType);

                    // Get the prefix of the model
                    var entityNamePrefix = entityType.Name.Replace(FrameworkConstructionExtensions.EntitySuffix, string.Empty);

                    // Get the response model type
                    var responseModelType = responseModelTypes.FirstOrDefault(x => (map is null ? true : x.Namespace == map.DTONamespace) && x.Name.Replace(FrameworkConstructionExtensions.ResponseModelSuffix, string.Empty) == entityNamePrefix);

                    // If there is a response model type...
                    if (responseModelType != null)
                        // Create a map
                        cfg.CreateMap(entityType, responseModelType);

                    // Get the embedded related entity type if any
                    var embeddedEntityType = entityTypes.FirstOrDefault(x => (map is null ? true : x.Namespace == map.EntityNamespace) && x.Name == FrameworkConstructionExtensions.EmbeddedPrefix + entityType.Name);

                    // If there is an embedded entity type...
                    if (embeddedEntityType != null)
                        // Create a map
                        cfg.CreateMap(entityType, embeddedEntityType);
                }

                cfg.CreateMap<ProfessorRequestModel, ProfessorEntity>()
                    .ForMember(x => x.Lectures, x =>
                    {
                        x.Ignore();
                    })
                    .AfterMap((request, entity) => 
                    {
                        if (request.Lectures is null)
                            return;
                        entity.Lectures = request.Lectures.ToList();
                    });

                // The property types that should be ignored
                var ignoredDestinationTypes = new List<Type>();

                // Add embedded types and the enumerable presentation of the embedded entity types
                ignoredDestinationTypes.AddRange(embeddedEntityTypes);
                ignoredDestinationTypes.AddRange(embeddedEntityTypes.Select(x => typeof(IEnumerable<>).MakeGenericType(x)));

                // Add the entities that contain an embedded entity type as a property
                var entityPropertyTypesToIgnore = entityTypes.Where(x => x.GetProperties().Any(y => ignoredDestinationTypes.Contains(y.PropertyType))).ToList();
                ignoredDestinationTypes.AddRange(entityPropertyTypesToIgnore);
                ignoredDestinationTypes.AddRange(entityPropertyTypesToIgnore.Select(x => typeof(IEnumerable<>).MakeGenericType(x)));

                // Do not map based on the ignored destination types
                cfg.Internal().ForAllPropertyMaps((propertyMap) => true, (propertyMap, opts) =>
                {
                    if (propertyMap.SourceType != propertyMap.DestinationType && ignoredDestinationTypes.Contains(propertyMap.DestinationType))
                        opts.Ignore();
                });

                cfg.Internal().ForAllPropertyMaps(
                    propertyMap => propertyMap.SourceMember is not null &&
                                   propertyMap.TypeMap.SourceType.Name.EndsWith(FrameworkConstructionExtensions.EntitySuffix) &&
                                   propertyMap.TypeMap.DestinationType.Name.EndsWith(FrameworkConstructionExtensions.EntitySuffix) &&
                                   propertyMap.SourceType.InheritsFrom(typeof(IEnumerable<>)) &&
                                   propertyMap.DestinationType.InheritsFrom(typeof(IEnumerable<>)),
                    (propertyMap, c) => c.MapFrom(propertyMap.SourceMember.Name));

                // Do not map values when the values of the properties of the request models are null
                cfg.Internal().ForAllMaps((map, options) =>
                {
                    if (map.SourceType.Name.EndsWith(FrameworkConstructionExtensions.RequestModelSuffix) && map.DestinationType.Name.EndsWith(FrameworkConstructionExtensions.EntitySuffix))
                        options.ForAllMembers(x =>
                        {
                            x.Condition((_, _, sourceValue) => sourceValue is not null);
                            x.UseDestinationValue();
                        });
                });

                // Set the DateModified to the current date after the map if the entity implements the IDateModifiable
                cfg.Internal().ForAllMaps((map, options) =>
                {
                    if (map.SourceType.Name.EndsWith(FrameworkConstructionExtensions.RequestModelSuffix) && map.DestinationType.GetInterfaces().Any(x => x == typeof(IDateModifiable)))
                        options.AfterMap((source, destination) => ((IDateModifiable)destination).DateModified = DateTimeOffset.Now);
                });

                cfg.Internal().ForAllMaps((map, options) =>
                {
                    var sourceTypeInterfaces = map.SourceType.GetInterfaces();
                    var destinationTypeInterfaces = map.DestinationType.GetInterfaces();

                    // If the destination is an embeddable entity and the source isn't of the same type...
                    // NOTE: Such mappings are used for creating an embeddable entity from a standard entity!
                    if (map.SourceType != map.DestinationType && sourceTypeInterfaces.Any(x => x == typeof(IIdentifiable<ObjectId>)) && destinationTypeInterfaces.Any(x => x == typeof(IEmbeddableIdentifiable<ObjectId>)))
                    {
                        // Never map the id property
                        options.ForMember(nameof(IIdentifiable.Id), config => config.Ignore());
                        options.ForMember(nameof(IEmbeddableIdentifiable.Source), config => config.MapFrom(nameof(IIdentifiable.Id)));
                    }

                    // If the destination is an entity that stores its creation date...
                    if (map.DestinationType.Name.EndsWith(FrameworkConstructionExtensions.EntitySuffix) && sourceTypeInterfaces.Any(y => y == typeof(IDateCreatable)) && destinationTypeInterfaces.Any(y => y == typeof(IDateCreatable)))
                        // Never map the date created property
                        options.ForMember(nameof(IDateCreatable.DateCreated), config => config.Ignore());

                    // If the destination is an entity that stores its modification date...
                    if (map.DestinationType.Name.EndsWith(FrameworkConstructionExtensions.EntitySuffix) && sourceTypeInterfaces.Any(y => y == typeof(IDateModifiable)) && destinationTypeInterfaces.Any(y => y == typeof(IDateModifiable)))
                        // Never map the date modified property
                        options.ForMember(nameof(IDateModifiable.DateModified), config => config.Ignore());
                });

                // Custom Maps

                // Uri
                cfg.CreateMap<Uri?, string?>().ConstructUsing((uri) => uri == null ? string.Empty : uri.ToString());
                cfg.CreateMap<string?, Uri?>().ConstructUsing((str) => str == null ? null : new Uri(str));

                // DateOnly
                cfg.CreateMap<DateTime, DateOnly>().ConstructUsing((date) => DateOnly.FromDateTime(date));
                cfg.CreateMap<DateOnly, DateTime>().ConstructUsing((date) => date.ToDateTime());
                cfg.CreateMap<DateTime?, DateOnly?>().ConstructUsing((date) => date == null ? null : DateOnly.FromDateTime(date.Value));
                cfg.CreateMap<DateOnly?, DateTime?>().ConstructUsing((date) => date == null ? null : date.Value.ToDateTime());

                // DateTime to DateTimeOffset
                cfg.CreateMap<DateTime, DateTimeOffset>().ConstructUsing((value) => new DateTimeOffset(value));
                cfg.CreateMap<DateTime?, DateTimeOffset?>().ConstructUsing((value) => value == null ? null : new DateTimeOffset(value.Value));

                // DateTimeOffset from DateTime (NOTE: We always store date times in UTC in MongoDb)
                cfg.CreateMap<DateTimeOffset, DateTime>().ConstructUsing((value) => value.UtcDateTime);
                cfg.CreateMap<DateTimeOffset?, DateTime?>().ConstructUsing((value) => value == null ? null : value.Value.UtcDateTime);
                
            });

            // Create the mapper
            var mapper = new Mapper(configuration);

            // Add the mapper to the services
            services.AddSingleton(mapper);

            // Return for chaining
            return services;
        }

        #endregion
    }

    /// <summary>
    /// The map from entities to data transfer objects
    /// </summary>
    public record EntityToDTONamespaceMap
    {
        #region Public Properties

        /// <summary>
        /// The namespace of the entity
        /// </summary>
        public string EntityNamespace { get; set; }

        /// <summary>
        /// The namespace of the DTO
        /// </summary>
        public string DTONamespace { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="entityNamespace">The namespace of the entity</param>
        /// <param name="dtoNamespace">The namespace of the DTO</param>
        public EntityToDTONamespaceMap(string entityNamespace, string dtoNamespace) : base()
        {
            EntityNamespace = entityNamespace.NotNullOrEmpty();
            DTONamespace = dtoNamespace.NotNullOrEmpty();
        }

        #endregion
    }
}
