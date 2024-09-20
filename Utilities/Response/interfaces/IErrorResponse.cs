namespace Utilities.Response.interfaces;

public interface IErrorResponse
{
    Dictionary<string, List<string>> Errors { get; set; }
}
