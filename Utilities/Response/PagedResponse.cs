using System.Net;
using Utilities.Response.interfaces;

namespace Utilities.Response;

public class PagedResponse<T> : IPagedResponse<T>, IResponseStructure
{
    public IEnumerable<T> Data { get; set; }
    public int SlideNumber { get; set; }
    public int SlideSize { get; set; }
    public int TotalCount { get; set; }
    public HttpStatusCode StatusCode { get; set; }
    public string Message { get; set; }
}
