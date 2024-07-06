using Blog.Domain.Abstractions;
using Blog.Domain.Abstractions.Repositories;
using Blog.Domain.Entities;
using Blog.Domain.ValueObjects;
using Blog.Shared.Requests.Users;
using Blog.Shared.Responses;
using MediatR;

namespace Blog.Application.Commands.Users;

public class CreateUserCommand(CreateUserRequest request) :
    CreateUserRequest(request.Name, request.Email, request.Password, request.ConfirmPassword),
    IRequest<BaseResponse<Guid>>;

public class CreateUserCommandHandler(IUserRepository repository, IUnitOfWork unitOfWork) :
    IRequestHandler<CreateUserCommand, BaseResponse<Guid>>
{
    private readonly IUserRepository _repository = repository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<BaseResponse<Guid>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var errors = request.Validate();
        if (errors.Count > 0)
            return BaseResponse<Guid>.ValidationError(errors);

        var userId = Guid.NewGuid();
        var password = new Password(userId, request.Password);
        var user = new User(userId, request.Name, request.Email, password);
        await _repository.AddAsync(user, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return BaseResponse<Guid>.Success(userId);
    }
}

