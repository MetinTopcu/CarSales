using CarSales.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarSales.Data
{
    public class CarDbContext : DbContext
    {
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Sales> Sales { get; set; }
        public DbSet<CarService> Services { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"server=(LocalDB)\MSSQLLocalDB; database=CarSales;integrated security=True;MultipleActiveResultSets=True;");

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Brand>().Property(m => m.Name).IsRequired().HasColumnType("varchar(50)");
            modelBuilder.Entity<Role>().Property(m => m.Name).IsRequired().HasColumnType("varchar(50)");
            modelBuilder.Entity<Role>().HasData(new Role
            {
                Id = 1,
                Name = "Admin"
            });
            modelBuilder.Entity<User>().HasData(new User
            {
                Id = 1,
                Name = "Admin",
                IsItActive = true,
                UserCreateDate = DateTime.Now,
                Email = "admin@otoservissatis.tc",
                UserName = "admin",
                Password = "123456",
                RoleId = 1,
                Surname = "admin",
                Phone = "0850",
            });
            base.OnModelCreating(modelBuilder);
        }
    }
}
