using CarSales.Data.Abstract;
using CarSales.Data;
using CarSales.Entities;
using CarSales.Service.Abstract;
using CarSales.WebUI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CarSales.WebUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly IService<Slider, CarDbContext> _service;
        private readonly ICarService _carService;
        private readonly IUnitOfWork<CarDbContext> _unitOfWork;

        public HomeController(IUnitOfWork<CarDbContext> unitOfWork, IService<Slider, CarDbContext> service, ICarService carService)
        {
            _unitOfWork = unitOfWork;
            _service = service;
            _carService = carService;
        }

        public async Task<IActionResult> IndexAsync()
        {
            var model = new HomePageViewModel()
            {
                Sliders = await _service.GetAllAsync(),
                Cars = await _carService.CarwithBrandListAsync(x => x.ForMotherPage)
            };

            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }
        [Route("AccessDenied")]
        public IActionResult AccessDenied()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
