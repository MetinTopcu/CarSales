using CarSales.Data;
using CarSales.Entities;
using CarSales.Service.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CarSales.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CustomersController : Controller
    {
        private readonly IService<Customer, CarDbContext> _service;
        private readonly IService<Car, CarDbContext> _serviceCar;

        public CustomersController(IService<Customer, CarDbContext> service, IService<Car, CarDbContext> serviceCar)
        {
            _service = service;
            _serviceCar = serviceCar;
        }
        public async Task<IActionResult> IndexAsync()
        {
            var model = await _service.GetAllAsync();
            return View(model);
        }

        public async Task<IActionResult> CreateAsync()
        {
            ViewBag.CarID = new SelectList(await _serviceCar.GetAllAsync(), "Id", "Model");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateAsync(Customer customer)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _service.InsertOneAsync(customer);
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    ModelState.AddModelError("", "Hata Oluştu");
                }
            }
            ViewBag.CarID = new SelectList(await _serviceCar.GetAllAsync(), "Id", "Model");
            return View(customer);
        }

        public async Task<IActionResult> EditAsync(int id)
        {
            ViewBag.CarID = new SelectList(await _serviceCar.GetAllAsync(), "Id", "Model");
            var model = await _service.GetByIdAsync(id);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditAsync(Customer customer)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _service.UpdateOne(customer);
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    ModelState.AddModelError("", "Hata Oluştu");
                }
            }
            ViewBag.CarID = new SelectList(await _serviceCar.GetAllAsync(), "Id", "Model");
            return View(customer);
        }

        public async Task<IActionResult> DeleteAsync(int id)
        {
            var model = await _service.GetByIdAsync(id);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Customer customer)
        {
            try
            {
                _service.DeleteOne(customer);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
