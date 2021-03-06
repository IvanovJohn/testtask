namespace PipelinesApp.Api.Users.Queries
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using MongoDB.Driver;

    using PipelinesApp.Api.Core.Queries;
    using PipelinesApp.Api.Settings;
    using PipelinesApp.Api.Users.ViewModels;

    internal class UsersQuery : IQuery<UsersCriterion, IEnumerable<UserViewModel>>
    {
        private readonly IMongoDbSettings mongoDbSettings;

        public UsersQuery(IMongoDbSettings mongoDbSettings)
        {
            this.mongoDbSettings = mongoDbSettings;
        }

        public async Task<IEnumerable<UserViewModel>> Ask(UsersCriterion criterion)
        {
            var client = new MongoClient(this.mongoDbSettings.ConnectionString);
            var database = client.GetDatabase(this.mongoDbSettings.DatabaseName);
            var collection = database.GetCollection<UserDbEntity>(this.mongoDbSettings.UsersCollectionName);

            var result = await collection.FindAsync(t => true);
            return result.ToList().Select(
                p =>
                    new UserViewModel
                    {
                            Id = p.Id,
                            Name = p.Name,
                    });
        }
    }
}