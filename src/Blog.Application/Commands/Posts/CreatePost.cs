using Blog.Application.Abstractions;
using Blog.Domain.Abstractions;
using Blog.Domain.Abstractions.Repositories;
using Blog.Domain.Entities;
using Blog.Shared.Requests.Posts;
using Blog.Shared.Responses;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Blog.Application.Commands.Posts;

public class CreatePostCommand(CreatePostRequest request) :
    CreatePostRequest(request.Title, request.Content),
    IRequest<BaseResponse<Guid>>;

public class CreatePostCommandHandler(ICurrentUser currentUser, IPostRepository repository, IUnitOfWork unitOfWork, INotificationService notificationService, ILogger<CreatePostCommandHandler> logger) :
    IRequestHandler<CreatePostCommand, BaseResponse<Guid>>
{
    private readonly ICurrentUser _currentUser = currentUser;
    private readonly IPostRepository _repository = repository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly INotificationService _notificationService = notificationService;
    private readonly ILogger<CreatePostCommandHandler> _logger = logger;

    public async Task<BaseResponse<Guid>> Handle(CreatePostCommand request, CancellationToken cancellationToken)
    {
        var errors = request.Validate();
        if (errors.Count > 0)
            return BaseResponse<Guid>.ValidationError(errors);

        var post = new Post(_currentUser.GetUserId(), request.Title, request.Content);
        await _repository.AddAsync(post, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        try
        {
            await _notificationService.NotifyPostCreated(post, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to notify post created");
        }
        return BaseResponse<Guid>.Success(post.Id);
    }
}