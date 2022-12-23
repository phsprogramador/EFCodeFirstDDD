namespace Domains;

public class Base
{
    public Guid id { get; set; }
    public DateTime dtInsert { get; set; }
    public DateTime dtUpdate { get; set; }
    public bool active { get; set; }
}
