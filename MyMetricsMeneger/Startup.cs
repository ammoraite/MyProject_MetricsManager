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
        /// ���� ����� ���������� ������ ����������. ���������� ��� ��� ���������� ����� � ���������
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
            ////        // ��������� ��������� SQLite
            ////        .AddSQLite()
            ////        // ������������� ������ �����������
            ////        .WithGlobalConnectionString(ConnectionString)
            ////        // ������������, ��� ������ ������ � ����������
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


            //��������� ������ 5 ������


            #endregion





        }

        /// <summary>
        /// ���� ����� ���������� ������ ����������.���������� ��� ��� ��������� ��������� HTTP-��������
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
                // ��������� ��������
                //migrationRunner.MigrateUp();
            }
    }
}