using CarSales.Data.Abstract;
using CarSales.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarSales.Service.Abstract
{
    public interface IService<T> : IGenericRepository<T, int> where T : class, IEntity<int>, new()
    {
    }
}
