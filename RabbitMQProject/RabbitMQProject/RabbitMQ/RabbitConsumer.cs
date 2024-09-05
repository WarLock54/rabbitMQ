using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace RabbitMQProject.RabbitMQ
{
    public class RabbitConsumer
   : IRabbitConsumer
    {
        private readonly IConfiguration configuration;
        public RabbitConsumer(IConfiguration _conf)
        {
            configuration = _conf;
        }

        public void ReceiveMessage()
        {
            var fac = new ConnectionFactory
            {
                HostName = configuration.GetSection("RabbitMQConfiguration:Connection").Value,
            };
            var connection = fac.CreateConnection();
            using var channel = connection.CreateModel();
            channel.QueueDeclare(queue: "product",
                                    durable: false,
                                    exclusive: false,
                                    autoDelete: false,
                                    arguments: null);
            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += Receiver;

            channel.BasicConsume(queue: "product", consumer: consumer);
            Console.ReadLine();
        }
        void Receiver(object model,BasicDeliverEventArgs basicDeliverEventArgs)
        {
            //byte string converter

            var body = basicDeliverEventArgs.Body.ToArray();
            var message= Encoding.UTF8.GetString(body);
            System.Console.WriteLine(message);
        }
    }
}
