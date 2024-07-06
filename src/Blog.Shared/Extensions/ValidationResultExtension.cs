using Blog.Shared.Responses;
using FluentValidation.Results;

namespace Blog.Shared.Extensions;

internal static class ValidationResultExtension
{
    public static List<ValidationErrorResponse> ToErrorList(this ValidationResult validationResult)
       => validationResult.Errors.Select(x => new ValidationErrorResponse(x.ErrorCode, x.ErrorMessage)).ToList();
}
