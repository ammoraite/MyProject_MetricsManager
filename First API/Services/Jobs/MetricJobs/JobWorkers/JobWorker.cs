using MetricsMeneger.DAL.BaseModuls;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace MetricsMeneger.Services.Repositories
{
    public static class JobWorker
    {
        public static Dictionary<PerformanceCounter,bool> _performanceCounters { get; private set; } = new Dictionary<PerformanceCounter, bool>();
        public static void Run(IRepositoryMetrics _repository)
        {
            if (_performanceCounters.Count > 0)
            {
                foreach (var _counter in _performanceCounters)
                {
                    if (_counter.Value)
                    {
                        _repository.Create(new Metric
                        {
                            CategoryName = _counter.Key.CategoryName,
                            InstanceName = _counter.Key.InstanceName,
                            CounterName = _counter.Key.InstanceName,
                            Time = TimeSpan.FromSeconds(DateTimeOffset.UtcNow.ToUnixTimeSeconds()),
                            Value = Convert.ToInt32(_counter.Key.NextValue())
                        });
                    }
                   
                }
            }
        }

        public static void Add(PerformanceCounter performanceCounter,bool Do)
        {
            if (!_performanceCounters.ContainsKey(performanceCounter))
            {
                _performanceCounters.Add(performanceCounter, Do);
            }
        }
        public static void SetOnOff(PerformanceCounter performanceCounter, bool DoOrNot)
        {
            if (_performanceCounters.ContainsKey(performanceCounter))
            {
                _performanceCounters.Remove(performanceCounter);
                _performanceCounters.Add(performanceCounter, DoOrNot);
            }

        }
    }
}
