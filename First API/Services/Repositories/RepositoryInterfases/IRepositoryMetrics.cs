using MetricsMeneger.DAL.BaseModuls;
using MetricsMeneger.Interfaces;
using System.Collections.Generic;


namespace MetricsMeneger.Services.Repositories
{
    public interface IRepositoryMetrics
    {
        IList<Metric> GetAll(string _nameTable);
        Metric GetById(int id, string _nameTable);
        void Create(Metric item);
        void Update(Metric item);
        void Delete(int id, string _nameTable);
        IList<string> GetAllCatecoriesInBaseData();
    }
}