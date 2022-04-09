using MetricsMeneger.DAL.BaseModuls;
using MetricsMeneger.Services.Repositories;
using Quartz;
using System.Threading.Tasks;

namespace MetricsAgent.Jobs
{
    public class MetricJob : IJob
    {
        private IRepositoryMetrics _repository;
        public MetricJob(IRepositoryMetrics repository)
        {
            _repository = repository;

        }
        public Task Execute(IJobExecutionContext context)
        {
            JobWorker.Run(_repository);
            return Task.CompletedTask;
        }
    }
}
