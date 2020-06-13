namespace Pipelines.Api.Tasks
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
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the ​AverageTime ​it takes to run.
        /// </summary>
        public long AverageTime { get; set; }
    }
}