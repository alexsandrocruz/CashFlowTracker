namespace CashFlowTracker.Infra.RabbitAdapter
{
    public interface IRabbitMQProducer
    {
        void Publish<T>(T message);
    }
}
