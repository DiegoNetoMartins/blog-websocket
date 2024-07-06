namespace Blog.Domain.Abstractions;

public interface ICurrentUser
{
    Guid GetUserId();
}
