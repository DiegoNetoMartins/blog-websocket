using Blog.Api.Extensions;
using Blog.Application.Commands.Users;
using Blog.Shared.Requests.Users;
using Blog.Shared.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Api.Endpoints.V1;

internal static class UsersEndpoints
{
    internal static async Task<IResult> CreateUserAsync(
        [FromServices] IMediator mediator,
        [FromBody] CreateUserRequest request,
        CancellationToken cancellationToken)
    {
        try
        {
            var response = await mediator.Send(new CreateUserCommand(request), cancellationToken);
            return response.ToResult();
        }
        catch (Exception ex)
        {
            return BaseResponse.Error(ex.Message).ToResult();
        }
    }

    internal static async Task<IResult> AuthenticateUserAsync(
        [FromServices] IMediator mediator,
        [FromBody] AuthenticateUserRequest request,
        CancellationToken cancellationToken)
    {
        try
        {
            var response = await mediator.Send(new AuthenticateUserCommand(request), cancellationToken);
            return response.ToResult();
        }
        catch (Exception ex)
        {
            return BaseResponse.Error(ex.Message).ToResult();
        }
    }
}
