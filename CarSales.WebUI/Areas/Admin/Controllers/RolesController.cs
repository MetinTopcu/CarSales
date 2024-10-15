﻿using CarSales.Data;
using CarSales.Data.Abstract;
using CarSales.Entities;
using CarSales.Service.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarSales.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Policy = "AdminPolicy")]
    public class RolesController : Controller
    {
        private readonly IService<Role, CarDbContext> _service;
        private readonly IUnitOfWork<CarDbContext> _unitOfWork;

        public RolesController(IService<Role, CarDbContext> service, IUnitOfWork<CarDbContext> unitOfWork)
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
        public async Task<IActionResult> CreateAsync(Role role)
        {
            var cancellationToken = new CancellationToken();
            await _unitOfWork.BeginTransactionAsync(cancellationToken);
            try
            {
                await _service.InsertOneAsync(role);
                await _unitOfWork.CommitAsync(cancellationToken);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                await _unitOfWork.RollbackAsync(cancellationToken);
                return View();
            }
        }
        public async Task<IActionResult> EditAsync(int id)
        {
            var model = await _service.GetByIdAsync(id);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditAsync(int id, Role role)
        {
            var cancellationToken = new CancellationToken();
            await _unitOfWork.BeginTransactionAsync(cancellationToken);
            try
            {
                _service.UpdateOne(role);
                await _unitOfWork.CommitAsync(cancellationToken);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                await _unitOfWork.RollbackAsync(cancellationToken);
                return View();
            }
        }
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var model = await _service.GetByIdAsync(id);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteAsync(Role role)
        {
            var cancellationToken = new CancellationToken();
            await _unitOfWork.BeginTransactionAsync(cancellationToken);
            try
            {
                _service.DeleteOne(role);
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
