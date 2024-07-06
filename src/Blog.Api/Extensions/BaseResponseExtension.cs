using Blog.Shared.Responses;

namespace Blog.Api.Extensions;

internal static class BaseResponseExtension
{
    internal static IResult ToResult(this BaseResponse response)
    {
        if (response.IsSuccess)
        {
            return Results.Ok(response);
        }
        else if (response?.Errors?.Count > 0)
        {
            return Results.BadRequest(response);
        }
        else
        {
            return Results.UnprocessableEntity(response);
        }
    }
}
