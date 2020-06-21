namespace PipelinesApp.Api.Pipelines.Queries
{
    using System.Collections.Generic;
    using System.Linq;
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

            var childTasksIds = pipeline.Tasks.Select(p => p.TaskId).ToArray();
            var pipelineTasks =
                (await this.queryDispatcher.Ask<TaskByIdsCriterion, IEnumerable<TaskViewModel>>(
                    new TaskByIdsCriterion { Ids = childTasksIds })).ToList();
            var result = new PipelineDetailsViewModel();
            result.Fill(pipeline);

            var tasksViewModel = new List<TaskGraphNodeViewModel>();
            foreach (var taskGraphNode in pipeline.Tasks)
            {
                tasksViewModel.Add(
                    new TaskGraphNodeViewModel
                        {
                            PreviosTaskId = taskGraphNode.PreviosTaskId,
                            Task = pipelineTasks.First(p => p.Id == taskGraphNode.TaskId),
                        });
            }

            result.Tasks = tasksViewModel;
            return result;
        }
    }
}