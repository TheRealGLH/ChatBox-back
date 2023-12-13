using RabbitMQ.Client;

namespace ChatBoxSharedObjects.Messages;
public interface IRabbitMqService
{
    IConnection CreateChannel();
}