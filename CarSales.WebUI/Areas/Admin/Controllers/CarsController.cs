using CarSales.Data;
using CarSales.Data.Abstract;
using CarSales.Entities;
using CarSales.Service.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CarSales.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class CarsController : Controller
    {
        private readonly IService<Car, CarDbContext> _service;
        private readonly IService<Brand, CarDbContext> _serviceBrand;
        private readonly IUnitOfWork<CarDbContext> _unitOfWork;

        public CarsController(IService<Car, CarDbContext> service, IService<Brand, CarDbContext> serviceBrand, IUnitOfWork<CarDbContext> unitOfWork)
        {
            _service = service;
            _serviceBrand = serviceBrand;
            _unitOfWork = unitOfWork;
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
            var cancellationToken = new CancellationToken();
            await _unitOfWork.BeginTransactionAsync(cancellationToken);
            if (ModelState.IsValid)
            {
                try
                {
                    await _service.InsertOneAsync(car);
                    await _unitOfWork.CommitAsync(cancellationToken);

                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    await _unitOfWork.RollbackAsync(cancellationToken);
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
            var cancellationToken = new CancellationToken();
            await _unitOfWork.BeginTransactionAsync(cancellationToken);
            if (ModelState.IsValid)
            {
                try
                {
                    _service.UpdateOne(car);
                    await _unitOfWork.CommitAsync(cancellationToken);
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    await _unitOfWork.RollbackAsync(cancellationToken);
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
        public async Task<IActionResult> DeleteAsync(Car car)
        {
            var cancellationToken = new CancellationToken();
            await _unitOfWork.BeginTransactionAsync(cancellationToken);
            try
            {
                _service.DeleteOne(car);
                await _unitOfWork.CommitAsync(cancellationToken);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                await _unitOfWork.RollbackAsync(cancellationToken);

                return View();
            }
        }
    }
}
