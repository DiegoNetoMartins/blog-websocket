using Blog.Application.Abstractions;
using Blog.Domain.Abstractions.Repositories;
using Blog.Shared.Requests.Users;
using Blog.Shared.Responses;
using Blog.Shared.Responses.Users;
using MediatR;

namespace Blog.Application.Commands.Users;

public class AuthenticateUserCommand(AuthenticateUserRequest request) :
    AuthenticateUserRequest(request.Email, request.Password),
    IRequest<BaseResponse<AuthenticateUserResponse>>;

public class AuthenticateUserCommandHandler(IUserRepository repository, ITokenService tokenService) :
    IRequestHandler<AuthenticateUserCommand, BaseResponse<AuthenticateUserResponse>>
{
    private readonly IUserRepository _repository = repository;
    private readonly ITokenService _tokenService = tokenService;

    public async Task<BaseResponse<AuthenticateUserResponse>> Handle(AuthenticateUserCommand request, CancellationToken cancellationToken)
    {
        var errors = request.Validate();
        if (errors.Count > 0)
            return BaseResponse<AuthenticateUserResponse>.ValidationError(errors);

        var user = await _repository.GetByEmailAsync(request.Email, cancellationToken);
        if (user is null)
            return BaseResponse<AuthenticateUserResponse>.Error("User not found");

        if (!user.Password.Verify(user.Id, request.Password))
            return BaseResponse<AuthenticateUserResponse>.Error("Invalid password");

        var token = _tokenService.GenerateToken(user);
        return BaseResponse<AuthenticateUserResponse>.Success(token);
    }
}