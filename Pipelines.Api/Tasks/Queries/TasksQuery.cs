namespace Pipelines.Api.Tasks.Queries
{
    using System.Collections.Generic;

    using Pipelines.Api.Core;
    using Pipelines.Api.Core.Queries;
    using Pipelines.Api.Tasks.ViewModels;

    public class TasksQuery : IQuery<IEnumerable<TaskViewModel>>
    {
    }
}