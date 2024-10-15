
using System.ComponentModel.DataAnnotations;

namespace CarSales.Entities
{
    public class Car : IEntity<int>
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Brand Name"), Required(ErrorMessage = "{0} Required")]
        public int BrandId { get; set; }
        [StringLength(50)]
        public string Color { get; set; }
        public decimal Price { get; set; }
        [StringLength(50)]
        public string Model { get; set; }
        [StringLength(50)]
        public string BodyType { get; set; }
        public int ModelYear { get; set; }
        [Display(Name = "for sale")]
        public bool IsItOnSale { get; set; }
        [Display(Name = "for mother page")]
        public bool ForMotherPage { get; set; }
        public string Notes { get; set; }
        [StringLength(100)]
        public string? Image1 { get; set; }
        [StringLength(100)]
        public string? Image2 { get; set; }
        [StringLength(100)]
        public string? Image3 { get; set; }
        [StringLength(100)]
        public string? Image4 { get; set; }
        [StringLength(100)]
        public string? Image5 { get; set; }
        public Brand? Brand { get; set; }
    }
}
