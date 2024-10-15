using CarSales.Data;
using CarSales.Data.Abstract;
using CarSales.Entities;
using CarSales.Service.Abstract;
using CarSales.WebUI.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CarSales.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Policy = "AdminPolicy")]
    public class CarsController : Controller
    {
        private readonly ICarService _service;
        private readonly IService<Brand, CarDbContext> _serviceBrand;
        private readonly IUnitOfWork<CarDbContext> _unitOfWork;

        public CarsController(ICarService service, IService<Brand, CarDbContext> serviceBrand, IUnitOfWork<CarDbContext> unitOfWork)
        {
            _service = service;
            _serviceBrand = serviceBrand;
            _unitOfWork = unitOfWork;
        }
        public async Task<IActionResult> IndexAsync()
        {
            var model = await _service.CarwithBrandListAsync();
            return View(model);
        }

        public async Task<IActionResult> CreateAsync()
        {
            ViewBag.BrandId = new SelectList(await _serviceBrand.GetAllAsync(), "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateAsync(Car car, IFormFile? Image1, IFormFile? Image2, IFormFile? Image3, IFormFile? Image4, IFormFile? Image5)
        {
            var cancellationToken = new CancellationToken();
            await _unitOfWork.BeginTransactionAsync(cancellationToken);
            if (ModelState.IsValid)
            {
                try
                {
                    car.Image1 = await FileHelper.FileLoaderAsync(Image1, "/Img/Cars/");
                    car.Image2 = await FileHelper.FileLoaderAsync(Image2, "/Img/Cars/");
                    car.Image3 = await FileHelper.FileLoaderAsync(Image3, "/Img/Cars/");
                    car.Image4 = await FileHelper.FileLoaderAsync(Image4, "/Img/Cars/");
                    car.Image5 = await FileHelper.FileLoaderAsync(Image5, "/Img/Cars/");
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
            ViewBag.BrandId = new SelectList(await _serviceBrand.GetAllAsync(), "Id", "Name");
            return View(car);
        }

        public async Task<IActionResult> EditAsync(int id)
        {
            ViewBag.BrandId = new SelectList(await _serviceBrand.GetAllAsync(), "Id", "Name");
            var model = await _service.GetByIdAsync(id);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditAsync(Car car, IFormFile? Image1, IFormFile? Image2, IFormFile? Image3, IFormFile? Image4, IFormFile? Image5)
        {
            var cancellationToken = new CancellationToken();
            await _unitOfWork.BeginTransactionAsync(cancellationToken);
            if (ModelState.IsValid)
            {
                try
                {
                    if (Image1 is not null)
                    {
                        car.Image1 = await FileHelper.FileLoaderAsync(Image1, "/Img/Cars/");
                    }
                    if (Image2 is not null)
                    {
                        car.Image2 = await FileHelper.FileLoaderAsync(Image2, "/Img/Cars/");
                    }
                    if (Image3 is not null)
                    {
                        car.Image3 = await FileHelper.FileLoaderAsync(Image3, "/Img/Cars/");
                    }
                    if (Image4 is not null)
                    {
                        car.Image4 = await FileHelper.FileLoaderAsync(Image4, "/Img/Cars/");
                    }
                    if (Image5 is not null)
                    {
                        car.Image5 = await FileHelper.FileLoaderAsync(Image5, "/Img/Cars/");
                    }
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
            ViewBag.BrandId = new SelectList(await _serviceBrand.GetAllAsync(), "Id", "Name");
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
