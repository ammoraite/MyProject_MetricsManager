using FluentMigrator;
using MetricsMeneger.Services.Repositories;
using System.Diagnostics;

namespace MetricsMeneger.Services
{
    public class MenegerMigrations
    {

        [Migration(0)]
        public class MenagerMetricsMigration : Migration
        {
            public override void Up()
            {
                foreach (var item in PerformanceCounterCategory.GetCategories())
                {
                    Create.Table($"{item.CategoryName}").WithColumn("Id").AsInt64().PrimaryKey().Identity()
                                        .WithColumn("CategoryName").AsString()
                                        .WithColumn("InstanceName").AsString()
                                        .WithColumn("CounterName").AsString()
                                        .WithColumn("Value").AsInt64()
                                        .WithColumn("Time").AsInt32();
                }
                    
                
                
                
            }

            public override void Down()
            {
                #region MetricsTable

                foreach (var item in JobWorker._performanceCounters)
                {
                    Delete.Table($"{item._counter.CategoryName}");
                }
                #endregion
            }
        }
    }
}