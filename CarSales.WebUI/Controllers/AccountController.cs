using CarSales.Data;
using CarSales.Data.Abstract;
using CarSales.Entities;
using CarSales.Service.Abstract;
using CarSales.WebUI.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CarSales.WebUI.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService _service;
        private readonly IUnitOfWork<CarDbContext> _unitOfWork;
        private readonly IService<Role, CarDbContext> _roleService;

        public AccountController(IUserService service, IUnitOfWork<CarDbContext> unitOfWork, IService<Role, CarDbContext> roleService)
        {
            _service = service;
            _unitOfWork = unitOfWork;
            _roleService = roleService;
        }
        [Authorize(Policy = "CustomerPolicy")]
        public async Task<IActionResult> IndexAsync()
        {
            var email = User.FindFirst(ClaimTypes.Email)?.Value;
            var userGuid = User.FindFirst(ClaimTypes.UserData)?.Value;
            if (!string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(userGuid))
            {
                var user = await _service.FirstOrDefaultAsync(x => x.Email == email && x.UserGuid.ToString() == userGuid);
                if (user != null)
                {
                    return View(user);
                }
            }
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> UserUpdateAsync(User user)
        {
            var cancellationToken = new CancellationToken();
            await _unitOfWork.BeginTransactionAsync(cancellationToken);
            try
            {
                var email = User.FindFirst(ClaimTypes.Email)?.Value;
                var userGuid = User.FindFirst(ClaimTypes.UserData)?.Value;
                if (!string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(userGuid))
                {
                    var getUser = await _service.FirstOrDefaultAsync(x => x.Email == email && x.UserGuid.ToString() == userGuid);
                    if (getUser != null)
                    {
                        getUser.Name = user.Name;
                        getUser.IsItActive = user.IsItActive;
                        getUser.Email = user.Email;
                        getUser.Password = user.Password;
                        getUser.Surname = user.Surname;
                        getUser.Phone = user.Phone;
                        getUser.UserName = user.Name + user.Surname + user.Id;

                        _service.UpdateOne(getUser);
                        await _unitOfWork.CommitAsync(cancellationToken);
                    }
                }
            }
            catch (Exception)
            {
                await _unitOfWork.RollbackAsync(cancellationToken);
                ModelState.AddModelError("", "Hata Oluştu!");
            }

            return RedirectToAction("Index");
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> LoginAsync(CustomerLogin customerLogin)
        {
            try
            {
                var account = await _service.SingleOrDefaultAsync(x => x.Email == customerLogin.Email && x.Password == customerLogin.Password && x.IsItActive == true);
                if (account == null)
                {
                    ModelState.AddModelError("", "Login is not Successfull");
                }
                else
                {
                    var role = await _roleService.FirstOrDefaultAsync(x => x.Id == account.RoleId);
                    var claims = new List<Claim>()
                    {
                        new Claim(ClaimTypes.Name, account.Name ),
                        new Claim(ClaimTypes.Email, account.Email),
                        new Claim(ClaimTypes.UserData, account.UserGuid.ToString())
                    };
                    if (role is not null)
                    {
                        claims.Add(new Claim(ClaimTypes.Role, role.Name));
                    }
                    var userIdentity = new ClaimsIdentity(claims, "Login");
                    ClaimsPrincipal principal = new ClaimsPrincipal(userIdentity);
                    await HttpContext.SignInAsync(principal);
                    if (role.Name == "Admin")
                    {
                        return Redirect("/Admin");
                    }
                    return Redirect("/Account");
                }
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Login is not Successfull");
                throw;
            }
            return View();
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> RegisterAsync(User user)
        {
            var cancellationToken = new CancellationToken();
            await _unitOfWork.BeginTransactionAsync(cancellationToken);
            if (ModelState.IsValid)
            {
                try
                {
                    var role = await _roleService.SingleOrDefaultAsync(x => x.Name == "Customer");
                    if (role == null)
                    {
                        ModelState.AddModelError("", "Register is not Successfull");
                        return View();
                    }
                    user.RoleId = role.Id;
                    user.UserName = user.Name + user.Surname + user.Id;
                    user.IsItActive = true;
                    user.UserCreateDate = DateTime.Now;
                    await _service.InsertOneAsync(user, cancellationToken);
                    await _unitOfWork.CommitAsync(cancellationToken);
                    return Redirect("/Login");
                }
                catch (Exception)
                {
                    await _unitOfWork.RollbackAsync(cancellationToken);
                    ModelState.AddModelError("", "Hata Oluştu!");
                }
            }
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                foreach (var error in errors)
                {
                    Console.WriteLine(error);  // Loglama veya hata mesajını ekranda gösterme
                }
                return View(user);
            }
            return View("Register", user);
        }
        public async Task<IActionResult> LogoutAsync()
        {
            await HttpContext.SignOutAsync();
            return Redirect("/");
        }
    }
}
