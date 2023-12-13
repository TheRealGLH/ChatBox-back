using ChatBoxSharedObjects.Messages;
using ChatBoxSharedObjects.Settings;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;
namespace CharacterService.Messaging;
public class RabbitMqProducer : IRabbitMqProducer
{

    private readonly RabbitMqSettings _configuration;
    private readonly IRabbitMqService _rabbitService;
    private IModel _model;
    private IConnection _connection;
    public RabbitMqProducer(IRabbitMqService rabbitMqService, IOptions<RabbitMqSettings> options)
    {
        this._configuration = options.Value;
        this._rabbitService = rabbitMqService;
    }
    public void SendCreationMessage<T>(T message)
    {

        _connection = _rabbitService.CreateChannel();
        _model = _connection.CreateModel();

        //declare the queue after mentioning name and a few property related to that
        //channel.QueueDeclare("profile", exclusive: false);
        //Serialize the message
        var json = JsonConvert.SerializeObject(message);
        var body = Encoding.UTF8.GetBytes(json);
        //put the data on to the product queue
        _model.BasicPublish(exchange: _configuration.ExchangeName, ExchangeType.Fanout, body: body);
    }

    void Dispose()
    {
        if(this._connection.IsOpen) this._connection.Close();
        if(this._model.IsOpen) this._model.Close();
    }
}
