using AutoMapper;
using First_API.Controllers.MetricControllers.Base;
using MetricsMeneger.Controllers.MetricControllers;
using MetricsMeneger.DAL.BaseModuls;
using MetricsMeneger.DAL.Modules;
using MetricsMeneger.DTO.Requests;
using MetricsMeneger.Responses;
using MetricsMeneger.Services.Jobs;
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
        IRepositoryMetrics _repository;
        private readonly ILogger<MetricsController> _cpuLogger;
        private readonly ControllerBaseWorker _controllerBaseWorker;

        public MetricsController(IRepositoryMetrics repository,
                                    ILogger<MetricsController> logger,
                                    IMapper mapper)
        {
            _repository=repository;
            _controllerBaseWorker = new ControllerBaseWorker(_repository, mapper, logger);
            _cpuLogger = logger;
            logger.LogDebug(1, $"NLog встроен в CpuMetricController");
        }      
        [HttpGet("GetallCategory")]
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

        [HttpGet("GetallInstanse")]
        public IActionResult GetAllInstanses([FromQuery] string category)
        {
            _cpuLogger.LogDebug(1, $"Отправлены все Instanses из категории \"{category}\"");
            Response cat = new Response() { ResponseData = new List<DtoMetric>() };
            foreach (var item in GetInfo.GetInstansesMetric(category))
            {
                cat.ResponseData.Add(new DtoMetric() { InstanceName = item });
            }
            return Ok(cat);
        }

        [HttpGet("GetallCounters")]
        public IActionResult GetAllCounters([FromQuery] string category, [FromQuery] string instanse)
        {
            _cpuLogger.LogDebug(1, $"Отправлены все Counters связанные с категорией: \"{category}\" и instanse: \"{instanse}\"");

            Response cat = new Response() { ResponseData = new List<DtoMetric>() };


            foreach (var item in GetInfo.GetCounters(instanse, category))
            {
                cat.ResponseData.Add(new DtoMetric() {CategoryName= category,InstanceName= instanse, CounterName = item });
            }
            return Ok(cat);
        }

        [HttpPut("СonfiguringMetricsCollector")]
        public IActionResult UpdateCountersStatus([FromQuery] string category, [FromQuery] string instanse, 
                                                  [FromQuery] string counter , [FromQuery] bool DoOrNot)
        {
            string summary = string.Empty;
            try
            {
                
                SettingnsItem _cpuCounter = new SettingnsItem(category, instanse, counter, DoOrNot);
                 
                JobWorker.SetOnOff(_cpuCounter);

                if (DoOrNot)
                {
                    _cpuLogger.LogDebug(1, $"Включен сбор метрики ( категория: \"{category}\"  instanse: \"{instanse}\" counter: \"{counter}\"");
                }
                else 
                {
                    _cpuLogger.LogDebug(1, $"Выключен сбор метрики ( категория: \"{category}\"  instanse: \"{instanse}\" counter: \"{counter}\"");
                }
            }
            catch (Exception ex)
            {
                summary = ex.Message;
            }
            return Ok(summary);
        }
    }
}


