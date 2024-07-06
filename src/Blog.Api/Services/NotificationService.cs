using Blog.Application.Abstractions;
using Blog.Domain.Entities;
using System.Collections.Concurrent;
using System.Net.WebSockets;
using System.Text;

namespace Blog.Api.Services;

internal class NotificationService : INotificationService
{

    private readonly ConcurrentDictionary<Guid, WebSocket> _clients = new();

    public async Task NotifyPostCreated(Post post, CancellationToken cancellationToken)
    {
        var message = $"New post: {post.Title}";
        var buffer = Encoding.UTF8.GetBytes(message);

        foreach (var client in _clients)
        {
            if (client.Value.State == WebSocketState.Open)
            {
                await client.Value.SendAsync(new ArraySegment<byte>(buffer, 0, buffer.Length), WebSocketMessageType.Text, true, cancellationToken);
            }
        }
    }

    public void AddClient(Guid id, WebSocket socket)
    {
        _clients.TryAdd(id, socket);
    }

    public void RemoveClient(Guid id)
    {
        _clients.TryRemove(id, out var _);
    }
}
