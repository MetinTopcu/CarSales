
using System.ComponentModel.DataAnnotations;

namespace CarSales.Entities
{
    public class Sales : IEntity<int>
    {
        [Key]
        public int Id { get; set; }
        public int CarId { get; set; }
        public int CustomerId { get; set; }
        public decimal SalesPrice { get; set; }
        public DateTime SalesDate { get; set; }
        public Car? Car { get; set; }
        public Customer? Customer { get; set; }
    }
}
