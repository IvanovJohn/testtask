namespace PipelinesApp.Api.Tasks.Commands
{
    using System.Threading.Tasks;

    using MongoDB.Driver;

    using PipelinesApp.Api.Core.Commands;
    using PipelinesApp.Api.Settings;

    internal abstract class AbstractTaskCommand<TCommandContext> : ICommand<TCommandContext>
        where TCommandContext : ICommandContext
    {
        public AbstractTaskCommand(IMongoDbSettings mongoDbSettings)
        {
            var client = new MongoClient(mongoDbSettings.ConnectionString);
            var database = client.GetDatabase(mongoDbSettings.DatabaseName);
            this.TasksCollection = database.GetCollection<TaskDbEntity>(mongoDbSettings.TasksCollectionName);
        }

        protected IMongoCollection<TaskDbEntity> TasksCollection { get;  }

        public abstract Task Execute(TCommandContext commandContext);
    }
}