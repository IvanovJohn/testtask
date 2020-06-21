namespace PipelinesApp.Api.Pipelines.Commands
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Authorization.Infrastructure;

    using MongoDB.Driver;

    using PipelinesApp.Api.Core.Queries;
    using PipelinesApp.Api.ExceptionHandling;
    using PipelinesApp.Api.Pipelines.Queries;
    using PipelinesApp.Api.Pipelines.ViewModels;
    using PipelinesApp.Api.Settings;

    internal class DeletePipelineCommand : AbstractPipelineCommand<DeletePipelineCommandContext>
    {
        private readonly IAuthorizationService authorizationService;

        private readonly IQueryDispatcher queryDispatcher;

        public DeletePipelineCommand(IMongoDbSettings mongoDbSettings, IAuthorizationService authorizationService, IQueryDispatcher queryDispatcher)
            : base(mongoDbSettings)
        {
            this.authorizationService = authorizationService;
            this.queryDispatcher = queryDispatcher;
        }

        public override async Task Execute(DeletePipelineCommandContext commandContext)
        {
            var pipeline = await this.queryDispatcher.Ask<PipelineByIdCriterion, PipelineDetailsViewModel>(new PipelineByIdCriterion { Id = commandContext.Id });
            var authResult = await this.authorizationService.AuthorizeAsync(commandContext.CurrentUser, pipeline, new NameAuthorizationRequirement(PipelineActions.Delete));
            if (!authResult.Succeeded)
            {
                throw ApiException.ErrorAuthorization("You cannot delete this pipeline");
            }

            await this.PipelinesCollection.DeleteOneAsync(p => p.Id == commandContext.Id);
        }
    }
}