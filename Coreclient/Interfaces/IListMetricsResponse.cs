using System.Collections.Generic;

namespace MetricsMeneger.Interfaces
{
    public interface IListMetricsResponse<T>
    {
        public List<T> Metrics { get; set; }
    }
}
