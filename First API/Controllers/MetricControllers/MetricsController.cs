using AutoMapper;
using First_API.Controllers.MetricControllers.Base;
using MetricsMeneger.Controllers.MetricControllers;
using MetricsMeneger.DTO.DTOModules;
using MetricsMeneger.DTO.Responses.OneStringResponses;
using MetricsMeneger.Services.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace MetricsMeneger.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MetricsController : ControllerBase, IMetricController
    {
        private IRepositoryMetrics _repository;
        private readonly ILogger<MetricsController> _controllerLogger;
        private readonly ControllerBaseWorker _controllerBaseWorker;

        public MetricsController(IRepositoryMetrics repository,
                                 ILogger<MetricsController> logger,
                                 IMapper mapper)
        {
            _repository = repository;
            _controllerBaseWorker = new ControllerBaseWorker(_repository, mapper);
            _controllerLogger = logger;
            logger.LogDebug(1, $"NLog встроен в CpuMetricController");
        }

        [HttpGet("GetallTables")]
        public IActionResult GetAllTablesInBaseData()
        {
            _controllerLogger.LogDebug(1, $"Отправлены все Tables");

            TableNamesResponse resTables = new TableNamesResponse() { TableNames = new List<TableName>() };

            foreach (var item in _repository.GetAllCatecoriesInBaseData())
            {
                resTables.TableNames.Add(new TableName() { _tableName = item });
            }
            return Ok(resTables);
        }

        [HttpGet("GetallCategory")]
        public IActionResult GetAllCategoryes()
        {
            _controllerLogger.LogDebug(1, $"Отправлены все Categoryes");

            CategoryNamesResponse resCategoryes = new CategoryNamesResponse() { CategoryNames = new List<CategoryName>() };

            foreach (var item in GetInfoPerformanceCounter.GetCategoryesMetric())
            {
                resCategoryes.CategoryNames.Add(new CategoryName() { _categoryName = item });
            }
            return Ok(resCategoryes);
        }

        [HttpGet("GetallInstanse")]
        public IActionResult GetAllInstanses([FromQuery] string category)
        {
            _controllerLogger.LogDebug(1, $"Отправлены все Instanses из категории \"{category}\"");

            InstanceNamesResponse resInstanse = new InstanceNamesResponse() { InstanceNames = new List<InstanceName>() };

            foreach (var item in GetInfoPerformanceCounter.GetInstansesMetric(category))
            {
                resInstanse.InstanceNames.Add(new InstanceName() { _instanceName = item });
            }
            return Ok(resInstanse);
        }

        [HttpGet("GetallCounters")]
        public IActionResult GetAllCounters([FromQuery] string category,
                                            [FromQuery] string instanse)
        {
            _controllerLogger.LogDebug(1, $"Отправлены все Counters связанные с категорией: \"{category}\" и instanse: \"{instanse}\"");

            CounterNamesResponse resCounters = new CounterNamesResponse() { CounterNames = new List<CounterName>() };

            foreach (var item in GetInfoPerformanceCounter.GetCounters(instanse, category))
            {
                resCounters.CounterNames.Add(new CounterName() { _counterName = item });
            }
            return Ok(resCounters);
        }

        [HttpPost("СonfiguringMetricsCollector")]
        public IActionResult UpdateCountersStatus([FromQuery] string category,
                                                  [FromQuery] string instanse,
                                                  [FromQuery] string counter,
                                                  [FromQuery] bool DoOrNot)
        {
            string summary = string.Empty;
            try
            {
                PerformanceCounter _counter = new PerformanceCounter(category, counter, instanse);
                summary = JobWorker.SetOnOff(_counter, DoOrNot);
                _controllerLogger.LogDebug(1, summary);

            }
            catch (Exception ex)
            {
                summary = ex.Message;
            }
            return Ok(summary);
        }
    }
}


