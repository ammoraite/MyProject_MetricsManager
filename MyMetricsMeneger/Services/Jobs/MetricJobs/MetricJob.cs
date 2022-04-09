using MyMetricsMeneger;
using Quartz;
using System;
using System.Threading.Tasks;

namespace MetricsAgent.Jobs
{
    public class MetricJob : IJob
    {
        //private IRepositorySqlMetrics _repository;
        private ISettingsMyMetricsMeneger _settingMeneger;
        public MetricJob( ISettingsMyMetricsMeneger settingMeneger)
        {
            //_repository = repository;
            _settingMeneger = settingMeneger;
        }
        public Task Execute(IJobExecutionContext context)
        {
            foreach (var _itemMyPerfomanseCategory in _settingMeneger._myPerfomanseCategories)
            {
                foreach (var _itemMyPerfomanseCounter in _itemMyPerfomanseCategory._performanceCounters)
                {
                    if (_itemMyPerfomanseCategory._performanceCounters.Count!=0&& _itemMyPerfomanseCounter._performanceCounter.InstanceName=="_Total")
                    {
                        Console.WriteLine($"{_itemMyPerfomanseCategory._categoryName}\n" +
                        $"{_itemMyPerfomanseCounter._performanceCounter.CounterName}\n" +
                        $"{_itemMyPerfomanseCounter._performanceCounter.NextValue()}");
                    }
                    
                }
            }
            return Task.CompletedTask;
        }
    }
}
