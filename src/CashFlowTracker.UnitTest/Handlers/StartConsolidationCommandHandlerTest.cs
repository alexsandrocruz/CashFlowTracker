using CashFlowTracker.Application.Commands;
using CashFlowTracker.Application.Handlers;
using CashFlowTracker.Application.Messages;
using CashFlowTracker.Domain.Enums;
using CashFlowTracker.Infra.RabbitAdapter;
using Moq;

namespace CashFlowTracker.UnitTest.Handlers
{
    public class StartConsolidationCommandHandlerTest
    {
        [Fact]
        public async Task Handle_ValidCommand_ShouldPublishMessageAndReturnSuccess()
        {
            var mockProducer = new Mock<IRabbitMQProducer>();
            var command = new StartConsolidationCommand
            {
                AccountId = Guid.NewGuid(),
                Date = DateTime.Now
            };

            mockProducer.Setup(p => p.Publish(It.IsAny<ConsolidationStartedMessage>())).Verifiable();

            var handler = new StartConsolidationCommandHandler(mockProducer.Object);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            mockProducer.Verify(p => p.Publish(It.IsAny<ConsolidationStartedMessage>()), Times.Once);
            Assert.True(result.Success);
            Assert.Equal("Pedido de consolidação enviado para processamento.", result.Message);
        }

        [Fact]
        public async Task Handle_WhenProducerThrowsException_ShouldReturnFailure()
        {
            var mockProducer = new Mock<IRabbitMQProducer>();

            var command = new StartConsolidationCommand
            {
                AccountId = Guid.NewGuid(),
                Date = DateTime.Now
            };

            var expectedException = new InvalidOperationException("Test Exception");
            mockProducer.Setup(p => p.Publish(It.IsAny<ConsolidationStartedMessage>()))
                .Throws(expectedException);

            var handler = new StartConsolidationCommandHandler(mockProducer.Object);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.False(result.Success);
            Assert.Equal(expectedException.Message, result.Message);
        }
    }
}