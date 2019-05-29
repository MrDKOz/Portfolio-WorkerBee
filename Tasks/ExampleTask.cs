using NLog;
using System;
using System.Threading;
using WorkerBee.Interfaces;

namespace WorkerBee.Tasks
{
    class ExampleTask : IWorkerBeeTask
    {
        private readonly Logger _logger = LogManager.GetLogger("example");

        public void PerformTask(CancellationToken cancellationToken)
        {
            _logger.Info("Example task initialised...");

            while (!cancellationToken.IsCancellationRequested)
            {
                try
                {
                    _logger.Info("Generating random numbers...");

                    int a = FetchRandomNumber();
                    int b = FetchRandomNumber(50, 61);

                    int result = SumOf(a, b);

                    _logger.Info($"{a} + {b} = {result}");

                    _logger.Info("Sum fetched, sleeping for 5 minutes before doing more maths...");
                    cancellationToken.WaitHandle.WaitOne(TimeSpan.FromMinutes(5));
                }
                catch (Exception ex)
                {
                    _logger.Error($"An error occurred during the example task. The task will now exit.{Environment.NewLine}" +
                                  $"Error: {ex.Message}{Environment.NewLine}" +
                                  $"Stack Trace: {ex.StackTrace}");

                    // Cancel the token so we can drop out of the thread.
                    CancellationTokenSource source = new CancellationTokenSource();
                    cancellationToken = source.Token;
                    source.Cancel();
                }
            }
        }

        /// <summary>
        /// Generates a random number between the given range.
        /// </summary>
        /// <param name="rangeMin">The minimum value of the returned integer.</param>
        /// <param name="rangeMax">The maximum -1 value of the returned integer.</param>
        /// <returns>An integer between given min-max, or -1 if the given range is valid.</returns>
        private int FetchRandomNumber(int rangeMin = 1, int rangeMax = 20)
        {
            int result = -1;
            _logger.Debug($"Generating integer between {rangeMin} and {rangeMax}...");

            // Check the given range makes sense.
            if (rangeMin <= rangeMax)
            {
                Random rnd = new Random();

                result = rnd.Next(rangeMin, rangeMax);

                _logger.Debug($"Generated pseudo-random number of {result}!");
            }
            else
            {
                _logger.Error("The range minimum value must be lower than the range maximum value.");
            }

            return result;
        }

        /// <summary>
        /// Returns the sum of the given integers.
        /// </summary>
        /// <param name="a">First integer.</param>
        /// <param name="b">Second integer./</param>
        /// <returns>The sum of a + b.</returns>
        private int SumOf(int a, int b)
        {
            _logger.Trace($"Attempting to add {a} and {b}...");

            int result = a + b;

            _logger.Trace($"Result of {result} found!");

            return result;
        }
    }
}
