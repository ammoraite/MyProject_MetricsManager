using MyMetricsMeneger.DAL.BaseModuls;
using System.Collections.Generic;

namespace MyMetricsMeneger.Responses
{
    public class ResponseAllMetrics
    {
        public List<MetricInSQLbase> Metrics { get; set; }
    }

}
