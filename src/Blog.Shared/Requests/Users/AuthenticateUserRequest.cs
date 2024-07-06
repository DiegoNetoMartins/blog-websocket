using Blog.Shared.Base;
using Blog.Shared.Extensions;
using Blog.Shared.Responses;
using FluentValidation;
using System.ComponentModel.DataAnnotations;

namespace Blog.Shared.Requests.Users;

public class AuthenticateUserRequest : IRequest
{
    public AuthenticateUserRequest(string email, string password)
    {
        Email = email;
        Password = password;
    }

    [Required]
    [EmailAddress]
    public string Email { get; set; }
    [Required]
    [MinLength(6)]
    public string Password { get; set; }

    public List<ValidationErrorResponse> Validate()
        => new AuthenticateUserValidator().Validate(this).ToErrorList();
}

internal class AuthenticateUserValidator : AbstractValidator<AuthenticateUserRequest>
{
    public AuthenticateUserValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Invalid email address.");
        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required.")
            .MinimumLength(6).WithMessage("Password must be at least 6 characters long.");
    }
}