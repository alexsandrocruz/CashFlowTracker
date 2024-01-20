
namespace CashFlowTracker.Application.Messages
{
    public class TransactionDeletedMessage : BaseMessage
    {
        public Guid Id { get; set; }
    }

}
