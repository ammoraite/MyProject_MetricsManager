﻿using First_API.Controllers.MetricControllers;
using MetricsMeneger.DAL.BaseModuls;
using MetricsMeneger.Interfaces;
using System.Collections.Generic;

namespace MetricsMeneger.Responses
{
    public class ResponseMetrics:IListMetricsResponse<DtoMetric>
    {
        public List<DtoMetric> ResponseData { get; set; }

    }

}
