using Legacy.Shared.ErrorHandling;
using Legacy.Shared.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Legacy.Shared;

public abstract class SystemController : ControllerBase
{
    protected ObjectResult Problem(IReadOnlyList<Error> errors)
    {
        HttpContext.Items[HttpContextItems.Error] = errors;

        var firstError = errors[0];

        var statusCode = firstError.ErrorType switch
        {
            ErrorType.Generic => StatusCodes.Status400BadRequest,
            ErrorType.Validation => StatusCodes.Status400BadRequest,
            ErrorType.NotFound => StatusCodes.Status404NotFound,
            _ => StatusCodes.Status500InternalServerError
        };

        return Problem(statusCode: statusCode, title: firstError.Message);
    }

    protected ObjectResult BadRequest(string title) => Problem(statusCode: StatusCodes.Status400BadRequest, title: title);
}
