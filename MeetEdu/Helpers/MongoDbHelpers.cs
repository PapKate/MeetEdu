using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Bson.Serialization;
using MongoDB.Bson;

namespace MeetEdu
{
    /// <summary>
    /// Helper methods related to MongoDb
    /// </summary>
    public static class MongoDbHelpers
    {
        /// <summary>
        /// Sets the global configurations to the MongoDb
        /// </summary>
        public static void Configure()
        {
            //BsonSerializer.RegisterSerializer<PhoneNumber>(MongoDbPhoneNumberSerializer.Instance);
            BsonSerializer.RegisterSerializer<DayOfWeekTimeRange>(MongoDbDayOfWeekTimeRangeSerializer.Instance);
            BsonSerializer.RegisterSerializer<TimeOnly>(MongoDbTimeOnlySerializer.Instance);
            BsonSerializer.RegisterSerializer<DateOnly>(MongoDbDateOnlySerializer.Instance);
            BsonSerializer.RegisterSerializer(new DateTimeOffsetSerializer(BsonType.DateTime));
            BsonSerializer.RegisterSerializer(typeof(decimal), new DecimalSerializer(BsonType.Decimal128));
            BsonSerializer.RegisterSerializer(typeof(decimal?), new NullableSerializer<decimal>(new DecimalSerializer(BsonType.Decimal128)));
            BsonSerializer.RegisterSerializer<DateTime>(DateTimeSerializer.LocalInstance);

            var conventionPack = new ConventionPack
            {
                new IgnoreIfNullConvention(true),
                new IgnoreExtraElementsConvention(true)
            };

            ConventionRegistry.Register("AtomConventions", conventionPack, x => true);
        }
    }
}
