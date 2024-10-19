using CarSales.Data;
using CarSales.Data.Abstract;
using CarSales.Data.Concrete;
using CarSales.Service.Abstract;
using CarSales.Service.Concrete;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<CarDbContext>();

builder.Services.AddTransient(typeof(IService<,>), typeof(Service<,>));
builder.Services.AddTransient<ICarService, CarService>();
builder.Services.AddTransient<IUserService, UserService>();

builder.Services.AddScoped<IUnitOfWork<CarDbContext>, UnitOfWork<CarDbContext>>();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie( x =>
{
    x.LoginPath = "/Account/Login"; //nereden login olacak
    x.AccessDeniedPath = "/AccessDenied"; // login engellendiði yer
    x.LogoutPath = "/Account/Logout";
    x.Cookie.Name = "Admin";
    x.Cookie.MaxAge = TimeSpan.FromDays(7);
    x.Cookie.IsEssential = true;
});

builder.Services.AddAuthorization(x =>
{
    x.AddPolicy("AdminPolicy", policy => policy.RequireClaim(ClaimTypes.Role, "Admin"));
    x.AddPolicy("UserPolicy", policy => policy.RequireClaim(ClaimTypes.Role, "Admin", "User"));
    x.AddPolicy("EmployePolicy", policy => policy.RequireClaim(ClaimTypes.Role, "Admin", "Employe"));
    x.AddPolicy("CustomerPolicy", policy => policy.RequireClaim(ClaimTypes.Role, "Admin", "Employe", "Customer"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
            name: "admin",
            pattern: "{area:exists}/{controller=Main}/{action=Index}/{id?}"
          );

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
