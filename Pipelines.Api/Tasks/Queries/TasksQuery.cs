namespace Pipelines.Api.Tasks.Queries
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using MongoDB.Driver;

    using Pipelines.Api.Settings;
    using Pipelines.Api.Tasks.ViewModels;

    internal class TasksQuery : AbstractTaskQuery<TasksCriterion, IEnumerable<TaskViewModel>>
    {
        public TasksQuery(IMongoDbSettings mongoDbSettings)
            : base(mongoDbSettings)
        {
        }

        public override async Task<IEnumerable<TaskViewModel>> Ask(TasksCriterion criterion)
        {
            // TODO: add pagination
            var result = await this.TasksCollection.FindAsync(t => true);
            return result.ToList().Select(p => p.ToViewModel());
        }
    }
}