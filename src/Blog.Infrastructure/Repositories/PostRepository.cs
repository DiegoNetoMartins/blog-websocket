using Blog.Domain.Abstractions.Repositories;
using Blog.Domain.Entities;
using Blog.Infrastructure.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace Blog.Infrastructure.Repositories;

public class PostRepository(BlogDbContext context) : Repository<Post>(context), IPostRepository
{
    private readonly BlogDbContext _context = context;

    public async Task<Post?> GetByIdAsync(Guid postId, CancellationToken cancellationToken)
        => await _context.Posts.FindAsync([postId], cancellationToken);

    public async Task DeleteAsync(Guid postId, Guid userId, CancellationToken cancellationToken)
        => await _context.Posts
            .Where(post => post.Id == postId && post.UserId == userId)
            .ExecuteDeleteAsync(cancellationToken);

    public async Task<(IList<Post> Posts, int TotalPosts)> GetAllAsync(int pageNumber, int pageSize, CancellationToken cancellationToken)
    {
        var posts = await _context.Posts
            .AsNoTracking()
            .OrderByDescending(post => post.CreatedAtOnUtc)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);
        var totalPosts = await _context.Posts.CountAsync(cancellationToken);
        return (posts, totalPosts);
    }

    public async Task<Post?> GetAsync(Guid postId, Guid userId, CancellationToken cancellationToken)
        => await _context.Posts
            .FirstOrDefaultAsync(post => post.Id == postId && post.UserId == userId, cancellationToken);

    public async Task<(IList<Post> Posts, int TotalPosts)> GetByUserIdAsync(Guid userId, int pageNumber, int pageSize, CancellationToken cancellationToken)
    {
        var posts = await _context.Posts
            .AsNoTracking()
            .Where(post => post.UserId == userId)
            .OrderByDescending(post => post.CreatedAtOnUtc)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);
        var totalPosts = await _context.Posts.CountAsync(post => post.UserId == userId, cancellationToken);
        return (posts, totalPosts);
    }
}
