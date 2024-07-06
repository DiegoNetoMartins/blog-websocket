using Blog.Shared.Base;
using Blog.Shared.Extensions;
using Blog.Shared.Responses;
using FluentValidation;
using System.ComponentModel.DataAnnotations;

namespace Blog.Shared.Requests.Users;

public class CreateUserRequest : IRequest
{
    public CreateUserRequest(string name, string email, string password, string confirmPassword)
    {
        Name = name;
        Email = email;
        Password = password;
        ConfirmPassword = confirmPassword;
    }

    [Required]
    [MaxLength(100)]
    public string Name { get; set; }
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    [Required]
    [MinLength(6)]
    public string Password { get; set; }
    [Required]
    [MinLength(6)]
    public string ConfirmPassword { get; set; }

    public List<ValidationErrorResponse> Validate()
        => new CreateUserValidator().Validate(this).ToErrorList();
}

internal class CreateUserValidator : AbstractValidator<CreateUserRequest>
{
    public CreateUserValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(100).WithMessage("Name must not exceed 100 characters.");
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Invalid email address.");
        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required.")
            .MinimumLength(6).WithMessage("Password must be at least 6 characters long.");
        RuleFor(x => x.ConfirmPassword)
            .Equal(x => x.Password).WithMessage("Passwords do not match.");
    }
}
