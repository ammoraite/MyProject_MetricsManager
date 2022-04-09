//using Dapper;
//using MyMetricsMeneger.DAL.BaseModuls;
//using Microsoft.Data.Sqlite;
//using System;
//using System.Collections.Generic;
//using System.Data.SQLite;
//using System.Diagnostics;
//using System.Linq;

//namespace MyMetricsMeneger.Services.Repositories
//{
//    public class RepositoryMetricWorker
//    {
//        private readonly string _connectionString;
//        private readonly string _nameTable;
//        public List<PerformanceCounter> _performanceCounters { get; private set; }
//        public RepositoryMetricWorker(string ConnectionString, string v)
//        {
//            _connectionString = ConnectionString;
//            _nameTable = v;
//            _performanceCounters = SetAllPerformanceCounter();
//            SetAllInSql(_performanceCounters);
//        }

//        private void InsertItem(MetricInSQLbase item)
//        {
//            using var connection = new SQLiteConnection(_connectionString);
//            connection.Open();
//            using var cmd = new SQLiteCommand(connection);
//            connection.Execute($"INSERT INTO {_nameTable} (catrgoryName, instanseName,counterName,Frequencyofmetricacollection,Do)" +
//                $" VALUES(@catrgoryName, @instanseName, @counterName, @Frequencyofmetricacollection, @Do);",

//                new
//                {
//                    catrgoryName = item.catrgoryName,
//                    instanseName = item.instanseName,
//                    counterName = item.counterName,
//                    Frequencyofmetricacollection = item.frequencyOfMetricaCollection,
//                    Do = item.Do
//                });
//        }
//        public List<MetricInSQLbase> GetAllMetricInSQLbase()
//        {
//            using (var connection = new SQLiteConnection(_connectionString))
//            {
//                // Читаем, используя Query, и в шаблон подставляем тип данных,
//                // объект которого Dapper, он сам заполнит его поля
//                // в соответствии с названиями колонок
//                return connection.Query<MetricInSQLbase>($"SELECT catrgoryName, instanseName,counterName,Frequencyofmetricacollection,Do FROM {_nameTable};").ToList();
//            }
//        }
//        public void SetAllInSql(List<PerformanceCounter> performanceCounters)
//        {
//            foreach (var item in performanceCounters)
//            {
//                try
//                {
//                        InsertItem(new MetricInSQLbase()
//                        {
//                            catrgoryName = item.CategoryName,
//                            instanseName = item.InstanceName,
//                            counterName = item.CounterName,
//                            frequencyOfMetricaCollection = 5,
//                            Do = true
//                        });      
//                }
//                catch (Exception)
//                {
//                    continue;
//                }
//            }

//        }
//        public List<PerformanceCounter> GetAllPerformanceCounterWhatDo()
//        {
//            List<PerformanceCounter> counters = new();

//            using (var connection = new SqliteConnection("Data Source=HardwareMetricinthisPC.db"))
//            {
                
//                connection.Open();
//                SqliteCommand command = new SqliteCommand();
//                command.Connection = connection;
//                command.CommandText = @$"SELECT * FROM {_nameTable} WHERE Do=@Do";
//                SqliteDataReader sqliteDataReader= command.ExecuteReader();


//                //SQLiteCommand Command = new SQLiteCommand
//                //    {
//                //        Connection = connection,
//                //        CommandText = @$"SELECT * FROM {_nameTable} WHERE Do=@Do"
//                //    };


//                while (sqliteDataReader.Read())
//                {
//                    MetricInSQLbase _dbImageByte = (MetricInSQLbase)sqliteDataReader["Do"];
//                    counters.Add(new PerformanceCounter
//                    {
//                        CategoryName = _dbImageByte.catrgoryName,
//                        CounterName = _dbImageByte.counterName,
//                        InstanceName = _dbImageByte.instanseName,
//                    });
//                }
//            }
            
//            return counters;
//        }
//        public List<PerformanceCounter> SetAllPerformanceCounter()
//        {
//            List<PerformanceCounter> counters = new();

//            foreach (var item in PerformanceCounterCategory.GetCategories())
//            {
                
//                    var a = item.GetInstanceNames();
//                    try
//                    {
//                        for (int i = 0; i < 1; i++)
//                        {
//                            foreach (var item2 in item.GetCounters(a[i]))
//                            {
//                                if (a[i] == "_Total" || a[i] == "_Total_" || a[i] == "Global")
//                                {
//                                    PerformanceCounter counter = new PerformanceCounter();
//                                    counter = item2;
//                                    counter.InstanceName = a[i];
//                                    counters.Add(counter);
//                                }

//                            }
//                        }
//                    }
//                    catch (Exception)
//                    {
//                        continue;
//                    } 

//            }
//            return counters;
//        }
//        public List<PerformanceCounter> SetFecurience(int freq, int id)
//        {
//            using (var connection = new SQLiteConnection(_connectionString))
//            {
//                connection.Execute($"UPDATE {_nameTable} SET Frequencyofmetricacollection" +
//                    $" = @Frequencyofmetricacollection WHERE id = @id",
//                    new
//                    {
//                        Frequencyofmetricacollection = freq,
//                    });
//            }
//            return GetAllPerformanceCounterWhatDo();
//        }
//        public List<PerformanceCounter> SetDoit(bool Do, int id)
//        {
//            using (var connection = new SQLiteConnection(_connectionString))
//            {
//                connection.Execute($"UPDATE {_nameTable} SET Do = @Do WHERE id = @id",
//                    new
//                    {
//                        Do = Do,
//                    });
//            }

//            return GetAllPerformanceCounterWhatDo();
//        }
//    }
//}
