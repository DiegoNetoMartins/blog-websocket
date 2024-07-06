using Blog.Domain.Abstractions;

namespace Blog.Infrastructure;

internal class UnitOfWork(BlogDbContext context) : IUnitOfWork
{
    private readonly BlogDbContext _context = context;

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        => await _context.SaveChangesAsync(cancellationToken);
}
