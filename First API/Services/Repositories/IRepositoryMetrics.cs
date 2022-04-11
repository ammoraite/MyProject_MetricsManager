using MetricsMeneger.DAL.BaseModuls;
using MetricsMeneger.Interfaces;
using System.Collections.Generic;


namespace MetricsMeneger.Services.Repositories
{
    public interface IRepositoryMetrics
    {
        IList<Metric> GetAll();
        Metric GetById(int id);
        void Create(Metric item);
        void Update(Metric item);
        void Delete(int id);
    }
}