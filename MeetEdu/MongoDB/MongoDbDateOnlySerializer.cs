using MongoDB.Bson.IO;
using MongoDB.Bson.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Serializers;

namespace MeetEdu
{
    /// <summary>
    /// An implementation of the <see cref="IBsonSerializer"/> for the <see cref="DateOnly"/>
    /// </summary>
    public class MongoDbDateOnlySerializer : IBsonSerializer<DateOnly>, IBsonDocumentSerializer
    {
        #region Singleton

        /// <summary>
        /// The instance
        /// </summary>
        public static MongoDbDateOnlySerializer Instance { get; } = new MongoDbDateOnlySerializer();

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the type of the value.
        /// </summary>
        public Type ValueType => typeof(DateOnly);

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        protected MongoDbDateOnlySerializer() : base()
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
        public DateOnly Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
        {
            context.Reader.ReadStartDocument();

            var year = context.Reader.ReadInt32(nameof(DateOnly.Year));

            var month = context.Reader.ReadInt32(nameof(DateOnly.Month));

            var second = context.Reader.ReadInt32(nameof(DateOnly.Day));

            context.Reader.ReadEndDocument();

            return new DateOnly(year, month, second);
        }

        /// <summary>
        /// Serializes a value.
        /// </summary>
        /// <param name="context">The serialization context.</param>
        /// <param name="args">The serialization args.</param>
        /// <param name="value">The value.</param>
        public void Serialize(BsonSerializationContext context, BsonSerializationArgs args, DateOnly value)
        {
            context.Writer.WriteStartDocument();

            context.Writer.WriteInt32(nameof(DateOnly.Year), value.Year);

            context.Writer.WriteInt32(nameof(DateOnly.Month), value.Month);

            context.Writer.WriteInt32(nameof(DateOnly.Day), value.Day);

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
            => Serialize(context, args, (DateOnly)value);

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
