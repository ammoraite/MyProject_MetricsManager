using AutoMapper;
using First_API.Controllers.MetricControllers.Base;
using MetricsMeneger.Controllers.MetricControllers;
using MetricsMeneger.DAL.BaseModuls;
using MetricsMeneger.DTO.Requests;
using MetricsMeneger.Responses;
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

        private readonly ILogger<MetricsController> _cpuLogger;

        private readonly ControllerBaseWorker _controllerBaseWorker;

        public MetricsController(IRepositoryMetrics repository,
                                    ILogger<MetricsController> logger,
                                    IMapper mapper)
        {
            _controllerBaseWorker = new ControllerBaseWorker(repository, mapper, logger);
            _cpuLogger = logger;
            logger.LogDebug(1, $"NLog встроен в CpuMetricController");
        }

        [HttpPost("create")]
        public IActionResult Create([FromBody] CpuRequestMetricCreate request)
        {
            _controllerBaseWorker.AddMetricFromRequest(request, new Metric());
            _cpuLogger.LogDebug(1, $"Добавлена CpuMetric");
            return Ok();
        }

        [HttpGet("all")]
        public IActionResult GetAll()
        {
            _cpuLogger.LogDebug(1, $"Отправлены все CpuMetric");
            return Ok();
        }
        [HttpGet("allcategory")]
        public IActionResult GetAllCategoryes()
        {
            _cpuLogger.LogDebug(1, $"Отправлены все Categoryes");

            Response resCategoryes = new Response() { ResponseData = new List<DtoMetric>() };


            foreach (var item in GetInfo.GetCategoryesMetric())
            {
                resCategoryes.ResponseData.Add(new DtoMetric() { CategoryName = item });
            }
            return Ok(resCategoryes);
        }

        [HttpGet("allInstanse")]
        public IActionResult GetAllInstanses([FromQuery] string category)
        {
            _cpuLogger.LogDebug(1, $"Отправлены все Instanses");
            Response cat = new Response() { ResponseData = new List<DtoMetric>() };
            foreach (var item in GetInfo.GetInstansesMetric(category))
            {
                cat.ResponseData.Add(new DtoMetric() { InstanceName = item });
            }
            return Ok(cat);
        }
        [HttpGet("allCounters")]
        public IActionResult GetAllCounters([FromQuery] string category, [FromQuery] string instanse)
        {


            _cpuLogger.LogDebug(1, $"Отправлены все Counters");

            Response cat = new Response() { ResponseData = new List<DtoMetric>() };


            foreach (var item in GetInfo.GetCounters(instanse, category))
            {
                cat.ResponseData.Add(new DtoMetric() {CategoryName= category,InstanceName= instanse, CounterName = item });
            }
            return Ok(cat);
        }

        [HttpPut("SetOnOffCounter")]
        public IActionResult UpdateCountersStatus([FromQuery] string category, [FromQuery] string instanse, [FromQuery] string counter, [FromQuery] bool DoOrNot)
        {
            string summary = string.Empty;
            try
            {
                PerformanceCounter _cpuCounter = new PerformanceCounter(category, counter, instanse);
                JobWorker.SetOnOff(_cpuCounter, DoOrNot);
            }
            catch (Exception ex)
            {
                summary = ex.Message;
            }
            return Ok(summary);
        }

        //[HttpGet("cluster/from/{fromTime}/to/{toTime}")]
        //public IActionResult GetCpuMetricsFromAllCluster
        //        (
        //        [FromRoute] TimeSpan fromTime,
        //        [FromRoute] TimeSpan toTime
        //        )
        //{
        //    return Ok(_cpuControllerBaseWorker.GetAllmetricToTime(fromTime, toTime));
        //}
    }


}


