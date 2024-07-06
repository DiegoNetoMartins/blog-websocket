using Blog.Application.Abstractions;
using Blog.Domain.Entities;
using Blog.Shared.Responses.Users;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Blog.Api.Services;

internal class TokenService : ITokenService
{
    public AuthenticateUserResponse GenerateToken(User user)
    {
        List<Claim> claims = [
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.GivenName, user.Name),
            new Claim(ClaimTypes.Name, user.Email)
        ];
        var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration.JwtPrivateKey));
        var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);
        var expires = DateTime.UtcNow.AddHours(10);
        var jwtSecurityToken = new JwtSecurityToken(
            claims: claims,
            expires: expires,
            signingCredentials: signIn
        );
        var token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

        return new AuthenticateUserResponse(token, expires);
    }
}
