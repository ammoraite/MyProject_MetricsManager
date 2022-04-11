using AutoMapper;
using FluentMigrator.Runner;
using MetricsAgent.Jobs;
using MetricsMeneger.DTO.Jobs;
using MetricsMeneger.Mappers;
using MetricsMeneger.Services.Jobs;
using MetricsMeneger.Services.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Quartz;
using Quartz.Impl;
using Quartz.Spi;

namespace MetricsMeneger
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        private const string AgentDataBaseConnectionString = @"Data Source=MenegerHardWareMetrics.db;Version=3;";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }


        ///<summary>
        /// Этот метод вызывается средой выполнения. Используем его для добавления служб в контейнер
        /// </summary>
        public void ConfigureServices(IServiceCollection services)
        {
            #region Controllers

            services.AddControllers();

            #endregion

            #region Repositoryes

            services.AddSingleton<IRepositoryMetrics, SqlLiteMetricsRepository>();


            #endregion

            #region Mapper

            var mapperConfiguration = new MapperConfiguration(mp =>
                mp.AddProfile(new MapperProfile()));
            var mapper = mapperConfiguration.CreateMapper();
            services.AddSingleton(mapper);

            #endregion

            #region Migrator



            services.AddFluentMigratorCore()
                .ConfigureRunner(arb => arb
                    // Добавляем поддержку SQLite
                    .AddSQLite()
                    // Устанавливаем строку подключения
                    .WithGlobalConnectionString(AgentDataBaseConnectionString)
                    // Подсказываем, где искать классы с миграциями
                    .ScanIn(typeof(Startup).Assembly).For.Migrations()
                ).AddLogging(alb => alb
                    .AddFluentMigratorConsole());


            #endregion

            #region Jobs

            

            services.AddSingleton<IJobFactory, SingletonJobFactory>();
            services.AddSingleton<ISchedulerFactory, StdSchedulerFactory>();
            services.AddHostedService<QuartzHostedService>();

            services.AddSingleton<MetricJob>();
            services.AddSingleton(new JobSchedule(
                jobType: typeof(MetricJob),cronExpression: 
                 "0/5 * * * * ?")); // Запускать каждые 5 секунд
            #endregion

            #region HttpClient



            #endregion
        }


        /// <summary>
        /// Этот метод вызывается средой выполнения.Используем его для настройки конвейера HTTP-запросов
        /// </summary>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IMigrationRunner migrationRunner)//
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
            // Запускаем миграции
            migrationRunner.MigrateUp();
        }
    }
}