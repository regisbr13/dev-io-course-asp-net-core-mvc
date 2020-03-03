using System;
using System.Threading.Tasks;
using MyStock.Business.Models;

namespace MyStock.Business.Interfaces.Services
{
    public interface ICategoryService
    {
        Task Insert(Category category);
        Task Update(Category category);
        Task Remove(Guid id);
    }
}