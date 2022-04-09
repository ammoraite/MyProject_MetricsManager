//using AutoMapper;
//using MyMetricsMeneger.Controllers.MetricControllers.Base;
//using MyMetricsMeneger.DAL.BaseModuls;
//using MyMetricsMeneger.Responses;
//using System.Collections.Generic;

//namespace MyMetricsMeneger.Controllers.MetricControllers
//{
//    public class ControllerBaseWorker
//    {
//        private IRepositorySqlMetrics repository;
//        private readonly IMapper mapper;
//        public ControllerBaseWorker(IRepositorySqlMetrics repository)
//        {
//            this.repository = repository;
//        }
//        public ResponseAllMetrics GetAllmetric()
//        {
//            var response = new ResponseAllMetrics()
//            {
//                Metrics = new List<MetricInSQLbase>()
//            };
//            foreach (var metric in repository.GetAllInSQLbase())
//            {
//                response.Metrics.Add(metric);
//            }
//            return response;
//        }
//    }
//}
