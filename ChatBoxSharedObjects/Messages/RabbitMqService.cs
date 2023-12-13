
using ChatBoxSharedObjects.Settings;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;

namespace ChatBoxSharedObjects.Messages;
public class RabbitMqService : IRabbitMqService
{
    private readonly RabbitMqSettings _configuration;
    public RabbitMqService(IOptions<RabbitMqSettings> options)
    {
        _configuration = options.Value;
    }
    public IConnection CreateChannel()
    {
        ConnectionFactory connection = new ConnectionFactory()
        {
            UserName = _configuration.Username,
            Password = _configuration.Password,
            HostName = _configuration.HostName
        };
        connection.DispatchConsumersAsync = true;
        var channel = connection.CreateConnection();
        return channel;
    }
}