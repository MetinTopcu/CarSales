using CarSales.Data;
using CarSales.Data.Concrete;
using CarSales.Service.Abstract;

namespace CarSales.Service.Concrete
{
    public class UserService : UserRepository, IUserService
    {
        public UserService(CarDbContext dbContext) : base(dbContext)
        {
        }
    }
}
