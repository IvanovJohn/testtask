namespace PipelinesApp.Api.Pipelines.Commands
{
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Threading;
    using System.Threading.Tasks;

    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Logging;

    using PipelinesApp.Api.BackgroundTasks;
    using PipelinesApp.Api.Core.Events;
    using PipelinesApp.Api.Core.Queries;
    using PipelinesApp.Api.Helpers;
    using PipelinesApp.Api.Pipelines.Events;
    using PipelinesApp.Api.Pipelines.Queries;
    using PipelinesApp.Api.Pipelines.ViewModels;
    using PipelinesApp.Api.Settings;
    using PipelinesApp.Api.Tasks.Events;

    internal class RunPipelineCommand : AbstractPipelineCommand<RunPipelineCommandContext>
    {
        private readonly IQueryDispatcher queryDispatcher;
        private readonly IBackgroundTaskQueue backgroundTaskQueue;
        private readonly ILogger<RunPipelineCommand> logger;
        private readonly CancellationToken appCancellationToken;
        private readonly IEventDispatcher eventDispatcher;

        public RunPipelineCommand(
            IMongoDbSettings mongoDbSettings,
            IQueryDispatcher queryDispatcher,
            IBackgroundTaskQueue backgroundTaskQueue,
            ILogger<RunPipelineCommand> logger,
            IHostApplicationLifetime applicationLifetime,
            IEventDispatcher eventDispatcher)
            : base(mongoDbSettings)
        {
            this.queryDispatcher = queryDispatcher;
            this.backgroundTaskQueue = backgroundTaskQueue;
            this.logger = logger;
            this.eventDispatcher = eventDispatcher;
            this.appCancellationToken = applicationLifetime.ApplicationStopping;
        }

        public override async Task Execute(RunPipelineCommandContext commandContext)
        {
            var pipeline = await this.queryDispatcher.Ask<PipelineByIdCriterion, PipelineDetailsViewModel>(new PipelineByIdCriterion { Id = commandContext.Id });
            this.backgroundTaskQueue.QueueBackgroundWorkItem(async token =>
                {
                    await this.Run(pipeline, this.appCancellationToken);
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

            this.eventDispatcher.PublishEvent(new PipelineCompletedEvent { Pipeline = pipeline });
            this.logger.LogInformation($"Pipeline {pipeline.Name} completed");
        }

        private async Task RunTask(TaskGraphNodeViewModel task, CancellationToken token)
        {
            var stopWatch = new Stopwatch();
            stopWatch.Start();
            this.logger.LogInformation($"Task {task.Task.Name} started");

            var processStartInfo = new ProcessStartInfo();
            var currentDir =
                Path.GetDirectoryName(new System.Uri(Assembly.GetExecutingAssembly().CodeBase).AbsolutePath);

            processStartInfo.FileName = Path.Combine(currentDir, "PipelineApp.TaskExample");

            // when we run it from vs (and build in win), file has extension
            if (!File.Exists(processStartInfo.FileName))
            {
                processStartInfo.FileName = Path.Combine(currentDir, "PipelineApp.TaskExample.exe");
                processStartInfo.WorkingDirectory = currentDir;
            }

            await ProcessHelper.RunProcessAsync(processStartInfo);

            stopWatch.Stop();
            this.eventDispatcher.PublishEvent(new TaskCompletedEvent { TaskId = task.Task.Id, Duration = stopWatch.ElapsedMilliseconds });
            this.logger.LogInformation($"Task {task.Task.Name} completed in {stopWatch.ElapsedMilliseconds} ms");
        }
    }
}