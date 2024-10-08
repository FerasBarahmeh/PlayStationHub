using System.Net;
using Utilities.Response.interfaces;
using Utilities.Response.ThirdParty;

namespace Utilities.Response;

public class ResponseOutcome<T> : BaseAttributesForResponseStructure, IResponseOutcome<T>, IMetadata<Metadata>
{
    public T Response { get; set; }

    public override bool HasError => Response == null;

    public Metadata Metadata { get; set; }

    public ResponseOutcome(T data, HttpStatusCode status, string message)
    {
        Response = data;
        StatusCode = status;
        Message = message;
        Metadata = null;
    }
    public ResponseOutcome(T data, HttpStatusCode status, string message, Metadata metadata)
    {
        Response = data;
        StatusCode = status;
        Message = message;
        Metadata = metadata;
    }
}
