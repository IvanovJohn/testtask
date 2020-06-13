namespace Pipelines.Api.Tasks
{
    /// <summary>
    /// ViewModel is used to view information about the task.
    /// </summary>
    public class TaskViewModel
    {
        /// <summary>
        /// Gets or sets the Name of the task.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the ​AverageTime ​it takes to run.
        /// </summary>
        public long AverageTime { get; set; }
    }
}