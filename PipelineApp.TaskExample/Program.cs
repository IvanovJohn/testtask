namespace PipelineApp.TaskExample
{
    using System;
    using System.Threading;

    class Program
    {
        static void Main(string[] args)
        {
            var rand = new Random();
            var sleepTime = rand.Next(1000, 5000);
            Console.WriteLine($"Started at {DateTime.UtcNow}, will be executing in {sleepTime}");
            Thread.Sleep(sleepTime);
            Console.WriteLine($"Finished at {DateTime.UtcNow}");
        }
    }
}
