using MyMetricsMeneger.DTO.Jobs;
using MyMetricsMeneger.Services.Jobs;
using MetricsAgent.Jobs;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Quartz;
using Quartz.Impl;
using Quartz.Spi;

namespace MyMetricsMeneger
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        //private const string ConnectionString = @"Data Source=HardwareMetricinthisPC.db;Version=3;";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }


        ///<summary>
        /// Этот метод вызывается средой выполнения. Используем его для добавления служб в контейнер
        /// </summary>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHealthChecks();
            #region Controllers

            //services.AddControllers();

            #endregion

            #region Repositoryes

            //services.AddSingleton<IRepositorySqlMetrics, SqlLiteHardWareMetricsREpository>();

            #endregion

            #region Mapper



            #endregion

            #region SettingsMeneger

            services.AddSingleton<ISettingsMyMetricsMeneger,SettingsMyMetricsMeneger>();

            #endregion

            //#region Migrator

            ////services.AddFluentMigratorCore()
            ////    .ConfigureRunner(rb => rb
            ////        // Добавляем поддержку SQLite
            ////        .AddSQLite()
            ////        // Устанавливаем строку подключения
            ////        .WithGlobalConnectionString(ConnectionString)
            ////        // Подсказываем, где искать классы с миграциями
            ////        .ScanIn(typeof(Migrators).Assembly).For.Migrations()
            ////    ).AddLogging(lb => lb
            ////        .AddFluentMigratorConsole());

            //#endregion

            #region Jobs
            services.AddSingleton<IJobFactory, SingletonJobFactory>();
            services.AddSingleton<ISchedulerFactory, StdSchedulerFactory>();
            services.AddHostedService<QuartzHostedService>();
            services.AddSingleton<MetricJob>();
            services.AddSingleton(new JobSchedule(
                     jobType: typeof(MetricJob),
                     cronExpression: "0/5 * * * * ?"));


            //Запускать каждые 5 секунд


            #endregion





        }

        /// <summary>
        /// Этот метод вызывается средой выполнения.Используем его для настройки конвейера HTTP-запросов
        /// </summary>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
            {
                if (env.IsDevelopment())
                {
                    app.UseDeveloperExceptionPage();
                }
                app.UseRouting();
                //app.UseAuthorization();
                
                //app.UseEndpoints(endpoints => { endpoints.MapControllers();});
                // Запускаем миграции
                //migrationRunner.MigrateUp();
            }
    }
}