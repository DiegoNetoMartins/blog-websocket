using Blog.Domain.Entities.Base;
using Blog.Domain.ValueObjects;

namespace Blog.Domain.Entities;

public class User : Entity
{
#pragma warning disable CS8618 // O campo não anulável precisa conter um valor não nulo ao sair do construtor. Considere declará-lo como anulável.
    protected User() { }
#pragma warning restore CS8618 // O campo não anulável precisa conter um valor não nulo ao sair do construtor. Considere declará-lo como anulável.

    public User(Guid id, string name, Email email, Password password)
    {
        Id = id;
        Name = name;
        Email = email;
        Password = password;
    }

    public string Name { get; private set; }
    public Email Email { get; private set; }
    public Password Password { get; private set; }
    public DateTime CreatedAtOnUtc { get; private set; } = DateTime.UtcNow;
    public DateTime? UpdatedAtOnUtc { get; private set; }

    public IList<Post> Posts { get; private set; } = [];
}
