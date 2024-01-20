using CashFlowTracker.Domain.Enums;
using MediatR;

namespace CashFlowTracker.Application.Commands
{
    public class UpdateTransactionCommand : IRequest<TransactionResult>
    {
        public Guid? Id { get; set; }
        public Guid AccountId { get; set; }
        public decimal Amount { get; set; }
        public TransactionType Type { get; set; }  
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public Guid CategoryId { get; set; }
    }
}
