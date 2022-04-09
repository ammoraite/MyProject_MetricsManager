namespace MyMetricsMeneger.Interfaces
{
    public interface IHardwareMetric
    {
        public int Id { get; set; }
        public string catrgoryName { get; set; }
        public string instanseName { get; set; }
        public string counterName { get; set; }
        public int frequencyOfMetricaCollection { get; set; }
        public bool Do { get; set; }

    }
}
