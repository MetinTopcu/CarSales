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
    public class UsersController : Controller
    {
        private readonly IService<User, CarDbContext> _service;
        private readonly IService<Role, CarDbContext> _serviceRole;
        private readonly IUnitOfWork<CarDbContext> _unitOfWork;


        public UsersController(IService<User, CarDbContext> service, IService<Role, CarDbContext> serviceRole, IUnitOfWork<CarDbContext> unitOfWork)
        {
            _service = service;
            _serviceRole = serviceRole;
            _unitOfWork = unitOfWork;
        }
        public async Task<IActionResult> IndexAsync()
        {
            var model = await _service.GetAllAsync();
            return View(model);
        }

        public async Task<IActionResult> CreateAsync()
        {
            ViewBag.RoleId = new SelectList(await _serviceRole.GetAllAsync(), "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateAsync(User user)
        {
            var cancellationToken = new CancellationToken();
            await _unitOfWork.BeginTransactionAsync(cancellationToken);
            if (ModelState.IsValid)
            {
                try
                {
                    await _service.InsertOneAsync(user);
                    await _unitOfWork.CommitAsync(cancellationToken);
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    await _unitOfWork.RollbackAsync(cancellationToken);
                    ModelState.AddModelError("", "Hata oluştu");
                }
            }
            ViewBag.RoleId = new SelectList(await _serviceRole.GetAllAsync(), "Id", "Name");
            return View(user);

        }

        public async Task<IActionResult> EditAsync(int id)
        {
            var model = await _service.GetByIdAsync(id);
            ViewBag.RoleId = new SelectList(await _serviceRole.GetAllAsync(), "Id", "Name");
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditAsync(User user)
        {
            var cancellationToken = new CancellationToken();
            await _unitOfWork.BeginTransactionAsync(cancellationToken);
            if (ModelState.IsValid)
            {
                try
                {
                    _service.UpdateOne(user);
                    await _unitOfWork.CommitAsync(cancellationToken);
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    await _unitOfWork.RollbackAsync(cancellationToken);
                    ModelState.AddModelError("", "Hata oluştu");
                }
            }
            ViewBag.RoleId = new SelectList(await _serviceRole.GetAllAsync(), "Id", "Name");
            return View(user);
        }

        public async Task<IActionResult> DeleteAsync(int id)
        {
            var model = await _service.GetByIdAsync(id);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteAsync(User user)
        {
            var cancellationToken = new CancellationToken();
            await _unitOfWork.BeginTransactionAsync(cancellationToken);
            try
            {
                _service.DeleteOne(user);
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
