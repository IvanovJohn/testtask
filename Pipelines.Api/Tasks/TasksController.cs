namespace Pipelines.Api.Tasks
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;

    using Pipelines.Api.Core;

    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly IQueryDispatcher queryDispatcher;

        /// <summary>
        /// Initializes a new instance of the <see cref="TasksController"/> class.
        /// </summary>
        /// <param name="queryDispatcher"><see cref="IQueryDispatcher"/>.</param>
        public TasksController(IQueryDispatcher queryDispatcher)
        {
            this.queryDispatcher = queryDispatcher;
        }

        public async Task<ActionResult<IEnumerable<TaskViewModel>>> GetTasks()
        {
            var tasks = await this.queryDispatcher.Ask<TasksQuery, IEnumerable<TaskViewModel>>(new TasksQuery());
            return tasks.ToList();
        }
    }
}