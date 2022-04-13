using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace First_API.Controllers.MetricControllers.Base
{
    public static class GetInfoPerformanceCounter
    {
        public static List<string> GetCategoryesMetric()
        {
            return PerformanceCounterCategory.GetCategories().Select(x=>x.CategoryName).ToList();
        }
        public static List<string> GetInstansesMetric(string categoryName)
        {
            return new PerformanceCounterCategory() { CategoryName = categoryName }.GetInstanceNames().ToList();
        }
        public static List<string> GetCounters(string InstanceName, string categoryName)
        {
            List<string> list = new();
            try
            {             
                var cat = new PerformanceCounterCategory() { CategoryName = categoryName };
                if (cat.InstanceExists(InstanceName))
                {
                    foreach (var item in cat.GetCounters(InstanceName))
                    {
                        list.Add(item.CounterName);
                    }
                }
                else
                {
                    list.Add($"{InstanceName} не существует в категории с таким именем");
                }
            }
            finally
            {

            }
            return list;
        }
    }
}
