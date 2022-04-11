using MetricsMeneger.DAL.BaseModuls;
using MetricsMeneger.DAL.Modules;
using Microsoft.Extensions.Logging;
using NLog;
using System;
using System.Collections.Generic;
using System.Threading;

namespace MetricsMeneger.Services.Repositories
{
    public static class JobWorker
    {        
        public static List<SettingnsItem> _performanceCounters { get; private set; } = new List<SettingnsItem>();

        public static void Run(IRepositoryMetrics _repository)
        {
            if (_performanceCounters.Count > 0)
            {
               
                foreach (var _counter in _performanceCounters)
                {
                    if (_counter._doOrNot)
                    {
                        AddItemInBaseData(_repository, _counter);
                    }
                }
            }

        }
        private static void AddItemInBaseData(IRepositoryMetrics _repository, SettingnsItem _counter)
        {
            _repository.Create(new Metric()
            {
                CategoryName = _counter._counter.CategoryName,
                InstanceName = _counter._counter.InstanceName,
                CounterName = _counter._counter.CounterName,
                Time = TimeSpan.FromSeconds(DateTimeOffset.UtcNow.ToUnixTimeSeconds()),
                Value = Convert.ToInt32(_counter._counter.NextValue())
            });
        }
        private static void AddCounter(SettingnsItem performanceCounter)
        {
            if (!ExistCounter(performanceCounter))
            {
                _performanceCounters.Add(performanceCounter);
            }
        }
        private static void RemoveCounter(SettingnsItem performanceCounter)
        {
            if (ExistCounter(performanceCounter))
            {
                foreach (var item in _performanceCounters)
                {
                    if (item._counter.CategoryName == performanceCounter._counter.CategoryName &&
                    item._counter.InstanceName == performanceCounter._counter.InstanceName &&
                    item._counter.CounterName == performanceCounter._counter.CounterName)
                    {
                        _performanceCounters.Remove(item);
                    }
                }
            }
        }
        private static bool ExistCounter(SettingnsItem performanceCounter)
        {
            bool Exist = false;

            foreach (var item in _performanceCounters)
            {
                if (item._counter.CategoryName == performanceCounter._counter.CategoryName &&
                    item._counter.InstanceName == performanceCounter._counter.InstanceName &&
                    item._counter.CounterName == performanceCounter._counter.CounterName)
                {
                    Exist = true;
                }
            }

            return Exist;
        }
        public static void SetOnOff(SettingnsItem performanceCounter)
        {
            if (ExistCounter(performanceCounter))
            {
                if (performanceCounter._doOrNot)
                {
                    RemoveCounter(performanceCounter);
                    AddCounter(performanceCounter);
                }
                else
                {
                    RemoveCounter(performanceCounter);
                }
            }
            else if (performanceCounter._doOrNot)
            {
                AddCounter(performanceCounter);
            }

        }
    }
}
