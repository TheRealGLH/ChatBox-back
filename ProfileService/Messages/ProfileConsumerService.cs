

using System.Text.Json;
using ChatBoxSharedObjects.Messages;
using ChatBoxSharedObjects.Settings;
using Microsoft.Extensions.Options;
using ProfileService.Models;
using ProfileService.Views;
using RabbitMQ.Client.Events;

namespace ProfileService.Messages;
class ProfileConsumerService : RabbitMqConsumerService
{
    private readonly IProfileStore _profileStore;
    public ProfileConsumerService(IRabbitMqService rabbitMqService, IOptions<RabbitMqSettings> options,
    IProfileStore profileStore) : base(rabbitMqService, options)
    {
        this._profileStore = profileStore;
    }

    public override void OnMessageReceived(BasicDeliverEventArgs eventArgs)
    {
        CharacterMessage IncomingMessage;
        IncomingMessage = JsonSerializer.Deserialize<CharacterMessage>(eventArgs.Body.ToString());
        if (IncomingMessage != null) this._profileStore.AddProfile(new Profile(IncomingMessage.CharId, IncomingMessage.OwnerId));
        else throw new JsonException("Deserialized message " + eventArgs.Body.ToString() + " was comehow null?");


    }
}