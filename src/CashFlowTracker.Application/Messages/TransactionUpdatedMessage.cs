using CashFlowTracker.Domain.Enums;

namespace CashFlowTracker.Application.Messages
{
    public class TransactionUpdatedMessage : BaseMessage
    {
        public Guid? Id { get; set; }
        public Guid AccountId { get; set; }
        public decimal Amount { get; set; }
        public TransactionType Type { get; set; }
        public DateTime TransactionDate { get; set; }
        public string Description { get; set; }
    }

}
