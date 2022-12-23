using System.ComponentModel;

namespace Applications.DTO.Request;

public class RequestUser : RequestBase
{   
    [DefaultValue("00000000-0000-0000-0000-000000000000")]
    public Guid idTypeUser { get; set; }
    public string name { get; set; }
    public string login { get; set; }
    public string password { get; set; }
    public string email { get; set; }
}
