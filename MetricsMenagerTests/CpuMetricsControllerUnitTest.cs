using First_API.Controllers;
using First_API.DAL.BaseModuls;
using First_API.Interfaces;
using First_API.Services.Repositories;
using Moq;
using System;
using Xunit;
namespace MetricsAgentTests
{
    public class CpuMetricsControllerUnitTests
    {
        private CpuMetricsController controller;
           
        private Mock<ICpuMetricsRepository<Metric>> mock;
        public CpuMetricsControllerUnitTests()
        {
            mock = new Mock<IRepositoryMetrics<IMetric>>();
            controller = new CpuMetricsController(mock.Object);
        }
        [Fact]
        public void Create_ShouldCall_Create_From_Repository()
        {
            // ������������� �������� ��������
            // � �������� �����������, ��� � ����������� ��������
            CpuMetric - ������
            mock.Setup(repository =>
                repository.Create(It.IsAny<CpuMetric>())).Verifiable();
            // ��������� �������� �� �����������
            var result = controller.Create(new
                MetricsAgent.Requests.CpuMetricCreateRequest
                {
                    Time = TimeSpan.FromSeconds(1),
                    Value = 50
                });
            // ��������� �������� �� ��, ��� ���� ������� ����������
            // �������� ����� Create ����������� � ������ ����� ������� �
            ���������
            mock.Verify(repository => repository.Create(It.IsAny<CpuMetric>()),
                Times.AtMostOnce());
        }
    }
}