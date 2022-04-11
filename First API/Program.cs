using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog.Web;
using System;

namespace MetricsMeneger
{
    public class Program
    {
        public static void Main(string[] args)
        {

            var logger = NLogBuilder.ConfigureNLog("nlog.config.xml").GetCurrentClassLogger();

            try
            {
                logger.Debug("init main");
                CreateHostBuilder(args).Build().Run();
            }
            // Отлов всех исключений в рамках работы приложения
            catch (Exception exception)
            {
                //NLog: устанавливаем отлов исключений
                logger.Error(exception, "Stopped program because of exception");
                throw;
            }
            finally
            {
                // Остановка логгера
                NLog.LogManager.Shutdown();
            }
        }
        public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args).ConfigureWebHostDefaults(webBuilder => {
            webBuilder.UseStartup<Startup>();
        }).ConfigureLogging(logging =>
        {
            logging.ClearProviders(); // Создание провайдеров логирования
            logging.SetMinimumLevel(LogLevel.Trace); // Устанавливаем минимальный уровень логирования
        }).UseNLog(); // Добавляем библиотеку nlog
    }
}
