using Newtonsoft.Json;

namespace MeetBase
{
    /// <summary>
    /// A <see cref="JsonConverter{T}"/> used for converting a <typeparamref name="TValue"/> to a <see cref="string"/>
    /// </summary>
    /// <typeparam name="TValue">The type of the value</typeparam>
    public abstract class BaseTimeOnlyToStringJsonConverter<TValue> : JsonConverter<TValue?>
        where TValue : struct
    {
        #region Constants

        /// <summary>
        /// The format that is used for serializing and deserializing the Time
        /// </summary>
        public const string SerializationFormat = "HH:mm:ss";

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public BaseTimeOnlyToStringJsonConverter() : base()
        {

        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Reads the JSON representation of the <paramref name="reader"/> value into <typeparamref name="TValue"/>
        /// </summary>
        /// <param name="reader">The reader</param>
        /// <param name="objectType">The object type</param>
        /// <param name="existingValue">The existing value</param>
        /// <param name="hasExistingValue">True is the existing value has a value</param>
        /// <param name="serializer">The serializer</param>
        /// <returns></returns>
        public sealed override TValue? ReadJson(JsonReader reader, Type objectType, TValue? existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            // Get the serializer value
            var value = serializer.Deserialize<string>(reader);

            // If there isn't a value...
            if (value.IsNullOrEmpty())
                // Return the existing value
                return existingValue;

            // Convert the value
            return Convert(value);
        }

        /// <summary>
        /// Writes the JSON representation of the <paramref name="value"/>
        /// </summary>
        /// <param name="writer">The writer</param>
        /// <param name="value">The value to write</param>
        /// <param name="serializer">The serializer</param>
        public sealed override void WriteJson(JsonWriter writer, TValue? value, JsonSerializer serializer)
        {
            if (value is null)
            {
                if (ShouldSerializeToNullWhenTheValueIsNull())
                {
                    // Write null
                    writer.WriteNull();

                    // Return
                    return;
                }

                // Return
                return;
            }

            // Write the converted value
            writer.WriteValue(Convert(value.Value));
        }

        #endregion

        #region Protected Methods

        /// <summary>
        /// Returns a flag indicating whether when there isn't a value,
        /// a null value should be added to the Json
        /// </summary>
        /// <returns></returns>
        protected virtual bool ShouldSerializeToNullWhenTheValueIsNull() => false;

        /// <summary>
        /// Converts the <paramref name="value"/> to the respective <typeparamref name="TValue"/>
        /// </summary>
        /// <param name="value">The string value</param>
        /// <returns></returns>
        protected abstract TValue Convert(string value);

        /// <summary>
        /// Writes the JSON representation of the <paramref name="value"/>
        /// </summary>
        /// <param name="value">The <typeparamref name="TValue"/></param>
        /// <returns></returns>
        protected abstract string Convert(TValue value);

        #endregion
    }
}
