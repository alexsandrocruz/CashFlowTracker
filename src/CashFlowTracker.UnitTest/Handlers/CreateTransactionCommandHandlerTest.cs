using CashFlowTracker.Application.Commands;
using CashFlowTracker.Application.Handlers;
using CashFlowTracker.Application.Messages;
using CashFlowTracker.Domain.Enums;
using CashFlowTracker.Infra.RabbitAdapter;
using Microsoft.Extensions.Configuration;
using Moq;

namespace CashFlowTracker.UnitTest.Handlers
{
    public class CreateTransactionCommandHandlerTests
    {
        [Fact]
        public async Task Handle_ValidCommand_ShouldPublishMessageAndReturnSuccess()
        {
            var mockProducer = new Mock<IRabbitMQProducer>();
            var command = new CreateTransactionCommand
            {
                AccountId = Guid.NewGuid(),
                Amount = 100,
                Type = TransactionType.Credit,
                Date = DateTime.Now,
                Description = "Test Transaction"
            };

            mockProducer.Setup(p => p.Publish(It.IsAny<TransactionCreatedMessage>())).Verifiable();

            var handler = new CreateTransactionCommandHandler(mockProducer.Object);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            mockProducer.Verify(p => p.Publish(It.IsAny<TransactionCreatedMessage>()), Times.Once);
            Assert.True(result.Success);
            Assert.Equal("Transação enviada para processamento.", result.Message);
        }

        [Fact]
        public async Task Handle_WhenProducerThrowsException_ShouldReturnFailure()
        {
            var mockProducer = new Mock<IRabbitMQProducer>();

            var command = new CreateTransactionCommand
            {
                AccountId = Guid.NewGuid(),
                Amount = 100,
                Type = TransactionType.Credit,
                Date = DateTime.Now,
                Description = "Test Transaction"
            };

            var expectedException = new InvalidOperationException("Test Exception");
            mockProducer.Setup(p => p.Publish(It.IsAny<TransactionCreatedMessage>()))
                .Throws(expectedException);

            var handler = new CreateTransactionCommandHandler(mockProducer.Object);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.False(result.Success);
            Assert.Equal(expectedException.Message, result.Message);
        }
    }
}