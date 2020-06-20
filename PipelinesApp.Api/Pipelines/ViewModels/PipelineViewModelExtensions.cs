namespace PipelinesApp.Api.Pipelines.ViewModels
{
    internal static class PipelineViewModelExtensions
    {
        public static PipelineViewModel ToViewModel(this PipelineDbEntity entity)
        {
            return new PipelineViewModel
            {
                           Id = entity.Id,
                           Name = entity.Name,
                           CreatorUserId = entity.CreatorUserId,
            };
        }
    }
}