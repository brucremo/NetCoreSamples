using Moq;
using Xunit;

namespace NetCoreSamples.Worker.Lib.Tests
{
    public class ServiceWorkerTests
    {
        private readonly Mock<IWorker> mockWorker;
        private readonly ServiceWorker serviceWorker;
        private readonly CancellationToken cancellationToken;

        public ServiceWorkerTests()
        {
            mockWorker = new Mock<IWorker>();
            serviceWorker = new ServiceWorker(mockWorker.Object);
            cancellationToken = new CancellationToken();
        }

        [Fact]
        public async Task StartAsync_CallsRunOnWorker()
        {
            // Act
            await serviceWorker.StartAsync(cancellationToken);

            // Assert
            mockWorker.Verify(w => w.Run(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task StartAsync_HandlesCancellation()
        {
            // Arrange
            var cancellationTokenSource = new CancellationTokenSource();
            var cancellationToken = cancellationTokenSource.Token;

            // Act
            cancellationTokenSource.Cancel();
            await serviceWorker.StartAsync(cancellationToken);

            // Assert
            mockWorker.Verify(w => w.Run(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task StartAsync_RethrowsExceptionThrownByWorkerRun()
        {
            // Arrange
            var expectedException = new Exception("Test exception");
            mockWorker.Setup(w => w.Run(It.IsAny<CancellationToken>()))
                      .Throws(expectedException);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<Exception>(() => serviceWorker.StartAsync(cancellationToken));
            Assert.Equal(expectedException, exception);
        }
    }
}

