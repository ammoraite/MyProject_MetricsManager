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
using Microsoft.OpenApi.Models;
using Quartz;
using Quartz.Impl;
using Quartz.Spi;
using System;

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
        /// ���� ����� ���������� ������ ����������. ���������� ��� ��� ���������� ����� � ���������
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
                    // ��������� ��������� SQLite
                    .AddSQLite()
                    // ������������� ������ �����������
                    .WithGlobalConnectionString(AgentDataBaseConnectionString)
                    // ������������, ��� ������ ������ � ����������
                    .ScanIn(typeof(Startup).Assembly).For.Migrations()
                ).AddLogging(alb => alb
                    .AddFluentMigratorConsole());


            #endregion

            #region HttpClient

            //services.AddHttpClient<IMetricsAgentClient, MetricsAgentClient>().AddTransientHttpErrorPolicy(p =>
            //p.WaitAndRetryAsync(3, _ => TimeSpan.FromMilliseconds(1000)));

            #endregion

            #region Swagger

            services.AddSwaggerGen();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "API ������� ������ ����� ������",
                    Description = "����� ����� �������� � api ������ �������",
                    TermsOfService = new Uri("https://example.com/terms"),
                    Contact = new OpenApiContact
                    {
                        Name = "Kadyrov",
                        Email = string.Empty,
                        Url = new Uri("https://kremlin.ru"),
                    },
                    License = new OpenApiLicense
                    {
                        Name = "����� �������, ��� ����� ��������� �� ������������",
                        Url = new Uri("https://example.com/license"),
                    }
                });
            });


            #endregion

            #region Jobs



            services.AddSingleton<IJobFactory, SingletonJobFactory>();
            services.AddSingleton<ISchedulerFactory, StdSchedulerFactory>();
            services.AddHostedService<QuartzHostedService>();

            services.AddSingleton<MetricJob>();
            services.AddSingleton(new JobSchedule(
                jobType: typeof(MetricJob), cronExpression:
                 "0/5 * * * * ?")); // ��������� ������ 5 ������

            #endregion

        }

        /// <summary>
        /// ���� ����� ���������� ������ ����������.���������� ��� ��� ��������� ��������� HTTP-��������
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
            // ��������� ��������
            migrationRunner.MigrateUp();

            // ��������� middleware � �������� ��� ��������� Swagger-��������.
            app.UseSwagger();
            // ��������� middleware ��� ��������� swagger-ui
            // ��������� �������� Swagger JSON (���� ���������� �� ���������������
            // �������������,
            // �� ������� ����� �������� UI).
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "API ������� ������ ����� ������");
            });

        }
    }
}