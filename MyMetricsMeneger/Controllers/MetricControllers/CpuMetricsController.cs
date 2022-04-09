//using MyMetricsMeneger.Controllers.MetricControllers;
//using MyMetricsMeneger.Controllers.MetricControllers.Base;
//using Microsoft.AspNetCore.Mvc;

//namespace MyMetricsMeneger.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class CpuMetricsController : ControllerBase
//    {
//        private readonly ControllerBaseWorker _ControllerBaseWorker;
//        public CpuMetricsController(IRepositorySqlMetrics repository)
//        {
//            _ControllerBaseWorker = new ControllerBaseWorker(repository);
//        }

//        [HttpGet("all")]
//        public IActionResult GetAll()
//        {
//            return Ok(_ControllerBaseWorker.GetAllmetric());
//        }
//    }
//}


