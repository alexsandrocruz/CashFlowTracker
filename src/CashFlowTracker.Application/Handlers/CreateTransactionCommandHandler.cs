using CashFlowTracker.Application.Commands;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashFlowTracker.Application.Handlers
{
    public class CreateTransactionCommandHandler : IRequestHandler<CreateTransactionCommand, TransactionResult>
    {
        public async Task<TransactionResult> Handle(CreateTransactionCommand request, CancellationToken cancellationToken)
        {
            try
            {
                // Aqui você implementaria a lógica para criar a transação
                // Isso pode incluir validações, manipulação de dados e persistência

                // Simulando a criação de uma transação
                var createdTransactionId = Guid.NewGuid(); // Simulação do ID da transação criada

                return new TransactionResult { Success = true, Message = "Transação criada com sucesso.", TransactionId = createdTransactionId };
            }
            catch (Exception ex)
            {
                // Tratamento de exceções
                return new TransactionResult { Success = false, Message = ex.Message };
            }
        }
    }
}
