using Blog.Shared.Base;
using Blog.Shared.Extensions;
using Blog.Shared.Responses;
using FluentValidation;
using System.ComponentModel.DataAnnotations;

namespace Blog.Shared.Requests.Posts;

public class UpdatePostRequest : IRequest
{
    public UpdatePostRequest(Guid id, string title, string content)
    {
        Id = id;
        Title = title;
        Content = content;
    }

    [Required]
    public Guid Id { get; set; }
    [Required]
    [MaxLength(100)]
    public string Title { get; set; }
    [Required]
    public string Content { get; set; }

    public List<ValidationErrorResponse> Validate()
        => new UpdatePostValidator().Validate(this).ToErrorList();
}

internal class UpdatePostValidator : AbstractValidator<UpdatePostRequest>
{
    public UpdatePostValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Id is required.");
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title is required.")
            .MaximumLength(100).WithMessage("Title must not exceed 100 characters.");
        RuleFor(x => x.Content)
            .NotEmpty().WithMessage("Content is required.");
    }
}
