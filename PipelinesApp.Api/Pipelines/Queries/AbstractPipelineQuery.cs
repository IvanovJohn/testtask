namespace PipelinesApp.Api.Pipelines.Queries
{
    using System.Threading.Tasks;

    using MongoDB.Driver;

    using PipelinesApp.Api.Core.Queries;
    using PipelinesApp.Api.Settings;

    internal abstract class AbstractPipelineQuery<TCriterion, TResult> : IQuery<TCriterion, TResult>
        where TCriterion : ICriterion
    {
        public AbstractPipelineQuery(IMongoDbSettings mongoDbSettings)
        {
            var client = new MongoClient(mongoDbSettings.ConnectionString);
            var database = client.GetDatabase(mongoDbSettings.DatabaseName);
            this.PipelinesCollection = database.GetCollection<PipelineDbEntity>(mongoDbSettings.PipelinesCollectionName);
        }

        protected IMongoCollection<PipelineDbEntity> PipelinesCollection { get; }

        public abstract Task<TResult> Ask(TCriterion query);
    }
}