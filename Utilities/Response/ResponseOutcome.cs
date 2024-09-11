using System.Net;
using Utilities.Response.interfaces;
using Utilities.Response.ThirdParty;

namespace Utilities.Response;

public class ResponseOutcome<T> : BaseAttributesForResponseStructure, IResponseOutcome<T>, IMetadata<Metadata>
{
    private int? iD;
    private HttpStatusCode created;
    private string v;

    public T Data { get; set; }

    public override bool HasError => Data == null;

    public Metadata Metadata { get; set; }

    public ResponseOutcome(T data, HttpStatusCode status, string message)
    {
        Data = data;
        StatusCode = status;
        Message = message;
        Metadata = null;
    }
    public ResponseOutcome(T data, HttpStatusCode status, string message, Metadata metadata)
    {
        Data = data;
        StatusCode = status;
        Message = message;
        Metadata = metadata;
    }

    public ResponseOutcome(int? iD, HttpStatusCode created, string v)
    {
        this.iD = iD;
        this.created = created;
        this.v = v;
    }
}
