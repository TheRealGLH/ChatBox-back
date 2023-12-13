

using ChatBoxSharedObjects.Messages;
using ChatBoxSharedObjects.Settings;
using Microsoft.Extensions.Options;
using RabbitMQ.Client.Events;

namespace ProfileService.Messages;
class ProfileConsumerService : RabbitMqConsumerService
{
    public ProfileConsumerService(IRabbitMqService rabbitMqService, IOptions<RabbitMqSettings> options) : base(rabbitMqService, options)
    {
    }

    public override void OnMessageReceived(BasicDeliverEventArgs eventArgs)
    {
        //eventArgs.Body.
    }
}