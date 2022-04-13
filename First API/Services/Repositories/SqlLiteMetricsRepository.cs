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


        public IList<Metric> GetAll(string _nameTable)
        {
            return _metricRepositoryWorker.GetAll(_nameTable);
        }
        public void Delete(int id, string _nameTable)
        {
            _metricRepositoryWorker.Delete(id,_nameTable);
        }
        public void Update(Metric item)
        {
            _metricRepositoryWorker.Update(item);
        }
        public Metric GetById(int id, string _nameTable)
        {
            return _metricRepositoryWorker.GetById(id,_nameTable);
        }
        public void Create(Metric item)
        {
            _metricRepositoryWorker.Create(item);
        }
        public IList<string> GetAllCatecoriesInBaseData()
        {
            return _metricRepositoryWorker.GetAllTables();
        }
    }
}
