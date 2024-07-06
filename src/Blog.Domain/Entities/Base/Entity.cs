namespace Blog.Domain.Entities.Base;

public abstract class Entity : IEquatable<Guid>
{
    public Entity()
        => Id = Guid.NewGuid();

    public Guid Id { get; protected set; }

    public bool Equals(Guid id)
        => Id == id;

    public override int GetHashCode()
        => Id.GetHashCode();
}
