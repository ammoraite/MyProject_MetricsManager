using Dapper;
using MetricsMeneger.DAL.BaseModuls;
using MetricsMeneger.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Threading;

namespace MetricsMeneger.Services.Repositories
{
    public class RepositoryMetricWorker
    {
        private readonly string _connectionString;
        
        

        public RepositoryMetricWorker(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void Create(Metric item)
        {
                using var connection = new SQLiteConnection(_connectionString);
                connection.Open();
                // Создаём команду
                using var cmd = new SQLiteCommand(connection);
                 // Прописываем в команду SQL-запрос на вставку данных

                connection.Execute($"CREATE TABLE IF NOT EXISTS {item.CategoryName.Trim().Replace(" ", "")} " +
                    $"(id INTEGER PRIMARY KEY AUTOINCREMENT, CategoryName TEXT, InstanceName TEXT,CounterName TEXT, Value INTEGER, Time INTEGER)");

                connection.Execute($"INSERT INTO {item.CategoryName.Trim().Replace(" ", "")}" +
                    $" (CategoryName, InstanceName, CounterName, Value, Time) VALUES(@CategoryName, @InstanceName, @CounterName, @Value, @Time)",
                    // Анонимный объект с параметрами запроса
                    new
                    {
                        CategoryName = item.CategoryName,
                        InstanceName = item.InstanceName,
                        CounterName = item.CounterName,
                        Value = item.Value,
                        Time = item.Time.TotalSeconds
                    });
        }

        internal IList<string> GetAllTables()
        {
            using var connection = new SQLiteConnection(_connectionString);
            connection.Open();
            using var cmd = new SQLiteCommand(connection); 
            return connection.Query<string>("SELECT name FROM sqlite_master WHERE type='table' ORDER BY name;").ToList();           
        }

        public void Delete(int id,string _nameTable)
        {
            using (var connection = new SQLiteConnection(_connectionString))
            {
                connection.Execute($"DELETE FROM {_nameTable.Trim().Replace(" ", "")} WHERE id=@id",
                    new
                    {
                        id = id
                    });
            }
        }

        public IList<Metric> GetAll(string _nameTable)
        {
            using (var connection = new SQLiteConnection(_connectionString))
            {
                // Читаем, используя Query, и в шаблон подставляем тип данных,
                // объект которого Dapper, он сам заполнит его поля
                // в соответствии с названиями колонок
                return connection.Query<Metric>($"SELECT Id,Value,Time FROM {_nameTable.Trim().Replace(" ", "")};").ToList();
            }
        }

        public Metric GetById(int id,string _nameTable)
        {
            using (var connection = new SQLiteConnection(_connectionString))
            {
                return connection.QuerySingle<Metric>($"SELECT Id, Value, Time FROM {_nameTable.Trim().Replace(" ", "")} WHERE id = @id",
                    new { id = id });
            }
        }

        public void Update(Metric item)
        {
            using (var connection = new SQLiteConnection(_connectionString))
            {
                connection.Execute($"UPDATE {item.CategoryName.Trim().Replace(" ", "")} SET value = @value, time = @time WHERE id = @id",
                    new
                    {
                        value = item.Value,
                        time = item.Time.TotalSeconds,
                        id = item.Id
                    });
            }
        }
    }
}
