namespace PipelinesApp.Api.Pipelines
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;

    using PipelinesApp.Api.Auth;
    using PipelinesApp.Api.Core.Commands;
    using PipelinesApp.Api.Core.Queries;
    using PipelinesApp.Api.Tasks.Commands;
    using PipelinesApp.Api.Tasks.Forms;
    using PipelinesApp.Api.Tasks.Queries;
    using PipelinesApp.Api.Tasks.ViewModels;

    [Route("api/pipelines")]
    [ApiController]
    public class PipelinesController : ControllerBase
    {
        private readonly IQueryDispatcher queryDispatcher;
        private readonly ICommandDispatcher commandDispatcher;

        /// <summary>
        /// Initializes a new instance of the <see cref="PipelinesController"/> class.
        /// </summary>
        /// <param name="queryDispatcher">
        /// <see cref="IQueryDispatcher"/>.
        /// </param>
        /// <param name="commandDispatcher">
        /// <see cref="ICommandDispatcher"/>.
        /// </param>
        public PipelinesController(IQueryDispatcher queryDispatcher, ICommandDispatcher commandDispatcher)
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