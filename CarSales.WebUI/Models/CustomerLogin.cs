using System.ComponentModel.DataAnnotations;

namespace CarSales.WebUI.Models
{
    public class CustomerLogin
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
