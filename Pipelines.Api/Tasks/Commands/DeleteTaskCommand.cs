namespace Pipelines.Api.Tasks.Commands
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Authorization.Infrastructure;

    using MongoDB.Driver;

    using Pipelines.Api.Core.Queries;
    using Pipelines.Api.ExceptionHandling;
    using Pipelines.Api.Settings;
    using Pipelines.Api.Tasks.Queries;
    using Pipelines.Api.Tasks.ViewModels;

    internal class DeleteTaskCommand : AbstractTaskCommand<DeleteTaskCommandContext>
    {
        private readonly IAuthorizationService authorizationService;

        private readonly IQueryDispatcher queryDispatcher;

        public DeleteTaskCommand(IMongoDbSettings mongoDbSettings, IAuthorizationService authorizationService, IQueryDispatcher queryDispatcher)
            : base(mongoDbSettings)
        {
            this.authorizationService = authorizationService;
            this.queryDispatcher = queryDispatcher;
        }

        public override async Task Execute(DeleteTaskCommandContext commandContext)
        {
            var task = await this.queryDispatcher.Ask<TaskByIdCriterion, TaskViewModel>(new TaskByIdCriterion { Id = commandContext.Id });
            var authResult = await this.authorizationService.AuthorizeAsync(commandContext.CurrentUser, task, new NameAuthorizationRequirement(TaskActions.Delete));
            if (!authResult.Succeeded)
            {
                throw ApiException.ErrorAuthorization("You cannot delete this task");
            }

            await this.TasksCollection.DeleteOneAsync(p => p.Id == commandContext.Id);
        }
    }
}