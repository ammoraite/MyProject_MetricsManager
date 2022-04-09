//using FluentMigrator;

//namespace MyMetricsMeneger.Services
//{
//    public class Migrators
//    {
//        [Migration(1)]
//        public class MetricsMigration : Migration
//        {
//            public override void Up()
//            {
//                Create.Table("HardwareMetric").WithColumn("Id").AsInt64().PrimaryKey().Identity()
//                    .WithColumn("catrgoryName").AsString()
//                    .WithColumn("instanseName").AsString()
//                    .WithColumn("counterName").AsString()
//                    .WithColumn("Frequencyofmetricacollection").AsInt32()
//                    .WithColumn("Do").AsBoolean();
//            }

//            public override void Down()
//            {
//                Delete.Table("HardwareMetricinthisPC");
//            }
//        }
//    }
    
//}