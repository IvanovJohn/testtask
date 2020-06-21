namespace PipelinesApp.Api.Pipelines
{
    public class TaskGraphNode
    {
        public string TaskId { get; set; }

        public string PreviosTaskId { get; set; }
    }
}