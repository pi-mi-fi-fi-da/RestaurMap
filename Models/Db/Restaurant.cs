using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestaurMap.Models.Db;
public class Restaurant  
{
    [Required]
    public string Id { get; set; }
    public string Name { get; set; }
    public string PlusCode { get; set; }
    public string Website { get; set; }
}
