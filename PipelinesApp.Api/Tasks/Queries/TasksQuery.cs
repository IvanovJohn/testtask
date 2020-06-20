namespace PipelinesApp.Api.Tasks.Queries
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using MongoDB.Driver;

    using PipelinesApp.Api.Settings;
    using PipelinesApp.Api.Tasks.ViewModels;

    internal class TasksQuery : AbstractTaskQuery<TasksCriterion, IEnumerable<TaskViewModel>>
    {
        public TasksQuery(IMongoDbSettings mongoDbSettings)
            : base(mongoDbSettings)
        {
        }

        public override async Task<IEnumerable<TaskViewModel>> Ask(TasksCriterion criterion)
        {
            // TODO: add pagination
            // TODO: refactor using ExpressionHelper
            IAsyncCursor<TaskDbEntity> result;
            if (string.IsNullOrEmpty(criterion.SearchString))
            {
                result = await this.TasksCollection.FindAsync(p => true);
            }
            else
            {
                result = await this.TasksCollection.FindAsync(p => p.Name.Contains(criterion.SearchString));
            }

            return result.ToList().Select(p => p.ToViewModel());
        }
    }
}