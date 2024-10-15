using CarSales.Data;
using CarSales.Data.Abstract;
using CarSales.Entities;
using CarSales.Service.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace CarSales.WebUI.Controllers
{
    public class CarController : Controller
    {
        private readonly ICarService _carService;
        private readonly IService<Customer, CarDbContext> _customerService;
        private readonly IUnitOfWork<CarDbContext> _unitOfWork;


        public CarController(ICarService carService, IService<Customer, CarDbContext> customerService, IUnitOfWork<CarDbContext> unitOfWork)
        {
            _carService = carService;
            _customerService = customerService;
            _unitOfWork = unitOfWork;
        }

        public async Task<IActionResult> IndexAsync(int id)
        {
            var model = await _carService.CarwithBrandFirstOrDefaultAsync(id);
            return View(model);
        }
        [Route("all-car")]
        public async Task<IActionResult> ListAsync()
        {
            var model = await _carService.CarwithBrandListAsync(x => x.IsItOnSale);
            return View(model);
        }
        public async Task<IActionResult> SearchAsync(string query)
        {
            var model = await _carService.CarwithBrandListAsync(x => x.IsItOnSale && x.Brand.Name.Contains(query) || x.BodyType.Contains(query) || x.Model.Contains(query));
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddCustomerAsync(Customer customer)
        {
            var cancellationToken = new CancellationToken();
            await _unitOfWork.BeginTransactionAsync(cancellationToken);
            if (ModelState.IsValid)
            {
                try
                {
                    await _customerService.InsertOneAsync(customer);
                    await _unitOfWork.CommitAsync(cancellationToken);
                    TempData["Message"] = "<div class='alert alert-success'>Talebiniz Alınmıştır. Teşekkürler..</div>";
                    return Redirect("/Car/Index/" + customer.CarID);
                }
                catch
                {
                    await _unitOfWork.RollbackAsync(cancellationToken);
                    ModelState.AddModelError("", "Hata Oluştu!");
                }
            }

            return View();
        }


    }
}

