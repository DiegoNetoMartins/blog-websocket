using Blog.Domain.Entities.Base;

namespace Blog.Domain.Abstractions.Base;

public interface IRepository
{

}

public interface IRepository<TEntity> : IRepository where TEntity : Entity
{
    Task AddAsync(TEntity entity, CancellationToken cancellationToken);

    void Update(TEntity entity);
}

