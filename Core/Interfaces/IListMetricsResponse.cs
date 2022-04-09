using System.Collections.Generic;

namespace MetricsMeneger.Interfaces
{
    public interface IListMetricsResponse<T>
    {
        public List<T> ResponseData { get; set; }
    }
}
