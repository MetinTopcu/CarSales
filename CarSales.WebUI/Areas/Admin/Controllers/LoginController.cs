using CarSales.Data;
using CarSales.Entities;
using CarSales.Service.Abstract;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CarSales.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class LoginController : Controller
    {
        private readonly IService<User, CarDbContext> _service;
        private readonly IService<Role, CarDbContext> _serviceRole;


        public LoginController(IService<User, CarDbContext> service, IService<Role, CarDbContext> serviceRole)
        {
            _service = service;
            _serviceRole = serviceRole;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> LogoutAsync()
        {
            await HttpContext.SignOutAsync();
            return Redirect("/Admin/Login");
        }
        [HttpPost]
        public async Task<IActionResult> IndexAsync(string email, string password)
        {
            try
            {
                var account = await _service.SingleOrDefaultAsync(x => x.Email == email && x.Password == password && x.IsItActive == true);
                if (account == null)
                {
                    TempData["Mesaj"] = "Login is not successfull";
                }
                else
                {
                    var role = await _serviceRole.FirstOrDefaultAsync(x => x.Id == account.RoleId);
                    var claims = new List<Claim>()
                    {
                        new Claim(ClaimTypes.Name, account.Name ),
                    };
                    if(role is not null)
                    {
                        claims.Add(new Claim(ClaimTypes.Role, role.Name));
                    }
                    var userIdentity = new ClaimsIdentity(claims, "Login");
                    ClaimsPrincipal principal = new ClaimsPrincipal(userIdentity);
                    await HttpContext.SignInAsync(principal);
                    return Redirect("/Admin");
                }
            }
            catch (Exception)
            {
                TempData["Mesaj"] = "Make Mistake";
                throw;
            }
            return View();
        }
    }
}
