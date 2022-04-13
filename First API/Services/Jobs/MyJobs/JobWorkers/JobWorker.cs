using MetricsMeneger.DAL.BaseModuls;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;


namespace MetricsMeneger.Services.Repositories
{
    internal static class JobWorker
    {
        public static List<PerformanceCounter> _performanceCounters { get; private set; } = new List<PerformanceCounter>();

        internal static void CollectAndRecordMetricsInTheDataBase(IRepositoryMetrics _repository)
        {
            if (_performanceCounters.Count > 0)
            {
                _performanceCounters.AsParallel().ForAll(x => 
                {
                    _repository.Create(new Metric()
                    {
                        CategoryName = x.CategoryName,
                        InstanceName = x.InstanceName,
                        CounterName = x.CounterName,
                        Time = TimeSpan.FromSeconds(DateTimeOffset.UtcNow.ToUnixTimeSeconds()),
                        Value = Convert.ToInt32(x.NextValue())
                    });
                });
            }
        }
        private static void AddCounter(PerformanceCounter performanceCounter)
        {
            if (!ExistCounter(performanceCounter))
            {
                _performanceCounters.Add(performanceCounter);
            }
        }
        private static void RemoveCounter(PerformanceCounter performanceCounter)
        {
            _performanceCounters.Where(x =>
            (x.CategoryName == performanceCounter.CategoryName) &&
            (x.InstanceName == performanceCounter.InstanceName) &&
            (x.CounterName == performanceCounter.CounterName))
            .AsParallel().ForAll(x => { _performanceCounters.Remove(x); });
        }
        private static bool ExistCounter(PerformanceCounter performanceCounter)
        {
            bool Exist = false;

            foreach (var item in _performanceCounters)
            {
                if (item.CategoryName == performanceCounter.CategoryName &&
                    item.InstanceName == performanceCounter.InstanceName &&
                    item.CounterName == performanceCounter.CounterName)
                {
                    Exist = true;
                    break;
                }
            }

            return Exist;
        }
        internal static string SetOnOff(PerformanceCounter performanceCounter, bool doOrNot)
        {
            if (ExistCounter(performanceCounter))
            {
                if (doOrNot)
                {
                    return $"PerformanceCounter уже был включен";
                }
                else
                {
                    RemoveCounter(performanceCounter);
                    return $"PerformanceCounter отключен";
                }
            }
            else if (doOrNot)
            {
                AddCounter(performanceCounter);
                return $"PerformanceCounter успешно включен";
            }
            return $"PerformanceCounter не существует для отключения";
        }
        internal static void SetOffCategory(string categoryName)
        {
            _performanceCounters.Where(x => x.CategoryName == categoryName)
            .AsParallel().ForAll(x => { _performanceCounters.Remove(x); });
        }
    }
}
