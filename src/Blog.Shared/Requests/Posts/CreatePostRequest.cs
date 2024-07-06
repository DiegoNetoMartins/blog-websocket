using Blog.Shared.Base;
using Blog.Shared.Extensions;
using Blog.Shared.Responses;
using FluentValidation;
using System.ComponentModel.DataAnnotations;

namespace Blog.Shared.Requests.Posts;

public class CreatePostRequest : IRequest
{
    public CreatePostRequest(string title, string content)
    {
        Title = title;
        Content = content;
    }

    [Required]
    [MaxLength(100)]
    public string Title { get; set; }
    [Required]
    public string Content { get; set; }

    public List<ValidationErrorResponse> Validate()
        => new CreatePostValidator().Validate(this).ToErrorList();
}

internal class CreatePostValidator : AbstractValidator<CreatePostRequest>
{
    public CreatePostValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title is required.")
            .MaximumLength(100).WithMessage("Title must not exceed 100 characters.");
        RuleFor(x => x.Content)
            .NotEmpty().WithMessage("Content is required.");
    }
}