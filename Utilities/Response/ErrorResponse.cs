using System.Net;
using Utilities.Response.interfaces;

namespace Utilities.Response;

public class ErrorResponse : NullableResponseData, IErrorResponse
{
    public Dictionary<string, List<string>> Errors { get; set; }
    public ErrorResponse(Dictionary<string, List<string>> errors, HttpStatusCode status, string messages) : base(status, messages)
    {
        Errors = errors;
    }


}
