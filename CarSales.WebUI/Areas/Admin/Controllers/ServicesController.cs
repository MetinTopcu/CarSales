using CarSales.Data;
using CarSales.Data.Abstract;
using CarSales.Entities;
using CarSales.Service.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarSales.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class ServicesController : Controller
    {
        private readonly IService<CarService, CarDbContext> _service;
        private readonly IUnitOfWork<CarDbContext> _unitOfWork;

        public ServicesController(IService<CarService, CarDbContext> service, IUnitOfWork<CarDbContext> unitOfWork)
        {
            _service = service;
            _unitOfWork = unitOfWork;
        }

        public async Task<IActionResult> IndexAsync()
        {
            var model = await _service.GetAllAsync();
            return View(model);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateAsync(CarService carService)
        {
            var cancellationToken = new CancellationToken();
            await _unitOfWork.BeginTransactionAsync(cancellationToken);
            if (ModelState.IsValid)
            {
                try
                {
                    await _service.InsertOneAsync(carService);
                    await _unitOfWork.CommitAsync(cancellationToken);
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    await _unitOfWork.RollbackAsync(cancellationToken);
                    ModelState.AddModelError("", "Hata Oluştu");
                }
            }

            return View(carService);
        }

        public async Task<IActionResult> EditAsync(int id)
        {
            var model = await _service.GetByIdAsync(id);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditAsync(CarService carService)
        {
            var cancellationToken = new CancellationToken();
            await _unitOfWork.BeginTransactionAsync(cancellationToken);
            if (ModelState.IsValid)
            {
                try
                {
                    _service.UpdateOne(carService);
                    await _unitOfWork.CommitAsync(cancellationToken);
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    await _unitOfWork.RollbackAsync(cancellationToken);
                    ModelState.AddModelError("", "Hata Oluştu");
                }
            }
            return View(carService);
        }
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var model = await _service.GetByIdAsync(id);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteAsync(CarService carService)
        {
            var cancellationToken = new CancellationToken();
            await _unitOfWork.BeginTransactionAsync(cancellationToken);
            try
            {
                _service.DeleteOne(carService);
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
