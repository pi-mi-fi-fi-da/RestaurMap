using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using RestaurMap.Repository;

namespace RestaurMap.Models.Db
{
    public class Restaurant : IEntity<int>
    {
        [Required]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public string Adress { get; set; }
        public decimal CordX { get; set; }
        public decimal CordY { get; set; }
    }
}
