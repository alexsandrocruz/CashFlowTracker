namespace CashFlowTracker.Application.Messages
{
    public class ConsolidationStartedMessage
    {
        public Guid AccountId { get; set; }
        public DateTime Date { get; set; }
    }
}
