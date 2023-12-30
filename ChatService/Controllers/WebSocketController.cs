using Microsoft.AspNetCore.Mvc;
using System.Net.WebSockets;
using Microsoft.AspNetCore.Authorization;
using System.Text;
using System.Text.Json.Serialization;
using SocketMessages.Client;
using System.Text.Json;
using ChatService.Interfaces;
using ChatService.Connectors;
using System.Security.Claims;
using Microsoft.AspNetCore.SignalR.Protocol;
namespace ChatService.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize]
public class WebSocketController : ControllerBase
{

    private Dictionary<WebSocket, IClientMessager> connectedClients = new Dictionary<WebSocket, IClientMessager>();
    private ILogger<WebSocketController> _logger;
    private IServerMessager serverMessager;

    public WebSocketController(ILogger<WebSocketController> logger)
    {
        _logger = logger;
        this.serverMessager = new ChatServer();
    }

    [Route("/ws")]
    public async Task Get()
    {
        if (HttpContext.WebSockets.IsWebSocketRequest)
        {
            using var webSocket = await HttpContext.WebSockets.AcceptWebSocketAsync();
            _logger.LogDebug("Connected " + webSocket.ToString() + " " + HttpContext.TraceIdentifier);
            connectedClients.Add(webSocket, new ClientMessager(webSocket));
            await DecodeStream(webSocket);
        }
        else
        {
            _logger.LogInformation("Non-websocket request received from: " + HttpContext.TraceIdentifier);
            HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
        }
    }

    private async Task DecodeStream(WebSocket webSocket)
    {
        var buffer = new byte[1024 * 4];
        var receiveResult = await webSocket.ReceiveAsync(
            new ArraySegment<byte>(buffer), CancellationToken.None);


        while (!receiveResult.CloseStatus.HasValue)
        {
            HandleIncomingJson(DecodeByteArray(buffer, receiveResult.Count), connectedClients[webSocket]);
            receiveResult = await webSocket.ReceiveAsync(
                new ArraySegment<byte>(buffer), CancellationToken.None);
        }

        await webSocket.CloseAsync(
            receiveResult.CloseStatus.Value,
            receiveResult.CloseStatusDescription,
            CancellationToken.None);
        Close(webSocket);
    }

    private void Close(WebSocket webSocket)
    {
        _logger.LogDebug("Disconnected" + webSocket.ToString() + " " + HttpContext.TraceIdentifier);
        connectedClients.Remove(webSocket);
    }

    private string DecodeByteArray(byte[] bytes, int count)
    {
        return Encoding.UTF8.GetString(bytes, 0, count);
    }

    void HandleIncomingJson(string json, IClientMessager client)
    {
        _logger.LogTrace("JSON from:" + User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
        if (json != null)
        {
            ClientMessage msg = JsonSerializer.Deserialize<ClientMessage>(json);
            switch (msg.MessageType)
            {
                case ClientMessageType.Ping:
                    _logger.Log(LogLevel.Debug, "Ping.");
                    serverMessager.SendPing(client);
                    break;
                case ClientMessageType.SignIn:
                    ClientMessageSignIn messageSignIn = JsonSerializer.Deserialize<ClientMessageSignIn>(json);
                    //serverMessager.SignIn(messageSignIn.characterId, User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                    break;
                case ClientMessageType.Text:
                    break;

            }
        }
        else
        {
            _logger.LogInformation("Null json received");
        }
    }
}

