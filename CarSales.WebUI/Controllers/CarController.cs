using CarSales.Data;
using CarSales.Data.Abstract;
using CarSales.Entities;
using CarSales.Service.Abstract;
using CarSales.WebUI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CarSales.WebUI.Controllers
{
    public class CarController : Controller
    {
        private readonly ICarService _carService;
        private readonly IService<Customer, CarDbContext> _customerService;
        private readonly IUnitOfWork<CarDbContext> _unitOfWork;
        private readonly IUserService _userService;


        public CarController(ICarService carService, IService<Customer, CarDbContext> customerService, IUnitOfWork<CarDbContext> unitOfWork, IUserService userService)
        {
            _carService = carService;
            _customerService = customerService;
            _unitOfWork = unitOfWork;
            _userService = userService;
        }

        public async Task<IActionResult> IndexAsync(int? id)
        {
            if (id == null)
                return BadRequest();


            var arac = await _carService.CarwithBrandFirstOrDefaultAsync(id.Value);
            if (arac == null)
                return NotFound();
            var model = new CarDetailViewModel();
            model.Car = arac;

            if (User.Identity.IsAuthenticated)
            {
                var email = User.FindFirst(ClaimTypes.Email)?.Value;
                var uguid = User.FindFirst(ClaimTypes.UserData)?.Value;
                if (!string.IsNullOrEmpty(email) || !string.IsNullOrEmpty(uguid))
                {
                    var user = await _userService.FirstOrDefaultAsync(k => k.Email == email && k.UserGuid.ToString() == uguid);
                    if (user != null)
                    {
                        model.Customer = new Customer
                        {
                            Name = user.Name,
                            Surname = user.Surname,
                            Email = user.Email,
                            Phone = user.Phone
                        };
                    }
                }
            }

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

