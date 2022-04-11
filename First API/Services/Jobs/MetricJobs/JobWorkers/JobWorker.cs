using MetricsMeneger.DAL.BaseModuls;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace MetricsMeneger.Services.Repositories
{
    public static class JobWorker
    {
        public static Dictionary<PerformanceCounter,bool> _performanceCounters { get;private set; } = new Dictionary<PerformanceCounter, bool>();
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
                            CounterName = _counter.Key.CounterName,
                            Time = TimeSpan.FromSeconds(DateTimeOffset.UtcNow.ToUnixTimeSeconds()),
                            Value = Convert.ToInt32(_counter.Key.NextValue())
                        });
                    }
                   
                }
            }
        }

        private static void AddCounter(PerformanceCounter performanceCounter,bool Do)
        {
            if (!ExistCounter(performanceCounter))
            {
                _performanceCounters.Add(performanceCounter, Do);
            }
        }
        private static void RemoveCounter(PerformanceCounter performanceCounter)
        {
            if (ExistCounter(performanceCounter))
            {

                for (int i = 0; i < _performanceCounters.Count; i++)
                {
                    if (item.Key.CategoryName == performanceCounter.CategoryName &&
                    item.Key.InstanceName == performanceCounter.InstanceName &&
                    item.Key.CounterName == performanceCounter.CounterName)
                    {

                    }
                }
                    
                
            }
        }

        private static bool ExistCounter(PerformanceCounter performanceCounter)
        {
            bool Exist = false;

            foreach (var item in _performanceCounters)
            {
                if (item.Key.CategoryName == performanceCounter.CategoryName &&
                    item.Key.InstanceName == performanceCounter.InstanceName &&
                    item.Key.CounterName == performanceCounter.CounterName)
                {
                    Exist = true;
                }
            }

            return Exist;
        }

        public static void SetOnOff(PerformanceCounter performanceCounter, bool DoOrNot)
        {
            if (ExistCounter(performanceCounter))
            {
                if (DoOrNot)
                {
                    RemoveCounter(performanceCounter);
                    AddCounter(performanceCounter,DoOrNot);
                }                
                else
                {
                    RemoveCounter(performanceCounter);
                }
            }
            else if (DoOrNot)
            {
                AddCounter(performanceCounter, DoOrNot);
            }
            
        }
    }
}
