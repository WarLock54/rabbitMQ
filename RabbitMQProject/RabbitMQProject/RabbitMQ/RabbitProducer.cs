using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json.Serialization;

namespace RabbitMQProject.RabbitMQ
{
    public class RabbitProducer : IRabbitProducer
    {
        private readonly IConfiguration configuration;
        public RabbitProducer(IConfiguration _conf)
        {
            configuration= _conf;
        }
        public void SendMessage<T>(T message)
        {
            var fac = new ConnectionFactory{
                HostName=configuration.GetSection("RabbitMQConfiguration:Connection").Value,
            };
            var connection=fac.CreateConnection();
            using (var channel = connection.CreateModel())
            {
                // Declare the queue here
                channel.QueueDeclare(queue: "product",
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                // Now you can use the queue safely

                channel.QueueDeclare(exclusive: true, autoDelete: true);
                var json = JsonConvert.SerializeObject(message);
                var body = Encoding.UTF8.GetBytes(json);

                channel.BasicPublish(exchange: "", routingKey: "Hello", body: body);
            }

        }
    }
}
