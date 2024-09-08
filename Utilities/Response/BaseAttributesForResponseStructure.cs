using System.Net;
using Utilities.Response.interfaces;

namespace Utilities.Response;

public abstract class BaseAttributesForResponseStructure : IResponseStructure
{
    public HttpStatusCode StatusCode { get; set; }
    public string Message { get; set; }
    public abstract bool HasError { get; }
}
