using Blog.Api.Services;
using Blog.Application.Abstractions;
using System.Net.WebSockets;

namespace Blog.Api.Endpoints.V1;

internal static class WebSocketsEndpoints
{
    public static async Task NotificationsAsync(HttpContext context, INotificationService notificationService)
    {
        if (context.WebSockets.IsWebSocketRequest && notificationService is NotificationService notification)
        {
            WebSocket webSocket = await context.WebSockets.AcceptWebSocketAsync();
            var id = Guid.NewGuid();
            notification.AddClient(id, webSocket);
            await WaitForConnectionClose(webSocket, CancellationToken.None);
            notification.RemoveClient(id);
        }
    }

    private static async Task WaitForConnectionClose(WebSocket webSocket, CancellationToken cancellationToken)
    {
        var buffer = new byte[1024 * 4];
        while (webSocket.State == WebSocketState.Open)
        {
            _ = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), cancellationToken);
        }
    }
}
