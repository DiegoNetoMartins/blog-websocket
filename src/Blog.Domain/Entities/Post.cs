using Blog.Domain.Entities.Base;

namespace Blog.Domain.Entities;

public class Post : Entity
{
#pragma warning disable CS8618 // O campo não anulável precisa conter um valor não nulo ao sair do construtor. Considere declará-lo como anulável.
    protected Post() { }
#pragma warning restore CS8618 // O campo não anulável precisa conter um valor não nulo ao sair do construtor. Considere declará-lo como anulável.

    public Post(Guid userId, string title, string content)
    {
        UserId = userId;
        Title = title;
        Content = content;
    }

    public string Title { get; private set; }
    public string Content { get; private set; }
    public DateTime CreatedAtOnUtc { get; private set; } = DateTime.UtcNow;
    public DateTime? UpdatedAtOnUtc { get; private set; }
    public Guid UserId { get; private set; }

    public User User { get; private set; } = null!;

    public void Update(string title, string content)
    {
        Title = title;
        Content = content;
        UpdatedAtOnUtc = DateTime.UtcNow;
    }
}
