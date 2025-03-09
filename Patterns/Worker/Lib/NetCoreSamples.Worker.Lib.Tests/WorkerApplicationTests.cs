using Microsoft.Extensions.Configuration.CommandLine;
using Microsoft.Extensions.Configuration.EnvironmentVariables;
using Microsoft.Extensions.Hosting;
using Moq;
using Xunit;

namespace NetCoreSamples.Worker.Lib.Tests
{
    public class WorkerApplicationTests
    {
        [Fact]
        public async Task Run_CallsRunOnIWorker()
        {
            // Arrange
            var mockWorker = new Mock<IWorker>();
            var mockServiceProvider = new Mock<IServiceProvider>();
            mockServiceProvider
                .Setup(sp => sp.GetService(typeof(IWorker)))
                .Returns(mockWorker.Object);
            var workerApplication = new WorkerApplication(mockServiceProvider.Object);

            // Act
            await workerApplication.Run();

            // Assert
            mockWorker.Verify(w => w.Run(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Run_ThrowsInvalidOperationException_WhenGetServiceReturnsNull()
        {
            // Arrange
            var mockServiceProvider = new Mock<IServiceProvider>();
            mockServiceProvider
                .Setup(sp => sp.GetService(typeof(IWorker)))
                .Returns(null!);
            var workerApplication = new WorkerApplication(mockServiceProvider.Object);

            // Act & Assert
            await Assert.ThrowsAsync<InvalidOperationException>(() => workerApplication.Run());
        }

        [Fact]
        public async Task Run_RethrowsException_ThrownByIWorkerRun()
        {
            // Arrange
            var expected = new Exception("Test exception");
            var mockWorker = new Mock<IWorker>();
            mockWorker
                .Setup(w => w.Run(It.IsAny<CancellationToken>()))
                .Throws(expected);
            var mockServiceProvider = new Mock<IServiceProvider>();
            mockServiceProvider
                .Setup(sp => sp.GetService(typeof(IWorker)))
                .Returns(mockWorker.Object);
            var workerApplication = new WorkerApplication(mockServiceProvider.Object);

            // Act & Assert
            var result = await Assert.ThrowsAsync<Exception>(() => workerApplication.Run());
            Assert.Same(expected, result);
        }

        [Fact]
        public async Task Run_CallsHostRun_WhenHostAppIsNotNull()
        {
            // Arrange
            var cts = new CancellationTokenSource(TimeSpan.FromSeconds(1));

            var mockHostApplicationLifetime = new Mock<IHostApplicationLifetime>();
            mockHostApplicationLifetime.Setup(h => h.ApplicationStopping)
                .Returns(cts.Token);

            var mockServiceProvider = new Mock<IServiceProvider>();
            mockServiceProvider.Setup(sp => sp.GetService(typeof(IHostApplicationLifetime)))
                .Returns(mockHostApplicationLifetime.Object);

            var mockHost = new Mock<IHost>();
            mockHost.Setup(h => h.StartAsync(It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);
            mockHost.Setup(h => h.Services)
                .Returns(mockServiceProvider.Object);

            var workerApplication = new WorkerApplication(mockServiceProvider.Object, mockHost.Object);

            // Act
            await workerApplication.Run();

            // Assert
            mockHost.Verify(h => h.StartAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public void CreateBuilder_AddsEnvironmentVariables()
        {
            // Act
            var builder = WorkerApplication.CreateBuilder();

            // Assert
            Assert.NotNull(builder);
            Assert.Contains(builder.Configuration.Sources, p => p is EnvironmentVariablesConfigurationSource);
        }

        [Fact]
        public void CreateBuilder_WithArgs_AddsCommandLineAndEnvironmentVariables()
        {
            // Arrange
            var args = new[] { "--worker", "TestWorker" };

            // Act
            var builder = WorkerApplication.CreateBuilder<WorkerOptions>(args);

            // Assert
            Assert.NotNull(builder);
            Assert.Contains(builder.Configuration.Sources, p => p is CommandLineConfigurationSource);
            Assert.Contains(builder.Configuration.Sources, p => p is EnvironmentVariablesConfigurationSource);
        }

        [Fact]
        public void CreateServiceBuilder_WithArgs_AddsCommandLineEnvironmentVariablesAndHostedService()
        {
            // Arrange
            var args = new[] { "--worker", "TestWorker" };

            // Act
            var builder = WorkerApplication.CreateServiceBuilder<WorkerOptions>(args);

            // Assert
            Assert.NotNull(builder);
            Assert.Contains(builder.Configuration.Sources, p => p is CommandLineConfigurationSource);
            Assert.Contains(builder.Configuration.Sources, p => p is EnvironmentVariablesConfigurationSource);
            Assert.Contains(builder.Services, s => s.ServiceType == typeof(IHostedService) && s.ImplementationType == typeof(ServiceWorker));
        }
    }
}
