using System;
using System.Threading.Tasks;
using MyStock.Business.Models;

namespace MyStock.Business.Interfaces.Repository
{
    public interface ICategoryRepository : IRepository<Category>
    {
         Task<Category> FindById(Guid id, bool products);
    }
}