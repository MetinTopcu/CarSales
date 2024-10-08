using CarSales.Data;
using CarSales.Entities;
using CarSales.Service.Abstract;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CarSales.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class LoginController : Controller
    {
        private readonly IService<User, CarDbContext> _service;

        public LoginController(IService<User, CarDbContext> service)
        {
            _service = service;
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
                    TempData["Mesaj"] = "Giriş Başarısız";
                }
                else
                {
                    var claims = new List<Claim>()
                    {
                        new Claim(ClaimTypes.Name, account.Name ),
                        new Claim("Role", "Admin"),
                    };
                    var userIdentity = new ClaimsIdentity(claims, "Login");
                    ClaimsPrincipal principal = new ClaimsPrincipal(userIdentity);
                    await HttpContext.SignInAsync(principal);
                    return Redirect("/Admin");
                }
            }
            catch (Exception)
            {
                TempData["Mesaj"] = "Hata Oluştu";
                throw;
            }
            return View();
        }
    }
}
