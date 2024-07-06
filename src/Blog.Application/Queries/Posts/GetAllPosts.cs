using Blog.Domain.Abstractions.Repositories;
using Blog.Shared.Responses;
using Blog.Shared.Responses.Posts;
using MediatR;

namespace Blog.Application.Queries.Posts;

public record GetAllPostsQuery(int PageNumber, int PageSize) : IRequest<BaseResponse<PaginatedListResponse<PostResponse>>>;

public class GetAllPostsQueryHandler(IPostRepository repository) :
    IRequestHandler<GetAllPostsQuery, BaseResponse<PaginatedListResponse<PostResponse>>>
{
    private readonly IPostRepository _repository = repository;

    public async Task<BaseResponse<PaginatedListResponse<PostResponse>>> Handle(GetAllPostsQuery request, CancellationToken cancellationToken)
    {
        var result = await _repository.GetAllAsync(request.PageNumber, request.PageSize, cancellationToken);
        if (result.TotalPosts == 0 || result.Posts.Count == 0)
            return BaseResponse<PaginatedListResponse<PostResponse>>.Error("No posts found");

        var posts = new PaginatedListResponse<PostResponse>(
            request.PageNumber,
            request.PageSize,
            result.TotalPosts / request.PageSize,
            result.TotalPosts,
            result.Posts.Select(post => new PostResponse(post.Id, post.Title, post.Content)).ToList()
        );
        return BaseResponse<PaginatedListResponse<PostResponse>>.Success(posts);
    }
}