namespace Pipelines.Api.Tasks
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Pipelines.Api.Core;

    internal class TasksQueryHandler : IQueryHandler<TasksQuery, IEnumerable<TaskViewModel>>
    {
        public Task<IEnumerable<TaskViewModel>> Ask(TasksQuery query)
        {
            throw new System.NotImplementedException();
        }
    }
}