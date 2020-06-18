namespace Pipelines.Api.Users
{
    using MongoDB.Bson;
    using MongoDB.Bson.Serialization.Attributes;

    public class UserDbEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("name")]
        public string Name { get; set; }
    }
}