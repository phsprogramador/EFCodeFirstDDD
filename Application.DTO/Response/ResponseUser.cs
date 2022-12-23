namespace Applications.DTO.Response;

public class ResponseUser : ResponseBase
{
    public Guid idTypeUser { get; set; }
    public string name { get; set; }
    public string login { get; set; }
    public string password { get; set; }
    public string email { get; set; }
}