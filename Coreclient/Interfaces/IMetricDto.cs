using System;

namespace MetricsMeneger.Interfaces
{
    public interface IMetricDto
    {
        public int Id { get; set; }
        public int Value { get; set; }
        public TimeSpan Time { get; set; }
    }
}
