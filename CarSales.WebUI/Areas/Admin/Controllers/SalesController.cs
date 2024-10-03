using CarSales.Data;
using CarSales.Entities;
using CarSales.Service.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CarSales.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SalesController : Controller
    {
        private readonly IService<Sales, CarDbContext> _service;
        private readonly IService<Car, CarDbContext> _serviceCar;
        private readonly IService<Customer, CarDbContext> _serviceCustomer;

        public SalesController(IService<Sales, CarDbContext> service, IService<Customer, CarDbContext> serviceCustomer, IService<Car, CarDbContext> serviceCar)
        {
            _service = service;
            _serviceCustomer = serviceCustomer;
            _serviceCar = serviceCar;
        }

        public async Task<IActionResult> IndexAsync()
        {
            var model = await _service.GetAllAsync();
            return View(model);
        }

        public async Task<IActionResult> CreateAsync()
        {
            ViewBag.CarId = new SelectList(await _serviceCar.GetAllAsync(), "Id", "Model");
            ViewBag.CustomerId = new SelectList(await _serviceCustomer.GetAllAsync(), "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateAsync(Sales sales)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _service.InsertOneAsync(sales);
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    ModelState.AddModelError("", "Hata Oluştu");
                }
            }
            ViewBag.CarId = new SelectList(await _serviceCar.GetAllAsync(), "Id", "Model");
            ViewBag.CustomerId = new SelectList(await _serviceCustomer.GetAllAsync(), "Id", "Name");

            return View(sales);
        }

        public async Task<IActionResult> EditAsync(int id)
        {
            ViewBag.CarId = new SelectList(await _serviceCar.GetAllAsync(), "Id", "Model");
            ViewBag.CustomerId = new SelectList(await _serviceCustomer.GetAllAsync(), "Id", "Name");
            var model = await _service.GetByIdAsync(id);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditAsync(Sales sales)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _service.UpdateOne(sales);
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    ModelState.AddModelError("", "Hata Oluştu");
                }
            }
            ViewBag.CarId = new SelectList(await _serviceCar.GetAllAsync(), "Id", "Model");
            ViewBag.CustomerId = new SelectList(await _serviceCustomer.GetAllAsync(), "Id", "Name");
            return View(sales);
        }

        public async Task<IActionResult> DeleteAsync(int id)
        {
            var model = await _service.GetByIdAsync(id);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Sales sales)
        {
            try
            {
                _service.DeleteOne(sales);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
