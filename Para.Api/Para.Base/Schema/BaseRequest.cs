using System.Text.Json.Serialization;

namespace Para.Base.Schema;

public abstract class BaseRequest
{
    [JsonIgnore]
    public string InsertUser { get; set; } = "System";
}