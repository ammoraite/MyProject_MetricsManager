using AutoMapper;
using MetricsMeneger.DAL.BaseModuls;


namespace MetricsMeneger.Mappers
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Metric, DtoMetric>();
            
        }
    }
}
