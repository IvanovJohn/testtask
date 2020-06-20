namespace PipelinesApp.Api.Tasks.Queries
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using MongoDB.Driver;

    using PipelinesApp.Api.Settings;
    using PipelinesApp.Api.Tasks.ViewModels;

    internal class TaskByIdsQuery : AbstractTaskQuery<TaskByIdsCriterion, IEnumerable<TaskViewModel>>
    {
        public TaskByIdsQuery(IMongoDbSettings mongoDbSettings)
            : base(mongoDbSettings)
        {
        }

        public override async Task<IEnumerable<TaskViewModel>> Ask(TaskByIdsCriterion query)
        {
            if (query.Ids == null || query.Ids.Length == 0)
            {
                return new List<TaskViewModel>();
            }

            var filterDef = new FilterDefinitionBuilder<TaskDbEntity>();
            var filter = filterDef.In(x => x.Id, query.Ids);

            var list = this.TasksCollection.Find(filter).ToList();
            return list.Select(p => p.ToViewModel());
        }
    }
}