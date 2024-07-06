using Blog.Domain.Abstractions;
using Blog.Domain.Abstractions.Repositories;
using Blog.Shared.Requests.Posts;
using Blog.Shared.Responses;
using MediatR;

namespace Blog.Application.Commands.Posts;

public class DeletePostCommand(DeletePostRequest request) :
    DeletePostRequest(request.PostId),
    IRequest<BaseResponse>;

public class DeletePostHandler(IPostRepository repository, IUnitOfWork unitOfWork, ICurrentUser currentUser) :
    IRequestHandler<DeletePostCommand, BaseResponse>
{
    private readonly IPostRepository _repository = repository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ICurrentUser _currentUser = currentUser;

    public async Task<BaseResponse> Handle(DeletePostCommand request, CancellationToken cancellationToken)
    {
        var errors = request.Validate();
        if (errors.Count > 0)
            return BaseResponse.ValidationError(errors);

        var userId = _currentUser.GetUserId();
        var post = await _repository.GetByIdAsync(request.PostId, cancellationToken);
        if (post is null || post.UserId != userId)
            return BaseResponse.Error("Post not found");

        await _repository.DeleteAsync(request.PostId, userId, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return BaseResponse.Success();

    }
}
