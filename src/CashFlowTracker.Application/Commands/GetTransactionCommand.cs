using MediatR;

namespace CashFlowTracker.Application.Commands
{
    public class GetTransactionCommand : IRequest<TransactionResult>
    {
        public Guid? Id { get; set; }
    }

}
