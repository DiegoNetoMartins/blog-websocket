using Blog.Shared.Responses;

namespace Blog.Shared.Base;

public interface IRequest
{
    List<ValidationErrorResponse> Validate();
}
