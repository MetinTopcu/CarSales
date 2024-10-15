
using System.ComponentModel.DataAnnotations;

namespace CarSales.Entities
{
    public class Customer : IEntity<int>
    {
        [Key]
        public int Id { get; set; }
        [Display(Name="Car")]
        public int CarID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string? TCNO { get; set; }
        public string Email { get; set; }
        public string? Adress { get; set; }
        public string? Phone { get; set; }
        public string Notes { get; set; }
        public Car? Car { get; set; }
    }
}
