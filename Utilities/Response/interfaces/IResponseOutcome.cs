namespace Utilities.Response.interfaces;

public interface IResponseOutcome<T> : IResponseStructure
{
    T Data { get; set; }
}
