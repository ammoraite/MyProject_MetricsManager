using MetricsMeneger.Interfaces;
using System.Collections.Generic;


namespace MetricsMeneger.Services.Repositories
{
    public interface IRepositoryMetrics<T> where T : IMetric
    {
        IList<T> GetAll();
        T GetById(int id);
        void Create(T item);
        void Update(T item);
        void Delete(int id);
    }
}