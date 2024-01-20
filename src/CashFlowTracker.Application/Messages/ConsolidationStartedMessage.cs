namespace CashFlowTracker.Application.Messages
{
    public class ConsolidationStartedMessage : BaseMessage
    {
        public Guid AccountId { get; set; }
        public DateTime Date { get; set; }
    }
}
