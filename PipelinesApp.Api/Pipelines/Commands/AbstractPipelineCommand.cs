namespace PipelinesApp.Api.Pipelines.Commands
{
    using System.Threading.Tasks;

    using MongoDB.Driver;

    using PipelinesApp.Api.Core.Commands;
    using PipelinesApp.Api.Settings;

    internal abstract class AbstractPipelineCommand<TCommandContext> : ICommand<TCommandContext>
        where TCommandContext : ICommandContext
    {
        public AbstractPipelineCommand(IMongoDbSettings mongoDbSettings)
        {
            var client = new MongoClient(mongoDbSettings.ConnectionString);
            var database = client.GetDatabase(mongoDbSettings.DatabaseName);
            this.PipelinesCollection = database.GetCollection<PipelineDbEntity>(mongoDbSettings.PipelinesCollectionName);
        }

        protected IMongoCollection<PipelineDbEntity> PipelinesCollection { get;  }

        public abstract Task Execute(TCommandContext commandContext);
    }
}