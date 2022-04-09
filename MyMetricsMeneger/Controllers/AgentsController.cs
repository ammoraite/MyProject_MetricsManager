using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

namespace MyMetricsMeneger.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AgentsController : ControllerBase
    {
        private readonly ILogger<AgentsController> _agentControllerLogger;

        public AgentsController(ILogger<AgentsController> logger)
        {
            _agentControllerLogger = logger;
            _agentControllerLogger.LogDebug(1, "NLog встроен в AgentsController");
        }

        [HttpPost("register")]
        public IActionResult RegisterAgent([FromBody] AgentInfo agentInfo)
        {
            _agentControllerLogger.LogDebug(1, $"Зарегистрирован агент (AgentId): {agentInfo.AgentId} ");
            return Ok();
        }

        [HttpPut("enable/{agentId}")]
        public IActionResult EnableAgentById([FromRoute] int agentId)
        {
            _agentControllerLogger.LogDebug(1, $"включен агент (AgentId): {agentId} ");
            return Ok();
        }
        [HttpPut("disable/{agentId}")]
        public IActionResult DisableAgentById([FromRoute] int agentId)
        {
            _agentControllerLogger.LogDebug(1, $"Агент выключен (AgentId): {agentId} ");
            return Ok();
        }
    }
    public class AgentInfo
    {
        public int AgentId { get; }
        public Uri AgentAddress { get; }
    }

}
