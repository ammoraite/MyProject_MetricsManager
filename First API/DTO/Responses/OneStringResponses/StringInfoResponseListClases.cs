using MetricsMeneger.DTO.DTOModules;
using System.Collections.Generic;

namespace MetricsMeneger.DTO.Responses.OneStringResponses
{
    public class CategoryNamesResponse{ public List<CategoryName> CategoryNames { get; set; }}
    public class CounterNamesResponse{ public List<CounterName> CounterNames { get; set; } }
    public class InstanceNamesResponse{ public List<InstanceName> InstanceNames { get; set; } }
    public class TableNamesResponse{ public List<TableName> TableNames { get; set; } }

}
