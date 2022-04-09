using System.Collections.Generic;
using System.Diagnostics;

namespace First_API.Controllers.MetricControllers.Base
{
    public static class GetInfo
    {
        public static List<string> GetCategoryesMetric()
        {
            List<string> categories = new(PerformanceCounterCategory.GetCategories().Length);
            foreach (var itemPerformanceCounterCategoryes in PerformanceCounterCategory.GetCategories())
            {
                categories.Add(itemPerformanceCounterCategoryes.CategoryName);
            }
            return categories;
        }
        public static IEnumerable<string> GetInstansesMetric(string categoryName)
        {
            foreach (var item in new PerformanceCounterCategory() { CategoryName = categoryName }.GetInstanceNames())
            {
                yield return item;
            }
        }
        public static IEnumerable<string> GetCounters(string InstanceName, string categoryName)
        {
            try
            {
                var cat = new PerformanceCounterCategory() { CategoryName = categoryName };
                if (cat.InstanceExists(InstanceName))
                {
                    foreach (var item in cat.GetCounters(InstanceName))
                    {
                        yield return item.CounterName;
                    }
                }
                else
                {
                    yield return $"{InstanceName} не существует в категории с таким именем";
                }
            }
            finally
            {

            }

        }
    }
}
