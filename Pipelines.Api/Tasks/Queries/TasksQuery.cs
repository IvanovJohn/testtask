namespace Pipelines.Api.Tasks.Queries
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using MongoDB.Driver;

    using Pipelines.Api.Core.Queries;
    using Pipelines.Api.Settings;
    using Pipelines.Api.Tasks.ViewModels;

    internal class TasksQuery : IQuery<TasksCriterion, IEnumerable<TaskViewModel>>
    {
        private readonly IMongoDbSettings mongoDbSettings;

        public TasksQuery(IMongoDbSettings mongoDbSettings)
        {
            this.mongoDbSettings = mongoDbSettings;
        }

        public async Task<IEnumerable<TaskViewModel>> Ask(TasksCriterion criterion)
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