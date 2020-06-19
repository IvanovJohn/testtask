namespace Pipelines.Api.Tasks
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;

    using Pipelines.Api.Auth;
    using Pipelines.Api.Core.Commands;
    using Pipelines.Api.Core.Queries;
    using Pipelines.Api.Tasks.Commands;
    using Pipelines.Api.Tasks.Forms;
    using Pipelines.Api.Tasks.Queries;
    using Pipelines.Api.Tasks.ViewModels;

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

        protected string CurrentUserId
        {
            get
            {
                return this.User.GetUserIdFromClaims();
            }
        }

        [HttpGet]
        [Route("")]
        public async Task<ActionResult<IEnumerable<TaskViewModel>>> GetTasks()
        {
            var tasks = await this.queryDispatcher.Ask<TasksCriterion, IEnumerable<TaskViewModel>>(new TasksCriterion());
            return tasks.ToList();
        }

        [HttpPost]
        [Route("")]
        public async Task Create(CreateTaskForm form)
        {
            await this.commandDispatcher.Execute(new CreateTaskCommandContext
                                                  {
                                                      Form = form,
                                                      CurrentUserId = this.CurrentUserId,
                                                  });
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task Delete(string id)
        {
            await this.commandDispatcher.Execute(new DeleteTaskCommandContext
                                                     {
                                                         Id = id,
                                                         CurrentUserId = this.CurrentUserId,
                                                     });
        }
    }
}