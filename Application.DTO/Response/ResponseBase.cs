using System.Text.Json.Serialization;

namespace Applications.DTO.Response;

public class ResponseBase
{
    public Guid id { get; set; }
    public DateTime dtInsert { get; set; }
    public DateTime dtUpdate { get; set; }
    public bool active { get; set; }
    [JsonIgnore]
    public string message { get; set; }
    [JsonIgnore]
    public int statusCode { get; set; }
}
