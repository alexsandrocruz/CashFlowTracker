using CashFlowTracker.Application.Commands;
using CashFlowTracker.Application.Messages;
using CashFlowTracker.Infra.RabbitAdapter;
using MediatR;

namespace CashFlowTracker.Application.Handlers
{
    public class StartConsolidationCommandHandler : IRequestHandler<StartConsolidationCommand, ConsolidationResult>
    {
        private readonly RabbitMQProducer _producer;

        public StartConsolidationCommandHandler(RabbitMQProducer producer)
        {
            _producer = producer;
        }

        public async Task<ConsolidationResult> Handle(StartConsolidationCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var message = new ConsolidationStartedMessage
                {
                    AccountId = request.AccountId,
                    Date = request.Date
                };

                // Publicar a mensagem no RabbitMQ
                _producer.Publish(message);

                return new ConsolidationResult { Success = true, Message = "Pedido de consolidação enviado para processamento." };
            }
            catch (Exception ex)
            {
                return new ConsolidationResult { Success = false, Message = ex.Message };
            }
        }
    }
}
