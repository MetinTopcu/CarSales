using CarSales.Entities;

namespace CarSales.WebUI.Models
{
    public class HomePageViewModel
    {
        public IEnumerable<Slider> Sliders { get; set; }
        public IEnumerable<Car> Cars { get; set; }

    }
}
