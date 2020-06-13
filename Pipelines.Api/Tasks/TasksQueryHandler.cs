namespace Pipelines.Api.Tasks
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using MongoDB.Driver;

    using Pipelines.Api.Core;
    using Pipelines.Api.Settings;

    internal class TasksQueryHandler : IQueryHandler<TasksQuery, IEnumerable<TaskViewModel>>
    {
        private readonly IMongoDbSettings mongoDbSettings;

        public TasksQueryHandler(IMongoDbSettings mongoDbSettings)
        {
            this.mongoDbSettings = mongoDbSettings;
        }

        public async Task<IEnumerable<TaskViewModel>> Ask(TasksQuery query)
        {
            var client = new MongoClient(this.mongoDbSettings.ConnectionString);
            var database = client.GetDatabase(this.mongoDbSettings.DatabaseName);
            var tasksCollection = database.GetCollection<TaskDbEntity>(this.mongoDbSettings.TasksCollectionName);

            // TODO: add pagination
            var result = await tasksCollection.FindAsync(t => true);
            return result.ToList().Select(
                p =>
                    new TaskViewModel
                        {
                            Id = p.Id,
                            Name = p.Name,
                            AverageTime = p.AverageTime,
                        });
        }
    }
}