using MongoDB.Bson.IO;
using MongoDB.Bson.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Serializers;

namespace MeetEdu
{
    /// <summary>
    /// An implementation of the <see cref="IBsonSerializer"/> for the <see cref="TimeOnly"/>
    /// </summary>
    public class MongoDbTimeOnlySerializer : IBsonSerializer<TimeOnly>, IBsonDocumentSerializer
    {
        #region Singleton

        /// <summary>
        /// The instance
        /// </summary>
        public static MongoDbTimeOnlySerializer Instance { get; } = new MongoDbTimeOnlySerializer();

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the type of the value.
        /// </summary>
        public Type ValueType => typeof(TimeOnly);

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        protected MongoDbTimeOnlySerializer() : base()
        {

        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Deserializes a value.
        /// </summary>
        /// <param name="context">The deserialization context.</param>
        /// <param name="args">The deserialization args.</param>
        /// <returns></returns>
        public TimeOnly Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
        {
            context.Reader.ReadStartDocument();

            var hour = context.Reader.ReadInt32(nameof(TimeOnly.Hour));

            var minutes = context.Reader.ReadInt32(nameof(TimeOnly.Minute));

            var seconds = context.Reader.ReadInt32(nameof(TimeOnly.Second));

            context.Reader.ReadEndDocument();

            return new TimeOnly(hour, minutes, seconds);
        }

        /// <summary>
        /// Serializes a value.
        /// </summary>
        /// <param name="context">The serialization context.</param>
        /// <param name="args">The serialization args.</param>
        /// <param name="value">The value.</param>
        public void Serialize(BsonSerializationContext context, BsonSerializationArgs args, TimeOnly value)
        {
            context.Writer.WriteStartDocument();

            context.Writer.WriteInt32(nameof(TimeOnly.Hour), value.Hour);

            context.Writer.WriteInt32(nameof(TimeOnly.Minute), value.Minute);

            context.Writer.WriteInt32(nameof(TimeOnly.Second), value.Second);

            context.Writer.WriteEndDocument();
        }

        /// <summary>
        /// Deserializes a value.
        /// </summary>
        /// <param name="context">The deserialization context.</param>
        /// <param name="args">The deserialization args.</param>
        /// <returns></returns>
        object IBsonSerializer.Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
            => Deserialize(context, args);

        /// <summary>
        /// Serializes a value.
        /// </summary>
        /// <param name="context">The serialization context.</param>
        /// <param name="args">The serialization args.</param>
        /// <param name="value">The value.</param>
        public void Serialize(BsonSerializationContext context, BsonSerializationArgs args, object value)
            => Serialize(context, args, (TimeOnly)value);

        /// <summary>
        /// Tries to get the serialization info for a member.
        /// </summary>
        /// <param name="memberName">Name of the member.</param>
        /// <param name="serializationInfo">The serialization information.</param>
        /// <returns></returns>
        public bool TryGetMemberSerializationInfo(string memberName, out BsonSerializationInfo serializationInfo)
        {
            serializationInfo = new BsonSerializationInfo(memberName, BsonValueSerializer.Instance, typeof(BsonValue));

            return true;
        }

        #endregion
    }
}
