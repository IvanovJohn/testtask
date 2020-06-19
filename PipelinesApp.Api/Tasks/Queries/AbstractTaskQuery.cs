namespace PipelinesApp.Api.Tasks.Queries
{
    using System.Threading.Tasks;

    using MongoDB.Driver;

    using PipelinesApp.Api.Core.Queries;
    using PipelinesApp.Api.Settings;

    internal abstract class AbstractTaskQuery<TCriterion, TResult> : IQuery<TCriterion, TResult> 
        where TCriterion : ICriterion
    {
        public AbstractTaskQuery(IMongoDbSettings mongoDbSettings)
        {
            var client = new MongoClient(mongoDbSettings.ConnectionString);
            var database = client.GetDatabase(mongoDbSettings.DatabaseName);
            this.TasksCollection = database.GetCollection<TaskDbEntity>(mongoDbSettings.TasksCollectionName);
        }

        protected IMongoCollection<TaskDbEntity> TasksCollection { get; }

        public abstract Task<TResult> Ask(TCriterion query);
    }
}