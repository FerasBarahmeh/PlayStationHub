namespace Utilities.Response.interfaces;

public interface IPagedResponse<T>
{
    public IEnumerable<T> Data { get; set; }
    public int SlideNumber { get; set; }
    public int SlideSize { get; set; }
    public int TotalCount { get; set; }
}
