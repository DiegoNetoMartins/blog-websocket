using Blog.Domain.Entities;

namespace Blog.Application.Abstractions;

public interface INotificationService
{
    Task NotifyPostCreated(Post post, CancellationToken cancellationToken);
}
