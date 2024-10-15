using CarSales.Data;
using CarSales.Data.Abstract;
using CarSales.Entities;
using CarSales.Service.Abstract;
using CarSales.WebUI.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Drawing.Drawing2D;

namespace CarSales.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Policy = "AdminPolicy")]
    public class SlidersController : Controller
    {
        private readonly IService<Slider, CarDbContext> _service;
        private readonly IUnitOfWork<CarDbContext> _unitOfWork;

        public SlidersController(IService<Slider, CarDbContext> service, IUnitOfWork<CarDbContext> unitOfWork)
        {
            _service = service;
            _unitOfWork = unitOfWork;
        }

        public async Task<IActionResult> IndexAsync()
        {
            return View(await _service.GetAllAsync());
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateAsync(Slider slider, IFormFile? Image)
        {
            var cancellationToken = new CancellationToken();
            await _unitOfWork.BeginTransactionAsync(cancellationToken);
            if (ModelState.IsValid)
            {
                try
                {
                    slider.Image = await FileHelper.FileLoaderAsync(Image, "/Img/Sliders/");
                    await _service.InsertOneAsync(slider);
                    await _unitOfWork.CommitAsync(cancellationToken);

                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    await _unitOfWork.RollbackAsync(cancellationToken);
                    ModelState.AddModelError("", "Hata Oluştu");
                }
            }
            return View(slider);

        }
        public async Task<IActionResult> EditAsync(int id)
        {
            var model = await _service.GetByIdAsync(id);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditAsync(int id, Slider slider, IFormFile? Image)
        {
            var cancellationToken = new CancellationToken();
            await _unitOfWork.BeginTransactionAsync(cancellationToken);
            if(ModelState.IsValid)
            {
                try
                {
                    if (Image is not null)
                    {
                        slider.Image = await FileHelper.FileLoaderAsync(Image, "/Img/Sliders/");
                    }
                    _service.UpdateOne(slider);
                    await _unitOfWork.CommitAsync(cancellationToken);
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    await _unitOfWork.RollbackAsync(cancellationToken);
                    ModelState.AddModelError("", "Hata Oluştu");
                }
            }
            return View(slider);

        }

        public async Task<IActionResult> DeleteAsync(int id)
        {
            var model = await _service.GetByIdAsync(id);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteAsync(Slider slider)
        {
            var cancellationToken = new CancellationToken();
            await _unitOfWork.BeginTransactionAsync(cancellationToken);
            try
            {
                _service.DeleteOne(slider);
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
