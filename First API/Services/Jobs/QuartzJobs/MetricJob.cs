using MetricsMeneger.DAL.BaseModuls;
using MetricsMeneger.Services.Repositories;
using Quartz;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MetricsAgent.Jobs
{
    public class MetricJob : IJob
    {
        Mutex mutexObj = new();
        private IRepositoryMetrics _repository;
        public MetricJob(IRepositoryMetrics repository)
        {
            _repository = repository;

        }
        public Task Execute(IJobExecutionContext context)
        {
            JobWorker.CollectAndRecordMetricsInTheDataBase(_repository);
            return Task.CompletedTask;
        }
    }
}
