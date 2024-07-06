using Blog.Domain.Abstractions.Base;
using Blog.Domain.Entities;

namespace Blog.Domain.Abstractions.Repositories;

public interface IPostRepository : IRepository<Post>
{
    Task<Post?> GetByIdAsync(Guid postId, CancellationToken cancellationToken);
    Task<Post?> GetAsync(Guid postId, Guid userId, CancellationToken cancellationToken);
    Task<(IList<Post> Posts, int TotalPosts)> GetAllAsync(int pageNumber, int pageSize, CancellationToken cancellationToken);
    Task<(IList<Post> Posts, int TotalPosts)> GetByUserIdAsync(Guid userId, int pageNumber, int pageSize, CancellationToken cancellationToken);
    Task DeleteAsync(Guid postId, Guid userId, CancellationToken cancellationToken);
}
