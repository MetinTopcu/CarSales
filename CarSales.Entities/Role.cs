
using System.ComponentModel.DataAnnotations;

namespace CarSales.Entities
{
    public class Role : IEntity<int>
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
