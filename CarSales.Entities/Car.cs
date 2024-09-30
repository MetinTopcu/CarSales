
using System.ComponentModel.DataAnnotations;

namespace CarSales.Entities
{
    public class Car : IEntity<int>
    {
        [Key]
        public int Id { get; set; }
        public int BrandId { get; set; }
        [StringLength(50)]
        public string Color { get; set; }
        public decimal Price { get; set; }
        [StringLength(50)]
        public string Model { get; set; }
        [StringLength(50)]
        public string BodyType { get; set; }
        public int ModelYear { get; set; }
        public bool IsItOnSale { get; set; }
        public string Notes { get; set; }
        public Brand? Brand { get; set; }
    }
}
