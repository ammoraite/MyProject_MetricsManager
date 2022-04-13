using System.Collections.Generic;

namespace WPF_Client.Models.MetricModels
{
    public class InstanceNamesResponse
    {
        public List<InstanceName> InstanceNames { get; set; }
    }
    public class InstanceName
    {
        public string _instanceName { get; set; }
    }
}