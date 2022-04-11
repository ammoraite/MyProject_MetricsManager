using MetricsMeneger.Interfaces;
using System;

namespace MetricsMeneger.DAL.BaseModuls
{
    public class DtoMetric : IMetricDto
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public string InstanceName { get; set; }
        public string CounterName { get; set; }
        public int Value { get; set; }
        public TimeSpan Time { get; set; }
    }
}
