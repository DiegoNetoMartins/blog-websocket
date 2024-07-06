using Blog.Domain.Abstractions.Base;
using Blog.Domain.Entities.Base;

namespace Blog.Infrastructure.Repositories.Base;

public abstract class Repository<TEntity>(BlogDbContext context) : IRepository<TEntity> where TEntity : Entity
{

    private BlogDbContext _context = context;

    public async Task AddAsync(TEntity entity, CancellationToken cancellationToken)
        => await _context.Set<TEntity>().AddAsync(entity, cancellationToken);

    public void Update(TEntity entity)
        => _context.Set<TEntity>().Update(entity);
}
