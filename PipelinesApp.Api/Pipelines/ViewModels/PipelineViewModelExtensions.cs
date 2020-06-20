namespace PipelinesApp.Api.Pipelines.ViewModels
{
    internal static class PipelineViewModelExtensions
    {
        public static void Fill(this PipelineViewModel viewModel, PipelineDbEntity entity)
        {
            viewModel.Id = entity.Id;
            viewModel.Name = entity.Name;
            viewModel.CreatorUserId = entity.CreatorUserId;
        }

        public static void Fill(this PipelineDetailsViewModel viewModel, PipelineDbEntity entity)
        {
            ((PipelineViewModel)viewModel).Fill(entity);
        }
    }
}