namespace PipelinesApp.Api.Pipelines
{
    using MongoDB.Bson;
    using MongoDB.Bson.Serialization.Attributes;

    internal class PipelineDbEntity
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
        /// Gets or sets user, who creates this task.
        /// </summary>
        [BsonElement("creatorUserId")]
        public string CreatorUserId { get; set; }

        /// <summary>
        /// Gets or sets array of Tasks.
        /// </summary>
        public TaskGraphNode[] Tasks { get; set; }
    }
}