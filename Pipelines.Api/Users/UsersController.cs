namespace Pipelines.Api.Users
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;

    using Pipelines.Api.Core.Queries;
    using Pipelines.Api.Users.Queries;
    using Pipelines.Api.Users.ViewModels;

    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IQueryDispatcher queryDispatcher;

        public UsersController(IQueryDispatcher queryDispatcher)
        {
            this.queryDispatcher = queryDispatcher;
        }

        [HttpGet]
        [Route("")]
        public async Task<ActionResult<IEnumerable<UserViewModel>>> GetUsers()
        {
            var items = await this.queryDispatcher.Ask<UsersCriterion, IEnumerable<UserViewModel>>(new UsersCriterion());
            return items.ToList();
        }
    }
}