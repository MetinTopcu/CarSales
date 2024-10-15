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
    [Authorize(Policy = "AdminPolicy")]
    public class SalesController : Controller
    {
        private readonly IService<Sales, CarDbContext> _service;
        private readonly IService<Car, CarDbContext> _serviceCar;
        private readonly IService<Customer, CarDbContext> _serviceCustomer;
        private readonly IUnitOfWork<CarDbContext> _unitOfWork;

        public SalesController(IService<Sales, CarDbContext> service, IService<Customer, CarDbContext> serviceCustomer, IService<Car, CarDbContext> serviceCar, IUnitOfWork<CarDbContext> unitOfWork)
        {
            _service = service;
            _serviceCustomer = serviceCustomer;
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
            ViewBag.CarId = new SelectList(await _serviceCar.GetAllAsync(), "Id", "Model");
            ViewBag.CustomerId = new SelectList(await _serviceCustomer.GetAllAsync(), "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateAsync(Sales sales)
        {
            var cancellationToken = new CancellationToken();
            await _unitOfWork.BeginTransactionAsync(cancellationToken);
            if (ModelState.IsValid)
            {
                try
                {
                    await _service.InsertOneAsync(sales);
                    await _unitOfWork.CommitAsync(cancellationToken);
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    await _unitOfWork.RollbackAsync(cancellationToken);
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
            var cancellationToken = new CancellationToken();
            await _unitOfWork.BeginTransactionAsync(cancellationToken);
            if (ModelState.IsValid)
            {
                try
                {
                    _service.UpdateOne(sales);
                    await _unitOfWork.CommitAsync(cancellationToken);
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    await _unitOfWork.RollbackAsync(cancellationToken);
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
        public async Task<IActionResult> DeleteAsync(Sales sales)
        {
            var cancellationToken = new CancellationToken();
            await _unitOfWork.BeginTransactionAsync(cancellationToken);
            try
            {
                _service.DeleteOne(sales);
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
