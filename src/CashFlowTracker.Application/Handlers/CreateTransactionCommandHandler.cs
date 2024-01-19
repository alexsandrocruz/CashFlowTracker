using CashFlowTracker.Application.Commands;
using CashFlowTracker.Application.Interfaces.Repository;
using CashFlowTracker.Domain.Entities;
using MediatR;

namespace CashFlowTracker.Application.Handlers
{
    public class CreateTransactionCommandHandler : IRequestHandler<CreateTransactionCommand, TransactionResult>
    {
        private readonly ITransactionRepository _transactionRepository;

        public CreateTransactionCommandHandler(ITransactionRepository transactionRepository)  
        {
            _transactionRepository = transactionRepository;
        }

        public async Task<TransactionResult> Handle(CreateTransactionCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var transaction = new Transaction
                {
                    AccountId = request.AccountId,
                    Amount = request.Amount,
                    Type = request.Type,
                    TransactionDate = request.Date,
                    Description = request.Description,
                };

                await _transactionRepository.AddAsync(transaction);
                await _transactionRepository.SaveChangesAsync();

                return new TransactionResult { Success = true, Message = "Transação criada com sucesso.", TransactionId = transaction.Id };
            }
            catch (Exception ex)
            {
                return new TransactionResult { Success = false, Message = ex.Message };
            }
        }
    }
}
