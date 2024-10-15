using CarSales.Data;
using CarSales.Data.Concrete;
using CarSales.Service.Abstract;

namespace CarSales.Service.Concrete
{
    public class CarService : CarRepository<CarDbContext>, ICarService
    {
        public CarService(CarDbContext dbContext) : base(dbContext)
        {
        }
    }
}
