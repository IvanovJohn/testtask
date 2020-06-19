namespace Pipelines.Api.Tasks.Commands
{
    using System.Threading.Tasks;

    using Pipelines.Api.ExceptionHandling;
    using Pipelines.Api.Settings;

    internal class CreateTaskCommand : AbstractTaskCommand<CreateTaskCommandContext>
    {
        public CreateTaskCommand(IMongoDbSettings mongoDbSettings)
            : base(mongoDbSettings)
        {
        }

        public override async Task Execute(CreateTaskCommandContext commandContext)
        {
            // TODO: move to authorizationhandler
            if (string.IsNullOrEmpty(commandContext.CurrentUserId))
            {
                throw ApiException.ErrorAuthentication("You should login before create a task");
            }

            await this.TasksCollection.InsertOneAsync(
                new TaskDbEntity
                    {
                        Name = commandContext.Form.Name,
                        CreatorUserId = commandContext.CurrentUserId,
                    });
        }
    }
}