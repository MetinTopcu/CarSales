using CarSales.Data;
using CarSales.Data.Abstract;
using CarSales.Entities;
using CarSales.Service.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CarSales.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Policy = "AdminPolicy")]
    public class CustomersController : Controller
    {
        private readonly IService<Customer, CarDbContext> _service;
        private readonly IService<Car, CarDbContext> _serviceCar;
        private readonly IUnitOfWork<CarDbContext> _unitOfWork;

        public CustomersController(IService<Customer, CarDbContext> service, IService<Car, CarDbContext> serviceCar, IUnitOfWork<CarDbContext> unitOfWork)
        {
            _service = service;
            _serviceCar = serviceCar;
            _unitOfWork = unitOfWork;
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
            var cancellationToken = new CancellationToken();
            await _unitOfWork.BeginTransactionAsync(cancellationToken);
            if (ModelState.IsValid)
            {
                try
                {
                    await _service.InsertOneAsync(customer);
                    await _unitOfWork.CommitAsync(cancellationToken);
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    await _unitOfWork.RollbackAsync(cancellationToken);
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
            var cancellationToken = new CancellationToken();
            await _unitOfWork.BeginTransactionAsync(cancellationToken);
            if (ModelState.IsValid)
            {
                try
                {
                    _service.UpdateOne(customer);
                    await _unitOfWork.CommitAsync(cancellationToken);
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    await _unitOfWork.RollbackAsync(cancellationToken);
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
        public async Task<IActionResult> DeleteAsync(Customer customer)
        {
            var cancellationToken = new CancellationToken();
            await _unitOfWork.BeginTransactionAsync(cancellationToken);
            try
            {
                _service.DeleteOne(customer);
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
