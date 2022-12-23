namespace Domains;

public class User : Base
{
    public Guid idProfile { get; set; }    
    public string name { get; set; }
    public string login { get; set; }
    public string password { get; set; }
    public string email { get; set; }
    public Profile Profiles { get; set; }
}