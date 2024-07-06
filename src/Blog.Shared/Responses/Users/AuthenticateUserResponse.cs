namespace Blog.Shared.Responses.Users;

public class AuthenticateUserResponse
{
    public AuthenticateUserResponse(string token, DateTime? expires)
    {
        Token = token;
        Expires = expires;
    }

    public string Token { get; set; }
    public DateTime? Expires { get; set; }
}
