using System.ComponentModel;

namespace Applications.DTO.Request;

public class RequestBase
{   
    [DefaultValue("00000000-0000-0000-0000-000000000000")]
    public Guid id { get; set; }
    [DefaultValue("1900-01-01T00:00:00.000Z")]    
    public DateTime dtInsert { get; set; }
    [DefaultValue("1900-01-01T00:00:00.000Z")]    
    public DateTime dtUpdate { get; set; }
    [DefaultValue("true")]    
    public bool active { get; set; }
}