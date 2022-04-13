namespace MetricsMeneger.Interfaces
{
    public interface IMetricCreateRequest
    {
        public int Value { get; set; }
        public int Time { get; set; }
    }
}
