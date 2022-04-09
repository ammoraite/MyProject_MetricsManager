//using MyMetricsMeneger.Controllers.MetricControllers.Base;
//using MyMetricsMeneger.DAL.BaseModuls;
//using System.Collections.Generic;
//using System.Diagnostics;

//namespace MyMetricsMeneger.Services.Repositories
//{

//    public class SqlLiteHardWareMetricsREpository : IRepositorySqlMetrics
//    {
//        private const string CpuRepConnectionString = "Data Source=HardwareMetricinthisPC.db";

//        private readonly RepositoryMetricWorker _SQLRepositoryWorker;

//        public  List<MetricInSQLbase> _allmetricsInPc { get; private set; }
//        public List<PerformanceCounter> _allPerformanceCounterDoInPc { get; set; }


//        public SqlLiteHardWareMetricsREpository()
//        {
//            _SQLRepositoryWorker = new(CpuRepConnectionString, "HardwareMetric");
//            _allmetricsInPc = _SQLRepositoryWorker.GetAllMetricInSQLbase();
//            _allPerformanceCounterDoInPc = _SQLRepositoryWorker.GetAllPerformanceCounterWhatDo();
//        }

//        public List<MetricInSQLbase> GetAllInSQLbase() => _allmetricsInPc = _SQLRepositoryWorker.GetAllMetricInSQLbase();

//        public void SetFecurience(int freq, int id) => _SQLRepositoryWorker.SetFecurience(freq, id);
//        public void SetDoit(bool Do, int id) => _SQLRepositoryWorker.SetDoit(Do, id);

//    }
//}
