using FluentMigrator;
using MetricsMeneger.Services.Repositories;
using System.Diagnostics;
using System.Linq;

namespace MetricsMeneger.Services
{
    public class MenegerMigrations
    {

        [Migration(0)]
        public class MenagerMetricsMigration : Migration
        {
            public override void Up()
            {
                PerformanceCounterCategory.GetCategories().AsParallel().ForAll(category =>
                {
                    Create.Table($"{category.CategoryName}").WithColumn("Id").AsInt64().PrimaryKey().Identity()
                                        .WithColumn("CategoryName").AsString()
                                        .WithColumn("InstanceName").AsString()
                                        .WithColumn("CounterName").AsString()
                                        .WithColumn("Value").AsInt64()
                                        .WithColumn("Time").AsInt32();
                });
            }

            public override void Down()
            {              
                JobWorker._performanceCounters.AsParallel().ForAll(x => { Delete.Table($"{x.CategoryName}");});
            }
        }
    }
}