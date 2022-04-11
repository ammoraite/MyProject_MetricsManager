using Dapper;
using MetricsMeneger.DAL.BaseModuls;
using MetricsMeneger.Handlers;
using MetricsMeneger.Interfaces;
using System.Collections.Generic;

namespace MetricsMeneger.Services.Repositories
{

    public class SqlLiteMetricsRepository : IRepositoryMetrics
    {
        private const string metricRepConnectionString = @"Data Source=MenegerHardWareMetrics.db;Version=3;";

        private readonly RepositoryMetricWorker _metricRepositoryWorker;
        public SqlLiteMetricsRepository()
        {
            SqlMapper.AddTypeHandler(new TimeSpanHandler());
            _metricRepositoryWorker = new RepositoryMetricWorker(metricRepConnectionString);
        }
        public IList<Metric> GetAll()
        {
            return _metricRepositoryWorker.GetAll();
        }
        public void Delete(int id)
        {
            _metricRepositoryWorker.Delete(id);
        }
        public void Update(Metric item)
        {
            _metricRepositoryWorker.Update(item);
        }
        public Metric GetById(int id)
        {
            return _metricRepositoryWorker.GetById(id);
        }

        public void Create(Metric item)
        {
            _metricRepositoryWorker.Create(item);
        }
    }
}
