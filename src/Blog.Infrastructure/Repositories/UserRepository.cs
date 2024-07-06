using Blog.Domain.Abstractions.Repositories;
using Blog.Domain.Entities;
using Blog.Infrastructure.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace Blog.Infrastructure.Repositories;

public class UserRepository(BlogDbContext context) : Repository<User>(context), IUserRepository
{
    private readonly BlogDbContext _context = context;

    public async Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken)
        => await _context.Users
            .FirstOrDefaultAsync(user => user.Email.Address == email, cancellationToken);
}
