using MyMetricsMeneger.Interfaces;

namespace MyMetricsMeneger.DAL.BaseModuls
{
    public class MetricInSQLbase : IHardwareMetric
    {
        public int Id { get; set; }
        public string catrgoryName { get; set; }
        public string instanseName { get; set; }
        public string counterName { get; set; }
        public int frequencyOfMetricaCollection { get; set; }
        public bool Do { get; set; }
    }
}
