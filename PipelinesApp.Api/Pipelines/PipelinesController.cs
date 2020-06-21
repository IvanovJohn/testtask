namespace PipelinesApp.Api.Pipelines
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;

    using PipelinesApp.Api.Core.Commands;
    using PipelinesApp.Api.Core.Queries;
    using PipelinesApp.Api.Pipelines.Commands;
    using PipelinesApp.Api.Pipelines.Forms;
    using PipelinesApp.Api.Pipelines.Queries;
    using PipelinesApp.Api.Pipelines.ViewModels;
    using PipelinesApp.Api.Runner;

    [Route("api/pipelines")]
    [ApiController]
    public class PipelinesController : ControllerBase
    {
        private readonly IQueryDispatcher queryDispatcher;
        private readonly ICommandDispatcher commandDispatcher;
        private readonly IPipelineRunner pipelineRunner;

        /// <summary>
        /// Initializes a new instance of the <see cref="PipelinesController"/> class.
        /// </summary>
        /// <param name="queryDispatcher">
        /// <see cref="IQueryDispatcher"/>.
        /// </param>
        /// <param name="commandDispatcher">
        /// <see cref="ICommandDispatcher"/>.
        /// </param>
        /// <param name="pipelineRunner">
        /// The pipeline Runner.
        /// </param>
        public PipelinesController(IQueryDispatcher queryDispatcher, ICommandDispatcher commandDispatcher, IPipelineRunner pipelineRunner)
        {
            this.queryDispatcher = queryDispatcher;
            this.commandDispatcher = commandDispatcher;
            this.pipelineRunner = pipelineRunner;
        }

        [HttpGet]
        [Route("")]
        public async Task<ActionResult<IEnumerable<PipelineViewModel>>> GetPipelines()
        {
            var pipelines = await this.queryDispatcher.Ask<PipelinesCriterion, IEnumerable<PipelineViewModel>>(new PipelinesCriterion());
            return pipelines.ToList();
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<PipelineDetailsViewModel>> GetById(string id)
        {
            return
                await this.queryDispatcher.Ask<PipelineByIdCriterion, PipelineDetailsViewModel>(
                    new PipelineByIdCriterion { Id = id });
        }

        [HttpPost]
        [Route("")]
        public async Task Create(CreatePipelineForm form)
        {
            await this.commandDispatcher.Execute(new CreatePipelineCommandContext
                                                  {
                                                      Form = form,
                                                      CurrentUser = this.User,
                                                  });
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task Delete(string id)
        {
            await this.commandDispatcher.Execute(new DeletePipelineCommandContext
                                                     {
                                                         Id = id,
                                                         CurrentUser = this.User,
                                                     });
        }

        [HttpPost]
        [Route("{id}/run")]
        public async Task RunPipeline(string id)
        {
            var pipeline = await this.queryDispatcher.Ask<PipelineByIdCriterion, PipelineDetailsViewModel>(
                               new PipelineByIdCriterion { Id = id });

            await this.pipelineRunner.RunInBackground(pipeline);
        }
    }
}