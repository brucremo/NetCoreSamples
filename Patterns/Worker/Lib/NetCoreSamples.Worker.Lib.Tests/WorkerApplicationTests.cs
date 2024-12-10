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
            mockWorker.Verify(w => w.Run(), Times.Once);
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
                .Setup(w => w.Run())
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
    }
}
