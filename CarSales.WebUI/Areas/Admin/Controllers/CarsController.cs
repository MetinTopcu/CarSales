using CarSales.Data;
using CarSales.Entities;
using CarSales.Service.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Drawing.Drawing2D;

namespace CarSales.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CarsController : Controller
    {
        private readonly IService<Car, CarDbContext> _service;
        private readonly IService<Brand, CarDbContext> _serviceBrand;

        public CarsController(IService<Car, CarDbContext> service, IService<Brand, CarDbContext> serviceBrand)
        {
            _service = service;
            _serviceBrand = serviceBrand;
        }
        public async Task<IActionResult> IndexAsync()
        {
            var model = await _service.GetAllAsync();
            return View(model);
        }

        public async Task<IActionResult> CreateAsync()
        {
            ViewBag.BrandId = new SelectList(await _service.GetAllAsync(), "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateAsync(Car car)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _service.InsertOneAsync(car);
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    ModelState.AddModelError("", "Hata Oluştu");
                }
            }
            ViewBag.BrandId = new SelectList(await _service.GetAllAsync(), "Id", "Name");
            return View(car);
        }

        public async Task<IActionResult> EditAsync(int id)
        {
            ViewBag.BrandId = new SelectList(await _service.GetAllAsync(), "Id", "Name");
            var model = await _service.GetByIdAsync(id);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditAsync(Car car)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _service.UpdateOne(car);
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    ModelState.AddModelError("", "Hata Oluştu");
                }
            }
            ViewBag.BrandId = new SelectList(await _service.GetAllAsync(), "Id", "Name");
            return View(car);
        }

        public async Task<IActionResult> DeleteAsync(int id)
        {
            var model = await _service.GetByIdAsync(id);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Car car)
        {
            try
            {
                _service.DeleteOne(car);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
