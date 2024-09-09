using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;
using Utilities.Response;

namespace PlayStationHub.API.Filters;

public class ValidationFilterAttribute : IActionFilter
{
    public void OnActionExecuted(ActionExecutedContext context)
    {

    }

    public void OnActionExecuting(ActionExecutingContext context)
    {
        if (!context.ModelState.IsValid)
        {
            var errors = context.ModelState.Values
            .SelectMany(v => v.Errors)
            .Select(e => e.ErrorMessage)
            .ToList();

            var response = new ErrorResponse(
                errors,
                HttpStatusCode.BadRequest,
                ""
            );

            context.Result = new BadRequestObjectResult(response);
        }
    }
}
