namespace PipelinesApp.Api.Pipelines.ViewModels
{
    /// <summary>
    /// ViewModel is used to view information about the pipeline.
    /// </summary>
    public class PipelineViewModel
    {
        /// <summary>
        /// Gets or sets the Id of the entity.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the Name of the pipeline.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets user, who creates this pipeline.
        /// </summary>
        public string CreatorUserId { get; set; }
    }
}