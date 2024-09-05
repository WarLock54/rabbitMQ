namespace RabbitMQProject.RabbitMQ
{
    public interface IRabbitProducer
    {
        public void SendMessage<T>(T message);
    }
}
