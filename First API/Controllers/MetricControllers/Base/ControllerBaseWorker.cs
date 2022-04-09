using AutoMapper;
using First_API.Controllers.MetricControllers;
using First_API.Controllers.MetricControllers.Base;
using MetricsMeneger.DAL.BaseModuls;
using MetricsMeneger.Interfaces;
using MetricsMeneger.Responses;
using MetricsMeneger.Services.Repositories;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace MetricsMeneger.Controllers.MetricControllers
{
    public class ControllerBaseWorker
    {
        private IRepositoryMetrics repository;
        private readonly IMapper mapper;



        public ControllerBaseWorker(
                IRepositoryMetrics repository,
                IMapper mapper,
                ILogger<IMetricController> logger)
        {
            this.repository = repository; ;
            this.mapper = mapper;
        }
        public void AddMetricFromRequest(IMetricCreateRequest request, Metric item)
        {
            item.Value = request.Value;
            item.Time = TimeSpan.FromSeconds(request.Time);
            repository.Create(item);
        }


        //public Response GetAllmetric()
        //{
        //    var response = new Response()
        //    {
        //        Metrics = new List<DtoMetric>()
        //    };
        //    foreach (var metric in repository.GetAll())
        //    {
        //        response.Metrics.Add(mapper.Map<DtoMetric>(metric));
        //    }
        //    return response;
        //}


        //internal Response GetAllmetricToTime(TimeSpan fromTime, TimeSpan toTime)
        //{
        //    var response = new Response()
        //    {
        //        Metrics = new List<DtoMetric>()
        //    };

        //    foreach (var metric in repository.GetAll())
        //    {
        //        if (metric.Time <= fromTime || metric.Time >= toTime)
        //        {
        //            response.Metrics.Add(mapper.Map<DtoMetric>(metric));
        //        }
        //    }
        //    return response;
        //}
    }
}
