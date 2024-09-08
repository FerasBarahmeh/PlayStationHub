using System.Net;

namespace Utilities.Response.interfaces;

public interface IResponseStructure
{
    HttpStatusCode StatusCode { get; set; }
    string Message { get; set; }
}
