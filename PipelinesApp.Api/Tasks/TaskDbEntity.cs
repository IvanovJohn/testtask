namespace PipelinesApp.Api.Tasks
{
    using MongoDB.Bson;
    using MongoDB.Bson.Serialization.Attributes;

    internal class TaskDbEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the Name of the task.
        /// </summary>
        [BsonElement("name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the ​AverageTime ​it takes to run.
        /// </summary>
        [BsonElement("averageTime")]
        public long? AverageTime { get; set; }

        /// <summary>
        /// Gets or sets user, who creates this task.
        /// </summary>
        [BsonElement("creatorUserId")]
        public string CreatorUserId { get; set; }
    }
}