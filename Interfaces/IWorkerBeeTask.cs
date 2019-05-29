using System.Threading;

namespace WorkerBee.Interfaces
{
    interface IWorkerBeeTask
    {
        void PerformTask(CancellationToken cancellationToken);
    }
}
