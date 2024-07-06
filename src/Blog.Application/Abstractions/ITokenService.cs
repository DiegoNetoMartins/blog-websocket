using Blog.Domain.Entities;
using Blog.Shared.Responses.Users;

namespace Blog.Application.Abstractions;

public interface ITokenService
{
    AuthenticateUserResponse GenerateToken(User user);
}
