namespace Utilities.Response.interfaces;

public interface IResponseOutcome<T> : IResponseStructure
{
    T Response { get; set; }
}
