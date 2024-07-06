using Blog.Shared.Base;
using Blog.Shared.Extensions;
using Blog.Shared.Responses;
using FluentValidation;
using System.ComponentModel.DataAnnotations;

namespace Blog.Shared.Requests.Posts;

public class DeletePostRequest : IRequest
{
    public DeletePostRequest(Guid postId)
    {
        PostId = postId;
    }

    [Required]
    public Guid PostId { get; set; }

    public List<ValidationErrorResponse> Validate()
        => new DeletePostValidator().Validate(this).ToErrorList();
}

public class DeletePostValidator : AbstractValidator<DeletePostRequest>
{
    public DeletePostValidator()
    {
        RuleFor(x => x.PostId)
            .NotEmpty().WithMessage("PostId is required.");
    }
}
