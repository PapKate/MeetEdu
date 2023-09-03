using Atom;

using System.Collections;
using System.Reflection;

namespace MeetEdu
{
    /// <summary>
    /// The base for all the classes that are used as arguments
    /// </summary>
    public abstract class BaseArgs
    {
        #region Private Members

        /// <summary>
        /// The maps
        /// </summary>
        private static readonly ConcurrentCollection<ArgsToArgsMap> mMaps = new();

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public BaseArgs() : base()
        {

        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Creates and returns an instance of <typeparamref name="TArgs"/> from the current arguments
        /// by copying the values of the properties with the same name and the same property type of
        /// the arguments
        /// </summary>
        /// <typeparam name="TArgs">The type of the arguments</typeparam>
        /// <returns></returns>
        public TArgs ToArgs<TArgs>()
            where TArgs : BaseArgs, new()
        {
            // Create the arguments
            var result = new TArgs();

            var sourceArgsType = GetType();
            var destinationArgsType = typeof(TArgs);

            // Get the map between the arguments if any
            var map = mMaps.FirstOrDefault(x => x.SourceArgsType == sourceArgsType && x.DestinationArgsType == destinationArgsType);

            // If there isn't a map...
            if (map is null)
            {
                // Create it
                map = new ArgsToArgsMap(sourceArgsType, destinationArgsType);

                // Add it to the maps
                mMaps.Add(map);
            }

            // For every property pair...
            foreach (var pair in map.SourcePropertyToDestinationPropertyPairs)
                // Copy the value of the source arguments property to the destination arguments
                pair.Value.SetValue(result, pair.Key.GetValue(this));

            // Return the result
            return result;
        }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            var mapper = new Dictionary<string, string?>();

            foreach (var property in GetType().GetProperties().Where(x => x.CanRead && x.CanWrite))
            {
                var value = property.GetValue(this);

                if (value is null)
                    continue;

                if (property.PropertyType != typeof(string) && property.PropertyType.IsGenericIEnumerable())
                {
                    if (((IEnumerable)value).IsNullOrEmpty())
                        continue;

                    mapper.Add(property.Name, ((IEnumerable)value).Cast<object>().AggregateString());
                }
                else
                    mapper.Add(property.Name, value.ToString());
            }

            return mapper.Where(x => x.Value is not null).AggregateString(x => x.Key + ": " + x.Value);
        }

        #endregion

        #region Private Classes

        private class ArgsToArgsMap
        {
            #region Public Properties

            /// <summary>
            /// The type of the source arguments
            /// </summary>
            public Type SourceArgsType { get; }

            /// <summary>
            /// The type of the destination arguments
            /// </summary>
            public Type DestinationArgsType { get; }

            /// <summary>
            /// Pairs between the source and the destination properties that can have their values copied
            /// </summary>
            public IEnumerable<KeyValuePair<PropertyInfo, PropertyInfo>> SourcePropertyToDestinationPropertyPairs { get; }

            #endregion

            #region Constructors

            /// <summary>
            /// Default constructor
            /// </summary>
            public ArgsToArgsMap(Type sourceArgsType, Type destinationArgsType) : base()
            {
                SourceArgsType = sourceArgsType ?? throw new ArgumentNullException(nameof(sourceArgsType));
                DestinationArgsType = destinationArgsType ?? throw new ArgumentNullException(nameof(destinationArgsType));

                // Get the properties of the source arguments type
                var sourceArgsTypeProperties = SourceArgsType.GetProperties();

                // Get the properties of the destination arguments type
                var desintationArgsTypeProperties = DestinationArgsType.GetProperties();

                var sourcePropertyToDestinationPropertyPairs = new List<KeyValuePair<PropertyInfo, PropertyInfo>>();

                // For every source argument type property...
                foreach (var sourceArgsTypeProperty in sourceArgsTypeProperties.Where(x => x.CanRead))
                {
                    // Get the destination property with the same name and type that can be written to if any
                    var destinationArgsTypeProperty = desintationArgsTypeProperties.FirstOrDefault(x => x.CanWrite && x.Name == sourceArgsTypeProperty.Name && x.PropertyType == sourceArgsTypeProperty.PropertyType);

                    // If no property is found...
                    if (destinationArgsTypeProperty is null)
                        // Continue
                        continue;

                    sourcePropertyToDestinationPropertyPairs.Add(new KeyValuePair<PropertyInfo, PropertyInfo>(sourceArgsTypeProperty, destinationArgsTypeProperty));
                }

                // Set the pairs
                SourcePropertyToDestinationPropertyPairs = sourcePropertyToDestinationPropertyPairs;
            }



            #endregion
        }

        #endregion
    }
}
