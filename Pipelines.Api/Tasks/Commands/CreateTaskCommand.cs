namespace Pipelines.Api.Tasks.Commands
{
    using System;
    using System.Threading.Tasks;

    using MongoDB.Driver;

    using Pipelines.Api.Core.Commands;
    using Pipelines.Api.Settings;
    using Pipelines.Api.Users;

    internal class CreateTaskCommand : ICommand<CreateTaskCommandContext>
    {
        private readonly IMongoDbSettings mongoDbSettings;

        public CreateTaskCommand(IMongoDbSettings mongoDbSettings)
        {
            this.mongoDbSettings = mongoDbSettings;
        }

        public async Task Execute(CreateTaskCommandContext commandContext)
        {
            if (string.IsNullOrEmpty(commandContext.CurrentUserId))
            {
                throw new ArgumentException("userId");
            }

            var client = new MongoClient(this.mongoDbSettings.ConnectionString);
            var database = client.GetDatabase(this.mongoDbSettings.DatabaseName);
            var tasksCollection = database.GetCollection<TaskDbEntity>(this.mongoDbSettings.TasksCollectionName);

            await tasksCollection.InsertOneAsync(
                new TaskDbEntity
                    {
                        Name = commandContext.Form.Name,
                        CreatorUserId = commandContext.CurrentUserId ,
                    });
        }
    }
}