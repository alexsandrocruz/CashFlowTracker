using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using Microsoft.Extensions.Configuration;
using System.Text.Json;

namespace CashFlowTracker.Infra.RabbitAdapter
{
    public class RabbitMQConsumer<T>
    {
        private readonly IModel _channel;
        private readonly IConnection _connection;
        private readonly string _queueName;

        public Func<T, Task> ProcessMessageAsync { get; set; }
        public Func<Exception, Task> ProcessErrorAsync { get; set; }

        public RabbitMQConsumer(IConfiguration configuration)
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

        public async Task StartProcessingAsync(CancellationToken cancellationToken)
        {
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += async (model, ea) =>
            {
                try
                {
                    var body = ea.Body.ToArray();
                    var messageJson = Encoding.UTF8.GetString(body);
                    var message = JsonSerializer.Deserialize<T>(messageJson);

                    if (ProcessMessageAsync != null && message != null)
                    {
                        await ProcessMessageAsync(message);
                    }
                }
                catch (Exception ex)
                {
                    if (ProcessErrorAsync != null)
                    {
                        await ProcessErrorAsync(ex);
                    }
                }
            };

            _channel.BasicConsume(queue: _queueName, autoAck: true, consumer: consumer);
        }

        public void Dispose()
        {
            _channel?.Close();
            _connection?.Close();
        }
    }

}