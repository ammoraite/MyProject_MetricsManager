//using AutoMapper;
//using MetricsMeneger.Services.Repositories;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.Extensions.Logging;
//using System;
//using System.Net.Http;
//using System.Text.Json;
//using MetricsMeneger.Controllers.MetricControllers;

//namespace MetricsMeneger.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class MetricsAgentClient : ControllerBase, IMetricsAgentClient
//    {

//        private readonly ILogger<MetricsAgentClient> _MetricsAgentLogger;

//        private readonly ControllerBaseWorker _cpuControllerBaseWorker;

//        private readonly HttpClient _httpClient;

//        public MetricsAgentClient(IRepositoryMetrics repository,
//                                    ILogger<MetricsAgentClient> logger,
//                                    IMapper mapper, HttpClient httpClient)
//        {
//            _cpuControllerBaseWorker = new ControllerBaseWorker(repository, mapper);

//            _MetricsAgentLogger = logger;
//            logger.LogDebug(1, $"NLog встроен в MetricsAgentClient");

//        }

//        public AllHddMetricsApiResponse GetAllHddMetrics(GetAllHddMetricsApiRequest request)
//        {
//            var fromParameter = request.FromTime.TotalSeconds;
//            var toParameter = request.ToTime.TotalSeconds;
//            var httpRequest = new HttpRequestMessage(HttpMethod.Get, $"{request.ClientBaseAddress}/api/hddmetrics/from/{fromParameter}/to/{toParameter}");
//            try
//            {
//                HttpResponseMessage response =
//                _httpClient.SendAsync(httprequest).Result;
//                using var responseStream =
//                response.Content.ReadAsStreamAsync().Result;
//                return
//                JsonSerializer.DeserializeAsync<AllHddMetricsApiResponse>(responseStream).Result;
//            }
//            catch (Exception ex)
//            {
//                _MetricsAgentLogger.LogError(ex.Message);
//            }
//            return null;
//        }


//        [HttpGet("agent/{agentId}/from/{fromTime}/to/{toTime}")]
//        public IActionResult GetMetricsFromAgent([FromRoute] int agentId,
//                                                 [FromRoute] TimeSpan fromTime,
//                                                 [FromRoute] TimeSpan toTime)
//        {
//            var request = new HttpRequestMessage(HttpMethod.Get,"http://localhost:5000/api/cpumetrics/from/1/to/999999?var=val&var1=val1");

//            request.Headers.Add("Accept", "application/vnd.github.v3+json");

//            var client = clientFactory.CreateClient();

//            HttpResponseMessage response = client.SendAsync(request).Result;
//            if (response.IsSuccessStatusCode)
//            {
//                using var responseStream = response.Content.ReadAsStreamAsync().Result;
//                var metricsResponse = JsonSerializer.DeserializeAsync
//                <AllCpuMetricsApiResponse>(responseStream).Result;
//            }

//            else
//            {
//                // ошибка при получении ответа
//            }
//            return Ok();
//        }
//    }
//}





