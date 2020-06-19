namespace Pipelines.Api.Tasks.Commands
{
    using System.Threading.Tasks;

    using MongoDB.Driver;

    using Pipelines.Api.Core.Commands;
    using Pipelines.Api.Settings;

    internal abstract class AbstractTaskCommand<TCommandContext> : ICommand<TCommandContext>
        where TCommandContext : ICommandContext
    {
        private readonly IMongoDbSettings mongoDbSettings;

        public AbstractTaskCommand(IMongoDbSettings mongoDbSettings)
        {
            this.mongoDbSettings = mongoDbSettings;

            var client = new MongoClient(this.mongoDbSettings.ConnectionString);
            var database = client.GetDatabase(this.mongoDbSettings.DatabaseName);
            this.TasksCollection = database.GetCollection<TaskDbEntity>(this.mongoDbSettings.TasksCollectionName);
        }

        protected IMongoCollection<TaskDbEntity> TasksCollection { get;  }

        public abstract Task Execute(TCommandContext commandContext);
    }
}