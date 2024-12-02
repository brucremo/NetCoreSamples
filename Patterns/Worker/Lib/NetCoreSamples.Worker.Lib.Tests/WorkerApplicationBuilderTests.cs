using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using NetCoreSamples.Worker.Lib.Tests.MockClasses;
using Xunit;

namespace NetCoreSamples.Worker.Lib.Tests
{
    public class WorkerApplicationBuilderTests
    {
        [Fact]
        public void BuildWithNamedWorkers_AddsTransientServiceOfWorkerType()
        {
            // Arrange
            var workerName = "MockWorker";
            var mockConfiguration = new Mock<IConfigurationManager>();
            mockConfiguration
                .Setup(c => c["WorkerOptions:Name"])
                .Returns(workerName);
            var builder = new WorkerApplicationBuilder
            {
                Configuration = mockConfiguration.Object,
                Assembly = typeof(MockWorker).Assembly
            };
            builder.ConfigureWorker(workerName, (services, configuration) => { });

            // Act
            var application = builder.BuildWithNamedWorkers();

            // Assert
            var serviceDescriptor = builder.Services.Single(sd => sd.ServiceType == typeof(IWorker));
            Assert.NotNull(serviceDescriptor);
            Assert.Equal(ServiceLifetime.Transient, serviceDescriptor.Lifetime);
        }

        [Fact]
        public void BuildWithNamedWorkers_ThrowsException_WhenWorkerNameIsNull()
        {
            // Arrange
            var mockConfiguration = new Mock<IConfigurationManager>();
            mockConfiguration
                .Setup(c => c["WorkerOptions:Name"])
                .Returns((string)null!);
            var builder = new WorkerApplicationBuilder
            {
                Configuration = mockConfiguration.Object,
                Assembly = typeof(MockWorker).Assembly
            };

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => builder.BuildWithNamedWorkers());
        }

        [Fact]
        public void BuildWithNamedWorkers_ThrowsException_WhenWorkerTypeNotFound()
        {
            // Arrange
            var workerName = "NonExistentWorker";
            var mockConfiguration = new Mock<IConfigurationManager>();
            mockConfiguration
                .Setup(c => c["WorkerOptions:Name"])
                .Returns(workerName);
            var builder = new WorkerApplicationBuilder
            {
                Configuration = mockConfiguration.Object,
                Assembly = typeof(MockWorker).Assembly
            };

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => builder.BuildWithNamedWorkers());
        }

        [Fact]
        public void BuildWithNamedWorkers_ThrowsException_WhenWorkerNameIsEmpty()
        {
            // Arrange
            var mockConfiguration = new Mock<IConfigurationManager>();
            mockConfiguration
                .Setup(c => c["WorkerOptions:Name"])
                .Returns(string.Empty);
            var builder = new WorkerApplicationBuilder
            {
                Configuration = mockConfiguration.Object,
                Assembly = typeof(MockWorker).Assembly
            };

            // Act & Assert
            Assert.Throws<KeyNotFoundException>(() => builder.BuildWithNamedWorkers());
        }

        [Fact]
        public void BuildWithNamedWorkers_ThrowsException_WhenWorkerNameIsWhitespace()
        {
            // Arrange
            var mockConfiguration = new Mock<IConfigurationManager>();
            mockConfiguration
                .Setup(c => c["WorkerOptions:Name"])
                .Returns("   ");
            var builder = new WorkerApplicationBuilder
            {
                Configuration = mockConfiguration.Object,
                Assembly = typeof(MockWorker).Assembly
            };

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => builder.BuildWithNamedWorkers());
        }
    }
}
