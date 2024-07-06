using Blog.Domain.Abstractions;
using Blog.Domain.Abstractions.Repositories;
using Blog.Shared.Requests.Posts;
using Blog.Shared.Responses;
using MediatR;

namespace Blog.Application.Commands.Posts;

public class UpdatePostCommand(UpdatePostRequest request) :
    UpdatePostRequest(request.Id, request.Title, request.Content),
    IRequest<BaseResponse>;

public class UpdatePostCommandHandler(ICurrentUser currentUser, IPostRepository repository, IUnitOfWork unitOfWork) :
    IRequestHandler<UpdatePostCommand, BaseResponse>
{
    private readonly ICurrentUser _currentUser = currentUser;
    private readonly IPostRepository _repository = repository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<BaseResponse> Handle(UpdatePostCommand request, CancellationToken cancellationToken)
    {
        var errors = request.Validate();
        if (errors.Count > 0)
            return BaseResponse.ValidationError(errors);

        var post = await _repository.GetByIdAsync(request.Id, cancellationToken);
        if (post == null || post.UserId != _currentUser.GetUserId())
            return BaseResponse.Error("Post not found");

        post.Update(request.Title, request.Content);

        _repository.Update(post);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return BaseResponse.Success();
    }
}