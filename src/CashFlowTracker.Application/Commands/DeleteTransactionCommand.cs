using MediatR;

namespace CashFlowTracker.Application.Commands
{
    public class DeleteTransactionCommand : IRequest<TransactionResult>
    {
        public Guid Id { get; set; }
    }
}
