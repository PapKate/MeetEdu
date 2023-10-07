using MongoDB.Bson.IO;
using MongoDB.Bson.Serialization;
using MongoDB.Bson;

namespace MeetEdu
{
    /// <summary>
    /// An implementation of the <see cref="IBsonSerializer"/> for the <see cref="DayOfWeekTimeRange"/>
    /// </summary>
    public class MongoDbDayOfWeekTimeRangeSerializer : IBsonSerializer<DayOfWeekTimeRange>
    {
        #region Singleton

        /// <summary>
        /// The instance
        /// </summary>
        public static MongoDbDayOfWeekTimeRangeSerializer Instance { get; } = new MongoDbDayOfWeekTimeRangeSerializer();

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the type of the value.
        /// </summary>
        public Type ValueType => typeof(DayOfWeekTimeRange);

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        protected MongoDbDayOfWeekTimeRangeSerializer() : base()
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
        public DayOfWeekTimeRange Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
        {
            var dayOfWeek = default(DayOfWeek);
            var start = default(TimeOnly);
            var end = default(TimeOnly);
            string? text = null;

            context.Reader.ReadStartDocument();

            while (context.Reader.State != BsonReaderState.Type || context.Reader.ReadBsonType() != BsonType.EndOfDocument)
            {
                if (context.Reader.State != BsonReaderState.Name)
                    continue;

                var name = context.Reader.ReadName();

                if (name == nameof(DayOfWeekTimeRange.DayOfWeek))
                {
                    dayOfWeek = (DayOfWeek)context.Reader.ReadInt32();
                    continue;
                }

                if (name == nameof(DayOfWeekTimeRange.Start))
                {
                    start = MongoDbTimeOnlySerializer.Instance.Deserialize(context, args);
                    continue;
                }

                if (name == nameof(DayOfWeekTimeRange.End))
                {
                    end = MongoDbTimeOnlySerializer.Instance.Deserialize(context, args);
                    continue;
                }

                if (name == nameof(DayOfWeekTimeRange.Text))
                {
                    text = context.Reader.ReadString();
                    continue;
                }

                if (context.Reader.State == BsonReaderState.Name)
                    context.Reader.SkipName();

                if (context.Reader.State == BsonReaderState.Value)
                    context.Reader.SkipValue();
            }

            context.Reader.ReadEndDocument();

            return new DayOfWeekTimeRange(text, dayOfWeek, start, end);
        }

        /// <summary>
        /// Serializes a value.
        /// </summary>
        /// <param name="context">The serialization context.</param>
        /// <param name="args">The serialization args.</param>
        /// <param name="value">The value.</param>
        public void Serialize(BsonSerializationContext context, BsonSerializationArgs args, DayOfWeekTimeRange value)
        {
            context.Writer.WriteStartDocument();

            context.Writer.WriteName(nameof(DayOfWeekTimeRange.DayOfWeek));
            context.Writer.WriteInt32((int)value.DayOfWeek);

            context.Writer.WriteName(nameof(DayOfWeekTimeRange.Start));
            MongoDbTimeOnlySerializer.Instance.Serialize(context, args, value.Start);

            context.Writer.WriteName(nameof(DayOfWeekTimeRange.End));
            MongoDbTimeOnlySerializer.Instance.Serialize(context, args, value.End);

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
            => Serialize(context, args, (DayOfWeekTimeRange)value);

        #endregion
    }
}
