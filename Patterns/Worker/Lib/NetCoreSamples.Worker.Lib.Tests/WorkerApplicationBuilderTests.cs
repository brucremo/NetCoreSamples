using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Moq;
using System.Reflection;
using Xunit;

namespace NetCoreSamples.Worker.Lib.Tests
{
    public class WorkerApplicationBuilderTests
    {
        readonly WorkerApplicationBuilder builder;

        readonly string validWorkerName = "MockWorker";

        readonly Mock<IConfigurationManager> mockConfiguration = new Mock<IConfigurationManager>();

        public WorkerApplicationBuilderTests()
        {
            mockConfiguration = new Mock<IConfigurationManager>();

            var mockSection = new Mock<IConfigurationSection>();
            mockSection.Setup(s => s["Name"])
                .Returns(validWorkerName);
            mockSection.Setup(s => s["ServiceDelayInSeconds"])
                .Returns("1");

            mockConfiguration
                .Setup(c => c.GetSection(nameof(WorkerOptions)))
                .Returns(mockSection.Object);

            mockConfiguration
                .Setup(c => c["WorkerOptions:Name"])
                .Returns(validWorkerName);

            builder = new WorkerApplicationBuilder
            {
                Configuration = mockConfiguration.Object
            }.WithCallingAssembly();
        }

        [Fact]
        public void Build_AddsTransientServiceOfWorkerType()
        {
            // Arrange
            builder.ConfigureWorker(validWorkerName, (services, configuration) => { });

            // Act
            var application = builder.Build();

            // Assert
            var serviceDescriptor = builder.Services.Single(sd => sd.ServiceType == typeof(IWorker));
            Assert.NotNull(serviceDescriptor);
            Assert.Equal(ServiceLifetime.Transient, serviceDescriptor.Lifetime);
        }

        [Theory]
        [InlineData(null, typeof(InvalidOperationException))]
        [InlineData("NonExistentWorker", typeof(InvalidOperationException))]
        [InlineData("   ", typeof(InvalidOperationException))]
        [InlineData("", typeof(KeyNotFoundException))]
        public void Build_ThrowsException_WhenWorkerNameIsInvalid(string? workerName, Type exceptionType)
        {
            // Arrange
            mockConfiguration
                .Setup(c => c["WorkerOptions:Name"])
                .Returns(workerName);

            builder.ConfigureWorker(validWorkerName, (services, configuration) => { });

            // Act & Assert
            var exception = Assert.ThrowsAny<Exception>(() => builder.Build());
            Assert.IsType(exceptionType, exception);
        }

        [Fact]
        public void WithAssembly_SetsAssemblyProperty()
        {
            // Arrange
            var assembly = Assembly.GetExecutingAssembly();

            // Act
            builder.WithAssembly(assembly);

            // Assert
            Assert.Equal(assembly, builder.Assembly);
        }

        [Fact]
        public void WithCallingAssembly_SetsAssemblyProperty()
        {
            // Act
            builder.WithCallingAssembly();

            // Assert
            Assert.Equal(GetType().Assembly, builder.Assembly);
        }

        [Fact]
        public void UseConfiguredWorkerType_RegistersCorrectWorkerType()
        {
            // Arrange
            var workerTypeName = "MockWorker";
            builder.WithCallingAssembly();

            // Act
            builder.UseConfiguredWorkerType(workerTypeName);
            builder.ConfigureWorker(validWorkerName, (services, configuration) => { });
            var application = builder.Build();

            // Assert
            var service = builder.Services.BuildServiceProvider().GetService<IWorker>();
            Assert.NotNull(service);
            Assert.Equal(workerTypeName, service.GetType().Name);
        }
    }
}
