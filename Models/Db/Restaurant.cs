using Microsoft.Build.Framework;

namespace RestaurMap.Models.Db;
public class Restaurant  
{
    [Required]
    public string Id { get; set; }
    public string Name { get; set; }
    public string PlusCode { get; set; }
    public string Website { get; set; }
    public string Category { get; set; }
    public string Adress { get; set; }
}
