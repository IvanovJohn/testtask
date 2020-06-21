namespace PipelinesApp.Api.Helpers
{
    using System.Diagnostics;
    using System.Threading.Tasks;

    internal class ProcessHelper
    {
        public static Task<int> RunProcessAsync(ProcessStartInfo processStartInfo)
        {
            var tcs = new TaskCompletionSource<int>();

            var process = new Process
                              {
                                  StartInfo = processStartInfo,
                                  EnableRaisingEvents = true,
                              };

            process.Exited += (sender, args) =>
                {
                    tcs.SetResult(process.ExitCode);
                    process.Dispose();
                };

            process.Start();

            return tcs.Task;
        }
    }
}