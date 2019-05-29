using NLog;
using System;
using System.Diagnostics;
using System.Reflection;
using System.ServiceProcess;
using System.Threading;
using System.Threading.Tasks;
using WorkerBee.Tasks;

namespace WorkerBee
{
    internal class Program
    {
        private static readonly Logger logger = LogManager.GetLogger("service");
        private static Service service = new Service();
        private static readonly CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

        static void Main()
        {
            Version version = Assembly.GetExecutingAssembly().GetName().Version;
            logger.Info($"Starting WorkerBee Service v{version}");

            if  (Environment.UserInteractive && Debugger.IsAttached)
            {
                logger.Info("Running in console mode");

                RunTasks();

                Console.WriteLine("Press any key to stop the service...");
                Console.ReadKey(true);

                Stop();
            }
            else
            {
                logger.Info("Starting service");
                ServiceBase.Run(service);
            }
        }

        class Service : ServiceBase
        {
            public Service()
            {
                ServiceName = "WorkerBee";
            }

            protected override void OnStart(string[] args)
            {
                base.OnStart(args);
                RunTasks();
            }

            protected override void OnStop()
            {
                Program.Stop();
            }
        }

        /// <summary>
        /// Prepares relevant tasks for execution.
        /// </summary>
        private static void RunTasks()
        {
            logger.Info("Starting tasks...");

            logger.Info("Starting example task thread...");
            Task.Run(() => new ExampleTask().PerformTask(cancellationTokenSource.Token));

            logger.Info("All task threads created...");
        }

        /// <summary>
        /// Stops this instance, and disposes of anything.
        /// </summary>
        private static void Stop()
        {
            logger.Info("Stopping service and cleaning up...");

            cancellationTokenSource.Cancel();
            cancellationTokenSource.Dispose();
        }

    }
}
