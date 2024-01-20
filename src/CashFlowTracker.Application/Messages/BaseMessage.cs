namespace CashFlowTracker.Application.Messages
{
    public abstract class BaseMessage
    {
        public Guid MessageId { get; set; }
        public DateTime Timestamp { get; set; }

        protected BaseMessage()
        {
            MessageId = Guid.NewGuid();
            Timestamp = DateTime.UtcNow;
        }
    }
}
