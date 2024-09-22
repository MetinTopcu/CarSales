
using System.ComponentModel.DataAnnotations;

namespace CarSales.Entities
{
    public class Brand : IEntity<int>
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
