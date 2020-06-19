namespace Pipelines.Api.Tasks.Queries
{
    using System.Threading.Tasks;

    using MongoDB.Driver;

    using Pipelines.Api.ExceptionHandling;
    using Pipelines.Api.Settings;
    using Pipelines.Api.Tasks.ViewModels;

    internal class TaskByIdQuery : AbstractTaskQuery<TaskByIdCriterion, TaskViewModel>
    {
        public TaskByIdQuery(IMongoDbSettings mongoDbSettings)
            : base(mongoDbSettings)
        {
        }

        public override async Task<TaskViewModel> Ask(TaskByIdCriterion query)
        {
            var task = (await this.TasksCollection.FindAsync(t => t.Id == query.Id)).FirstOrDefault();
            if (task == null)
            {
                throw ApiException.ErrorItemNotFound("Task not found");
            }

            return task.ToViewModel();
        }
    }
}