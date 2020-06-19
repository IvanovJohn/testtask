namespace Pipelines.Api.Tasks.Commands
{
    using System.Threading.Tasks;

    using MongoDB.Driver;

    using Pipelines.Api.Settings;

    internal class DeleteTaskCommand : AbstractTaskCommand<DeleteTaskCommandContext>
    {
        public DeleteTaskCommand(IMongoDbSettings mongoDbSettings)
            : base(mongoDbSettings)
        {
        }

        public override async Task Execute(DeleteTaskCommandContext commandContext)
        {
            await this.TasksCollection.DeleteOneAsync(p => p.Id == commandContext.Id);
        }
    }
}