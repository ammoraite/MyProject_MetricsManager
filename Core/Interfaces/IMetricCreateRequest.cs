namespace MetricsMeneger.Interfaces
{
    public interface IMetricCreateRequest
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public string InstanceName { get; set; }
        public string CounterName { get; set; }
        public int Value { get; set; }
        public int Time { get; set; }
    }
}
