namespace PipelinesApp.Api.Runner
{
    using System.IO;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Logging;

    using PipelinesApp.Api.Helpers;
    using PipelinesApp.Api.Pipelines.ViewModels;

    public interface IPipelineRunner
    {
        public Task RunInBackground(PipelineDetailsViewModel pipeline);
    }

    internal class PipelineRunner : IPipelineRunner
    {
        private readonly IBackgroundTaskQueue backgroundTaskQueue;
        private readonly CancellationToken cancellationToken;
        private readonly ILogger<PipelineRunner> logger;

        public PipelineRunner(IBackgroundTaskQueue backgroundTaskQueue, IHostApplicationLifetime applicationLifetime, ILogger<PipelineRunner> logger)
        {
            this.backgroundTaskQueue = backgroundTaskQueue;
            this.logger = logger;
            this.cancellationToken = applicationLifetime.ApplicationStopping;
        }

        public async Task RunInBackground(PipelineDetailsViewModel pipeline)
        {
            this.backgroundTaskQueue.QueueBackgroundWorkItem(async token =>
                {
                    await this.Run(pipeline, this.cancellationToken);
                });
        }

        private async Task Run(PipelineDetailsViewModel pipeline, CancellationToken token)
        {
            this.logger.LogInformation($"Pipeline {pipeline.Name} started");
            if (pipeline.Tasks != null && pipeline.Tasks.Any())
            {
                string previosTaskId = null;
                do
                {
                    var task = pipeline.Tasks.FirstOrDefault(p => p.PreviosTaskId == previosTaskId);
                    if (task == null)
                    {
                        break;
                    }

                    await this.RunTask(task, token);
                    previosTaskId = task.Task.Id;
                }
                while (true);
            }

            this.logger.LogInformation($"Pipeline {pipeline.Name} completed");
        }

        private async Task RunTask(TaskGraphNodeViewModel task, CancellationToken token)
        {
            this.logger.LogInformation($"Task {task.Task.Name} started");
            this.logger.LogInformation(Directory.GetCurrentDirectory());
#if DEBUG
            await ProcessHelper.RunProcessAsync("./bin/Debug/netcoreapp3.1/PipelineApp.TaskExample.exe");
#endif
#if !DEBUG
            await ProcessHelper.RunProcessAsync("PipelineApp.TaskExample.exe");
#endif
            this.logger.LogInformation($"Task {task.Task.Name} completed");
        }
    }
}