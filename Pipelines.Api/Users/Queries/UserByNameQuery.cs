namespace Pipelines.Api.Users.Queries
{
    using System.Threading.Tasks;

    using MongoDB.Driver;

    using Pipelines.Api.Core.Queries;
    using Pipelines.Api.Settings;

    internal class UserByNameQuery : IQuery<UserByNameCriterion, UserDbEntity>
    {
        private readonly IMongoDbSettings mongoDbSettings;

        public UserByNameQuery(IMongoDbSettings mongoDbSettings)
        {
            this.mongoDbSettings = mongoDbSettings;
        }

        public async Task<UserDbEntity> Ask(UserByNameCriterion criterion)
        {
            var client = new MongoClient(this.mongoDbSettings.ConnectionString);
            var database = client.GetDatabase(this.mongoDbSettings.DatabaseName);
            var collection = database.GetCollection<UserDbEntity>(this.mongoDbSettings.UsersCollectionName);

            var result = await collection.FindAsync(t => t.Name == criterion.Name);
            return result.SingleOrDefault();
        }
    }
}