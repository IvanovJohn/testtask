namespace PipelinesApp.Api.Tasks
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;

    using PipelinesApp.Api.Core.Commands;
    using PipelinesApp.Api.Core.Queries;
    using PipelinesApp.Api.Tasks.Commands;
    using PipelinesApp.Api.Tasks.Forms;
    using PipelinesApp.Api.Tasks.Queries;
    using PipelinesApp.Api.Tasks.ViewModels;

    [Route("api/Tasks")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly IQueryDispatcher queryDispatcher;
        private readonly ICommandDispatcher commandDispatcher;

        /// <summary>
        /// Initializes a new instance of the <see cref="TasksController"/> class.
        /// </summary>
        /// <param name="queryDispatcher">
        /// <see cref="IQueryDispatcher"/>.
        /// </param>
        /// <param name="commandDispatcher">
        /// <see cref="ICommandDispatcher"/>.
        /// </param>
        public TasksController(IQueryDispatcher queryDispatcher, ICommandDispatcher commandDispatcher)
        {
            this.queryDispatcher = queryDispatcher;
            this.commandDispatcher = commandDispatcher;
        }

        [HttpGet]
        [Route("")]
        public async Task<ActionResult<IEnumerable<TaskViewModel>>> GetTasks(string s)
        {
            var tasks = await this.queryDispatcher.Ask<TasksCriterion, IEnumerable<TaskViewModel>>(new TasksCriterion { SearchString = s });
            return tasks.ToList();
        }

        [HttpPost]
        [Route("")]
        public async Task Create(CreateTaskForm form)
        {
            await this.commandDispatcher.Execute(new CreateTaskCommandContext
                                                  {
                                                      Form = form,
                                                      CurrentUser = this.User,
                                                  });
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task Delete(string id)
        {
            await this.commandDispatcher.Execute(new DeleteTaskCommandContext
                                                     {
                                                         Id = id,
                                                         CurrentUser = this.User,
                                                     });
        }
    }
}