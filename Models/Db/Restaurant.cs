using Microsoft.Build.Framework;

namespace RestaurMap.Models.Db
{
    public class Restaurant
    {
        [Required]
        public string Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public string Adress { get; set; }
        public decimal CordX { get; set; }
        public decimal CordY { get; set; }
    }
}
