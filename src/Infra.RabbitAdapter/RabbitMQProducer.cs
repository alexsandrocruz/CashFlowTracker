using RabbitMQ.Client;
using System.Text;
using Microsoft.Extensions.Configuration;
using System.Text.Json;

namespace CashFlowTracker.Infra.RabbitAdapter
{
    public class RabbitMQProducer : IRabbitMQProducer
    {
        private readonly IModel _channel;
        private readonly IConnection _connection;
        private readonly string _queueName;

        public RabbitMQProducer(IConfiguration configuration)
        {
            var rabbitMQConfig = configuration.GetSection("RabbitMQ");
            _queueName = rabbitMQConfig["QueueName"];
            var factory = new ConnectionFactory()
            {
                HostName = rabbitMQConfig["HostName"],
                Port = int.Parse(rabbitMQConfig["Port"]),
                UserName = rabbitMQConfig["Username"],
                Password = rabbitMQConfig["Password"]
            };

            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.QueueDeclare(queue: _queueName, durable: true, exclusive: false, autoDelete: false, arguments: null);
        }

        public virtual void Publish<T>(T message)
        {
            var messageJson = JsonSerializer.Serialize(message);
            var body = Encoding.UTF8.GetBytes(messageJson);

            _channel.BasicPublish(exchange: "", routingKey: _queueName, basicProperties: null, body: body);
        }

        public void Dispose()
        {
            _channel?.Close();
            _connection?.Close();
        }
    }
}
