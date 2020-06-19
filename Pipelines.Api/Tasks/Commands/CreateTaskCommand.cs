﻿namespace Pipelines.Api.Tasks.Commands
{
    using System.Threading.Tasks;

    using Pipelines.Api.Auth;
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
            if (commandContext.CurrentUser == null || !commandContext.CurrentUser.Identity.IsAuthenticated)
            {
                throw ApiException.ErrorAuthorization("You should login before create task");
            }

            await this.TasksCollection.InsertOneAsync(
                new TaskDbEntity
                    {
                        Name = commandContext.Form.Name,
                        CreatorUserId = commandContext.CurrentUser.GetUserIdFromClaims(),
                    });
        }
    }
}