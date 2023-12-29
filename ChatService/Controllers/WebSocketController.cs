using Microsoft.AspNetCore.Mvc;
using System.Net.WebSockets;
using Microsoft.AspNetCore.Authorization;
using System.Text;
using System.Text.Json.Serialization;
using SocketMessages.Client;
using System.Text.Json;
namespace ChatService.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize]
public class WebSocketController : ControllerBase
{
    [Route("/ws")]
    public async Task Get()
    {
        if (HttpContext.WebSockets.IsWebSocketRequest)
        {
            using var webSocket = await HttpContext.WebSockets.AcceptWebSocketAsync();
            await DecodeStream(webSocket);
        }
        else
        {
            HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
        }
    }

    private static async Task DecodeStream(WebSocket webSocket)
    {
        var buffer = new byte[1024 * 4];
        var receiveResult = await webSocket.ReceiveAsync(
            new ArraySegment<byte>(buffer), CancellationToken.None);

        while (!receiveResult.CloseStatus.HasValue)
        {
            HandleIncomingJson(DecodeByteArray(buffer,receiveResult.Count));
            await webSocket.SendAsync(
                new ArraySegment<byte>(buffer, 0, receiveResult.Count),
                receiveResult.MessageType,
                receiveResult.EndOfMessage,
                CancellationToken.None);

            receiveResult = await webSocket.ReceiveAsync(
                new ArraySegment<byte>(buffer), CancellationToken.None);
        }

        await webSocket.CloseAsync(
            receiveResult.CloseStatus.Value,
            receiveResult.CloseStatusDescription,
            CancellationToken.None);
    }

    private static string DecodeByteArray(byte[] bytes, int count)
    {
        return Encoding.UTF8.GetString(bytes,0,count);
    }

    static void HandleIncomingJson(string json)
    {
        ClientMessage msg = JsonSerializer.Deserialize<ClientMessage>(json);
        switch (msg.MessageType)
        {
            case ClientMessageType.Ping:
            Console.WriteLine("Ping.");
            break;

        }
    }
}

