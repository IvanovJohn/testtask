namespace Pipelines.Api.Tasks.ViewModels
{
    /// <summary>
    /// ViewModel is used to view information about the task.
    /// </summary>
    public class TaskViewModel
    {
        /// <summary>
        /// Gets or sets the Id of the entity.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the Name of the task.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the ​AverageTime ​it takes to run.
        /// </summary>
        public long? AverageTime { get; set; }
    }
}