﻿using CashFlowTracker.Application.Commands;
using CashFlowTracker.Application.Interfaces.Repository;
using CashFlowTracker.Application.Messages;
using CashFlowTracker.Domain.Entities;
using CashFlowTracker.Infra.RabbitAdapter;
using MediatR;

namespace CashFlowTracker.Application.Handlers
{
    public class CreateTransactionCommandHandler : IRequestHandler<CreateTransactionCommand, TransactionResult>
    {
        private readonly RabbitMQProducer _producer;

        public CreateTransactionCommandHandler(RabbitMQProducer producer)
        {
            _producer = producer;
        }

        public async Task<TransactionResult> Handle(CreateTransactionCommand request, CancellationToken cancellationToken)
        {
            try
            {
                // Criar a mensagem com os dados da transação
                var message = new TransactionCreatedMessage
                {
                    AccountId = request.AccountId,
                    Amount = request.Amount,
                    Type = request.Type,
                    TransactionDate = request.Date,
                    Description = request.Description
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
