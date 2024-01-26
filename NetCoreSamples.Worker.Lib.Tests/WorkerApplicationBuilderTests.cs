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
        public void Build_AddsTransientServiceOfWorkerType()
        {
            // Arrange
            var workerName = "MockWorker";
            var mockConfiguration = new Mock<IConfigurationManager>();
            mockConfiguration
                .Setup(c => c["WorkerBaseOptions:Worker"])
                .Returns(workerName);
            var builder = new WorkerApplicationBuilder
            {
                Configuration = mockConfiguration.Object,
                WorkerAssembly = typeof(MockWorker).Assembly
            };
            builder.ConfigureWorker(workerName, (services, configuration) => { });

            // Act
            var application = builder.Build();

            // Assert
            var serviceDescriptor = builder.Services.Single(sd => sd.ServiceType == typeof(IWorker));
            Assert.NotNull(serviceDescriptor);
            Assert.Equal(ServiceLifetime.Transient, serviceDescriptor.Lifetime);
        }

        [Fact]
        public void Build_ThrowsException_WhenWorkerNameIsNull()
        {
            // Arrange
            var mockConfiguration = new Mock<IConfigurationManager>();
            mockConfiguration
                .Setup(c => c["WorkerBaseOptions:Worker"])
                .Returns((string)null);
            var builder = new WorkerApplicationBuilder
            {
                Configuration = mockConfiguration.Object,
                WorkerAssembly = typeof(MockWorker).Assembly
            };

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => builder.Build());
        }

        [Fact]
        public void Build_ThrowsException_WhenWorkerTypeNotFound()
        {
            // Arrange
            var workerName = "NonExistentWorker";
            var mockConfiguration = new Mock<IConfigurationManager>();
            mockConfiguration
                .Setup(c => c["WorkerBaseOptions:Worker"])
                .Returns(workerName);
            var builder = new WorkerApplicationBuilder
            {
                Configuration = mockConfiguration.Object,
                WorkerAssembly = typeof(MockWorker).Assembly
            };

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => builder.Build());
        }

        [Fact]
        public void Build_ThrowsException_WhenWorkerNameIsEmpty()
        {
            // Arrange
            var mockConfiguration = new Mock<IConfigurationManager>();
            mockConfiguration
                .Setup(c => c["WorkerBaseOptions:Worker"])
                .Returns(string.Empty);
            var builder = new WorkerApplicationBuilder
            {
                Configuration = mockConfiguration.Object,
                WorkerAssembly = typeof(MockWorker).Assembly
            };

            // Act & Assert
            Assert.Throws<KeyNotFoundException>(() => builder.Build());
        }

        [Fact]
        public void Build_ThrowsException_WhenWorkerNameIsWhitespace()
        {
            // Arrange
            var mockConfiguration = new Mock<IConfigurationManager>();
            mockConfiguration
                .Setup(c => c["WorkerBaseOptions:Worker"])
                .Returns("   ");
            var builder = new WorkerApplicationBuilder
            {
                Configuration = mockConfiguration.Object,
                WorkerAssembly = typeof(MockWorker).Assembly
            };

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => builder.Build());
        }

    }
}
