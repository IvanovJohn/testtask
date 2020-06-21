namespace PipelinesApp.Api.Runner
{
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.InteropServices;
    using System.Threading;
    using System.Threading.Tasks;

    using Microsoft.CodeAnalysis.CSharp.Syntax;
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

            var processStartInfo = new ProcessStartInfo();
            var currentDir =
                Path.GetDirectoryName(new System.Uri(Assembly.GetExecutingAssembly().CodeBase).AbsolutePath);

            processStartInfo.FileName = Path.Combine(currentDir, "PipelineApp.TaskExample");

            // when we run it form vs, file has extension
            if (!File.Exists(processStartInfo.FileName))
            {
                processStartInfo.FileName = Path.Combine(currentDir, "PipelineApp.TaskExample.exe");
                processStartInfo.WorkingDirectory = currentDir;
            }

            this.logger.LogInformation(processStartInfo.FileName);
            await ProcessHelper.RunProcessAsync(processStartInfo);

            this.logger.LogInformation($"Task {task.Task.Name} completed");
        }
    }
}