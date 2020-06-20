namespace PipelinesApp.Api.Pipelines.Queries
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using MongoDB.Driver;

    using PipelinesApp.Api.Core.Queries;
    using PipelinesApp.Api.ExceptionHandling;
    using PipelinesApp.Api.Pipelines.ViewModels;
    using PipelinesApp.Api.Settings;
    using PipelinesApp.Api.Tasks.Queries;
    using PipelinesApp.Api.Tasks.ViewModels;

    internal class PipelineByIdQuery : AbstractPipelineQuery<PipelineByIdCriterion, PipelineDetailsViewModel>
    {
        private readonly IQueryDispatcher queryDispatcher;

        public PipelineByIdQuery(IMongoDbSettings mongoDbSettings, IQueryDispatcher queryDispatcher)
            : base(mongoDbSettings)
        {
            this.queryDispatcher = queryDispatcher;
        }

        public override async Task<PipelineDetailsViewModel> Ask(PipelineByIdCriterion query)
        {
            var pipeline = (await this.PipelinesCollection.FindAsync(t => t.Id == query.Id)).FirstOrDefault();
            if (pipeline == null)
            {
                throw ApiException.ErrorItemNotFound("Pipeline not found");
            }

            var pipelineTasks =
                await this.queryDispatcher.Ask<TaskByIdsCriterion, IEnumerable<TaskViewModel>>(
                    new TaskByIdsCriterion { Ids = pipeline.TasksId });
            var result = new PipelineDetailsViewModel();
            result.Fill(pipeline);
            result.Tasks = pipelineTasks;

            return result;
        }
    }
}