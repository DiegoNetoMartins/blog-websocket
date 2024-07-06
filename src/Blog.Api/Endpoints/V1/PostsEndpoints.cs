using Blog.Api.Extensions;
using Blog.Application.Commands.Posts;
using Blog.Application.Queries.Posts;
using Blog.Shared.Requests.Posts;
using Blog.Shared.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Api.Endpoints.V1;

internal static class PostsEndpoints
{
    internal static async Task<IResult> CreatePostAsync(
        [FromServices] IMediator mediator,
        [FromBody] CreatePostRequest request,
        CancellationToken cancellationToken)
    {
        try
        {
            var response = await mediator.Send(new CreatePostCommand(request), cancellationToken);
            return response.ToResult();
        }
        catch (Exception ex)
        {
            return BaseResponse.Error(ex.Message).ToResult();
        }
    }

    internal static async Task<IResult> UpdatePostAsync(
        [FromServices] IMediator mediator,
        [FromBody] UpdatePostRequest request,
        CancellationToken cancellationToken)
    {
        try
        {
            var response = await mediator.Send(new UpdatePostCommand(request), cancellationToken);
            return response.ToResult();
        }
        catch (Exception ex)
        {
            return BaseResponse.Error(ex.Message).ToResult();
        }
    }

    internal static async Task<IResult> DeletePostAsync(
        [FromServices] IMediator mediator,
        [FromBody] DeletePostRequest request,
        CancellationToken cancellationToken)
    {
        try
        {
            var response = await mediator.Send(new DeletePostCommand(request), cancellationToken);
            return response.ToResult();
        }
        catch (Exception ex)
        {
            return BaseResponse.Error(ex.Message).ToResult();
        }
    }

    internal static async Task<IResult> GetPostSByUserIdAsync(
        [FromServices] IMediator mediator,
        [FromQuery] int pageNumber,
        [FromQuery] int pageSize,
        CancellationToken cancellationToken)
    {
        try
        {
            var response = await mediator.Send(new GetPostsByUserIdQuery(pageNumber, pageSize), cancellationToken);
            return response.ToResult();
        }
        catch (Exception ex)
        {
            return BaseResponse.Error(ex.Message).ToResult();
        }
    }

    internal static async Task<IResult> GetPostsAsync(
        [FromServices] IMediator mediator,
        [FromQuery] int pageNumber,
        [FromQuery] int pageSize,
        CancellationToken cancellationToken)
    {
        try
        {
            var response = await mediator.Send(new GetAllPostsQuery(pageNumber, pageSize), cancellationToken);
            return response.ToResult();
        }
        catch (Exception ex)
        {
            return BaseResponse.Error(ex.Message).ToResult();
        }
    }
}
