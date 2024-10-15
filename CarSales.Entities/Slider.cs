using System.ComponentModel.DataAnnotations;

namespace CarSales.Entities
{
    public class Slider : IEntity<int>
    {
        public int Id { get; set; }
        [StringLength(100)]
        public string? Title { get; set; }
        [StringLength(500)]
        public string? Description { get; set; }
        [StringLength(100)]
        public string? Image { get; set; }
        [StringLength(100)]
        public string? Url { get; set; }

    }
}
