namespace PipelinesApp.Api.Pipelines.Queries
{
    using System.Threading.Tasks;

    using MongoDB.Driver;

    using PipelinesApp.Api.ExceptionHandling;
    using PipelinesApp.Api.Pipelines.ViewModels;
    using PipelinesApp.Api.Settings;

    internal class PipelineByIdQuery : AbstractPipelineQuery<PipelineByIdCriterion, PipelineViewModel>
    {
        public PipelineByIdQuery(IMongoDbSettings mongoDbSettings)
            : base(mongoDbSettings)
        {
        }

        public override async Task<PipelineViewModel> Ask(PipelineByIdCriterion query)
        {
            var pipeline = (await this.PipelinesCollection.FindAsync(t => t.Id == query.Id)).FirstOrDefault();
            if (pipeline == null)
            {
                throw ApiException.ErrorItemNotFound("Pipeline not found");
            }

            return pipeline.ToViewModel();
        }
    }
}