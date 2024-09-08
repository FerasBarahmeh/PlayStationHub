using System.Net;
using Utilities.Response.interfaces;

namespace Utilities.Response;

public class NullableResponseData : BaseAttributesForResponseStructure, INullableResponse
{
    public override bool HasError => true;

    public object Data => null;

    public NullableResponseData(HttpStatusCode status, string messages)
    {
        StatusCode = status;
        Message = messages;
    }
}
