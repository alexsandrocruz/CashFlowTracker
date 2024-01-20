using CashFlowTracker.Application.Commands;
using CashFlowTracker.Application.Messages;
using CashFlowTracker.Infra.RabbitAdapter;
using MediatR;

namespace CashFlowTracker.Application.Handlers
{
    public class GetTransactionCommandHandler : IRequestHandler<DeleteTransactionCommand, TransactionResult>
    {
        private readonly IRabbitMQProducer _producer;

        public GetTransactionCommandHandler(IRabbitMQProducer producer)
        {
            _producer = producer;
        }

        public async Task<TransactionResult> Handle(DeleteTransactionCommand request, CancellationToken cancellationToken)
        {
            try
            {
                // Criar a mensagem com os dados da transação
                var message = new TransactionDeletedMessage
                {
                    Id = request.Id
                };

                // Publicar a mensagem no RabbitMQ
                _producer.Publish(message);

                return new TransactionResult { Success = true, Message = "Transação enviada para processamento." };
            }
            catch (Exception ex)
            {
                return new TransactionResult { Success = false, Message = ex.Message };
            }
        }
    }
}
