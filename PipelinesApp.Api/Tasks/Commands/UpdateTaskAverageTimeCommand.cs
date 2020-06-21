namespace PipelinesApp.Api.Tasks.Commands
{
    using System.Threading.Tasks;

    using MongoDB.Driver;

    using PipelinesApp.Api.Settings;

    internal class UpdateTaskAverageTimeCommand : AbstractTaskCommand<UpdateTaskAverageTimeCommandContext>
    {
        public UpdateTaskAverageTimeCommand(IMongoDbSettings mongoDbSettings)
            : base(mongoDbSettings)
        {
        }

        public override async Task Execute(UpdateTaskAverageTimeCommandContext commandContext)
        {
            // TODO: add transaction
            var task = (await this.TasksCollection.FindAsync(t => t.Id == commandContext.Id)).FirstOrDefault();
            if (task == null)
            {
                return;
            }

            if (task.RunCount == null)
            {
                task.RunCount = 0;
            }

            if (task.AverageTime == null)
            {
                task.AverageTime = 0;
            }

            task.RunCount++;
            task.AverageTime = ((task.AverageTime * (task.RunCount - 1)) + commandContext.LastDuration) / task.RunCount;

            var filter = new ExpressionFilterDefinition<TaskDbEntity>(d => d.Id == task.Id);
            var updateRowCount = Builders<TaskDbEntity>.Update.Set(p => p.RunCount, task.RunCount);
            var updateAverageTime = Builders<TaskDbEntity>.Update.Set(p => p.AverageTime, task.AverageTime);
            var update = Builders<TaskDbEntity>.Update.Combine(updateAverageTime, updateRowCount);
            await this.TasksCollection.UpdateOneAsync(filter, update);
        }
    }
}