using ChatBoxSharedObjects.Settings;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace ChatBoxSharedObjects.Messages;

public abstract class RabbitMqConsumerService : IRabbitMqConsumerService, IDisposable
{
    private readonly IModel _model;
    private readonly IConnection _connection;
    public readonly RabbitMqSettings _configuration;
    public RabbitMqConsumerService(IRabbitMqService rabbitMqService, IOptions<RabbitMqSettings> options)
    {
        _configuration = options.Value;
        _connection = rabbitMqService.CreateChannel();
        _model = _connection.CreateModel();
        _model.QueueDeclare(_configuration.QueueName, durable: true, exclusive: false, autoDelete: false);
        _model.ExchangeDeclare(_configuration.ExchangeName, ExchangeType.Fanout, durable: true, autoDelete: false);
        _model.QueueBind(_configuration.QueueName, _configuration.ExchangeName, string.Empty);
    }

    public async Task ReadMessgaes()
    {
        var consumer = new AsyncEventingBasicConsumer(_model);
        consumer.Received += async (channel, EventArgs) =>
        {
            var body = EventArgs.Body.ToArray();
            var text = System.Text.Encoding.UTF8.GetString(body);
            Console.WriteLine(text);
            await Task.CompletedTask;
            _model.BasicAck(EventArgs.DeliveryTag, false);
            OnMessageReceived(EventArgs);
        };
        _model.BasicConsume(_model.CurrentQueue, false, consumer);
        await Task.CompletedTask;
    }

    public void Dispose()
    {
        if (_model.IsOpen)
            _model.Close();
        if (_connection.IsOpen)
            _connection.Close();
    }

    public abstract void OnMessageReceived(BasicDeliverEventArgs eventArgs);
}