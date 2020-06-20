namespace PipelinesApp.Api.Pipelines.Queries
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using MongoDB.Driver;

    using PipelinesApp.Api.Pipelines.ViewModels;
    using PipelinesApp.Api.Settings;

    internal class PipelinesQuery : AbstractPipelineQuery<PipelinesCriterion, IEnumerable<PipelineViewModel>>
    {
        public PipelinesQuery(IMongoDbSettings mongoDbSettings)
            : base(mongoDbSettings)
        {
        }

        public override async Task<IEnumerable<PipelineViewModel>> Ask(PipelinesCriterion criterion)
        {
            // TODO: add pagination
            var result = await this.PipelinesCollection.FindAsync(t => true);
            return result.ToList().Select(p => p.ToViewModel());
        }
    }
}