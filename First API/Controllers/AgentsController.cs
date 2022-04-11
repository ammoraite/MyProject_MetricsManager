//using Microsoft.AspNetCore.Mvc;
//using Microsoft.Extensions.Logging;
//using System;
//using MetricsMeneger.Client;
//using MetricsMeneger.DTO.Requests;

//namespace MetricsMeneger.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class AgentsController : ControllerBase
//    {
//        private readonly ILogger<AgentsController> _agentControllerLogger;
//        private MetricsAgentClient metricsAgentClient;

//        public AgentsController(ILogger<AgentsController> logger)
//        {
//            _agentControllerLogger = logger;
//            _agentControllerLogger.LogDebug(1, "NLog встроен в AgentsController");
//        }

//        [HttpPost("register")]
//        public IActionResult RegisterAgent([FromBody] AgentInfo agentInfo)
//        {
//            _agentControllerLogger.LogDebug(1, $"Зарегистрирован агент (AgentId): {agentInfo.AgentId} ");
//            return Ok();
//        }

//        [HttpPut("enable/{agentId}")]
//        public IActionResult EnableAgentById([FromRoute] int agentId)
//        {
//            _agentControllerLogger.LogDebug(1, $"включен агент (AgentId): {agentId} ");
//            return Ok();
//        }
//        [HttpPut("disable/{agentId}")]
//        public IActionResult DisableAgentById([FromRoute] int agentId)
//        {
//            _agentControllerLogger.LogDebug(1, $"Агент выключен (AgentId): {agentId} ");
//            return Ok();
//        }

//        [HttpGet("agent/{agentId}/from/{fromTime}/to/{toTime}")]
//        public IActionResult GetMetricsFromAgent([FromRoute] int agentId, [FromRoute]
//            TimeSpan fromTime, [FromRoute] TimeSpan toTime)
//        {
//            // логируем, что мы пошли в соседний сервис
//            _agentControllerLogger.LogInformation($"starting new request to metrics agent");
//            // обращение в сервис
//            var metrics = metricsAgentClient.GetAllHddMetrics(new GetAllHddMetricsApiRequest
//            {
//                FromTime = fromTime,
//                ToTime = toTime
//            });
//            // возвращаем ответ
//            return Ok(metrics);
//        }
//    }
//    public class AgentInfo
//    {
//        public int AgentId { get; }
//        public Uri AgentAddress { get; }
//    }

//}
