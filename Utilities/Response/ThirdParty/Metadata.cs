namespace Utilities.Response.ThirdParty;

public class Metadata
{
    public Dictionary<string, object> Data { get; set; } = new Dictionary<string, object>();
    public Metadata() { }
    public Metadata(Dictionary<string, object> data)
    {
        Data = data;
    }

    public Metadata Push(string Key, object Value)
    {
        Data.Add(Key, Value);
        return this;
    }
}
