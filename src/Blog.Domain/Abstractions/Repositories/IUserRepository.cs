using Blog.Domain.Abstractions.Base;
using Blog.Domain.Entities;

namespace Blog.Domain.Abstractions.Repositories;

public interface IUserRepository : IRepository<User>
{
    Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken);
}
