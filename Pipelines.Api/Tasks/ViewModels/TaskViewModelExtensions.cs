namespace Pipelines.Api.Tasks.ViewModels
{
    internal static class TaskViewModelExtensions
    {
        public static TaskViewModel ToViewModel(this TaskDbEntity task)
        {
            return new TaskViewModel
                       {
                           Id = task.Id,
                           Name = task.Name,
                           AverageTime = task.AverageTime,
                           CreatorUserId = task.CreatorUserId,
                       };
        }
    }
}